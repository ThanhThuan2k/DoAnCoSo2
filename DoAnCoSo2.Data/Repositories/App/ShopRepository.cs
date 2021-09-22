using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.RequestModel.Shop;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Repositories.App
{
	public class ShopRepository : IShopRepository
	{
		private readonly DoAnCoSo2DbContext db;
		private readonly ISysErrorRepository ISysErrorRepository;
		private IConfiguration IConfiguration;
		public ShopRepository(DoAnCoSo2DbContext _db, ISysErrorRepository _error, IConfiguration _config)
		{
			db = _db;
			ISysErrorRepository = _error;
			IConfiguration = _config;
		}

		public async Task<Customer> GetCustomer(string salt)
		{
			return await db.Customers.AsNoTracking().Include(x => x.Shop).SingleOrDefaultAsync(x => x.Salt == salt && x.DeleteAt == null);
		}

		public async Task<Shop> GetShopByUri(string uri)
		{
			return await db.Shops.SingleOrDefaultAsync(x => x.ShopUri == uri && x.DeleteAt == null);
		}

		public bool IsExistShop(int customerId)
		{
			return db.Shops.Any(x => x.OwnerID == customerId);
		}

		public async Task<StandardResponse> CreateShop(Shop newShop)
		{
			try
			{
				await db.Shops.AddAsync(newShop);
				await db.SaveChangesAsync();

				return new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = newShop
				};
			}
			catch(Exception ex)
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 0,
						ErrorMessage = ex.Message
					}
				};
			}
		}

		public async Task<List<Shop>> Search(string searchString)
		{
			return await db.Shops.AsNoTracking().Where(x => x.Name.Contains(searchString) || x.Nickname.Contains(searchString)).ToListAsync();
		}

		public async Task<StandardResponse> UploadAvatar(string salt, string path)
		{
			Shop shop = await db.Shops.SingleOrDefaultAsync(x => x.ShopUri == salt);
			string thisDomain = IConfiguration.GetSection("Domain").Value;
			if (shop != null)
			{
				shop.Avatar = thisDomain + path;
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = new { URL = thisDomain + path },
					Error = null
				};
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new { URL = thisDomain + path },
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = ISysErrorRepository.GetName(1404)
					}
				};
			}
		}
		public async Task<string> GetAddress(string customerSalt)
		{
			return await db.Customers.AsNoTracking()
				.Where(x => x.Salt == customerSalt && x.DeleteAt == null)
				.Include(x => x.Addresses)
				.Select(x => x.Addresses
					.Where(x => x.IsDefault == true && x.DeleteAt == null)
					.Select(x => x.FullAddress)
					.FirstOrDefault())
				.FirstOrDefaultAsync();
		}
		public async Task<Customer> GetCustomerWithTracking(string customerSalt)
		{
			return await db.Customers.SingleOrDefaultAsync(x => x.Salt == customerSalt && x.DeleteAt == null);
		}
		public async Task<StandardResponse> GetAll()
		{
			return new StandardResponse()
			{
				IsSuccess = true,
				Payload = await db.Shops.AsNoTracking()
				.Where(x => x.DeleteAt == null)
				.Include(x => x.Customer)
				.Include(x => x.Products)
				.ToListAsync(),
				Error = null
			};
		}
	}
}
