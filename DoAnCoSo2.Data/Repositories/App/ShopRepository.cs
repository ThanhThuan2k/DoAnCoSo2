using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.RequestModel.Shop;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
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
		public ShopRepository(DoAnCoSo2DbContext _db, ISysErrorRepository _error)
		{
			db = _db;
			ISysErrorRepository = _error;
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

		public async Task<StandardResponse> CreateShop(string customerSalt, RegisterShopRequestModel shop)
		{
			Customer thisCustomer = await db.Customers.SingleOrDefaultAsync(x => x.Salt == customerSalt);
			if (IsExistShop(thisCustomer.Id))
			{
				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = db.Shops.Single(x => x.OwnerID == thisCustomer.Id),
					Error = new StandardError()
					{
						ErrorCode = 1939,
						ErrorMessage = ISysErrorRepository.GetName(1939)
					}
				};
			}
			else
			{
				thisCustomer.Shop = new Shop()
				{
					OwnerID = thisCustomer.Id,
					CreateAt = DateTime.Now,
					CreateDate = DateTime.Now,
					ShopUri = PasswordHelper.RandomNumber(10, 11),
					Address = thisCustomer.Addresses.Where(x => x.IsDefault == true).Select(x => x.FullAddress).FirstOrDefault(),
					Name = shop.Name,
					Description = shop.Description,
					Nickname = shop.Nickname
				};
				await db.SaveChangesAsync();

				string hiddenPhone = thisCustomer.PhoneNumber.Substring(0, 4);
				for(int i = 0; i < thisCustomer.PhoneNumber.Length - 4; i++)
				{
					hiddenPhone += "*";
				}

				string hiddenEmail = thisCustomer.Email;
				string before = hiddenEmail.Split('@').First();
				string last = hiddenEmail.Split('@').Last();
				string star = "";
				for(int i = 0; i < before.Length; i++)
				{
					star += "*";
				}
				hiddenEmail = String.Join('@', star, last);

				return new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = await db.Customers.AsNoTracking().Include(x => x.Shop)
						.Where(x => x.Salt == customerSalt)
						.Select(x => new
						{
							Id = x.Id,
							Email = hiddenEmail,
							Fullname = x.FullName,
							Sex = x.Sex,
							PhoneNumber = hiddenPhone,
							Username = x.Username,
							DateOfBirth = x.DateOfBirth,
							Avatar = x.Avatar,
							CreateDate = x.CreateAt,
							Shop = new
							{
								Id = x.Shop.Id,
								CreateDate = x.Shop.CreateDate,
								ShopName = x.Shop.Name,
								Nickname = x.Shop.Nickname,
								Follower = x.Shop.Follower,
								Description = x.Shop.Description,
								Address = x.Shop.Address,
								Avatar = x.Shop.Avatar,
								ShopUri = x.Shop.ShopUri
							}
						})
						.SingleOrDefaultAsync()
				};
			}
		}

		public async Task<List<Shop>> Search(string searchString)
		{
			return await db.Shops.AsNoTracking().Where(x => x.Name.Contains(searchString) || x.Nickname.Contains(searchString)).ToListAsync();
		}
	}
}
