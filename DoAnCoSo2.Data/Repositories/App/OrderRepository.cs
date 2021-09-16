using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.Repositories.Sys;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Repositories.App
{
	public class OrderRepository : IOrderRepository
	{
		private readonly DoAnCoSo2DbContext db;
		public OrderRepository()
		{
			db = new DoAnCoSo2DbContext();
		}

		public async Task<StandardResponse> Order(string customerSalt)
		{
			Customer thisCustomer = await GetCustomer(customerSalt);
			if (thisCustomer != null)
			{

				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = null,
					Error = null
				};
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Error = new StandardError
					{
						ErrorCode = 1404,
						ErrorMessage = db.SysErrors.Where(x => x.ErrorCode == 1404).Select(x => x.ErrorName).SingleOrDefault()
					}
				};
			}
		}

		public async Task<Customer> GetCustomer(string salt)
		{
			return await db.Customers.SingleOrDefaultAsync(x => x.Salt == salt && x.DeleteAt == null);
		}

		public async Task<Product> GetProduct(int productId)
		{
			return await db.Products.Include(x => x.Shop).SingleOrDefaultAsync(x => x.Id == productId && x.DeleteAt == null);
		}

		public async Task<StandardResponse> Create(Cart newCart)
		{
			try
			{
				await db.Carts.AddAsync(newCart);
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = null
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = newCart,
					Error = new StandardError()
					{
						ErrorCode = 0000,
						ErrorMessage = ex.Message
					}
				};
			}
		}

		public async Task<List<Cart>> GetAllCartAsync(int customerId)
		{
			return await db.Carts
				.Include(x => x.Product)
				.ThenInclude(x => x.Shop)
				.Where(x => x.CustomerId == customerId && x.DeleteAt == null)
				.ToListAsync();
		}

		public SysOrderStatus GetOrderStatus(int id)
		{
			return db.SysOrderStatus.SingleOrDefault(x => x.Id == id && x.DeleteAt == null);
		}

		public async Task<StandardResponse> Create(Order newOrder)
		{
			try
			{
				await db.Order.AddAsync(newOrder);
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = newOrder,
					Error = null
				};
			}
			catch (Exception ex)
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = newOrder,
					Error = new StandardError()
					{
						ErrorCode = 0000,
						ErrorMessage = ex.Message
					}
				};
			}
		}

		public async Task<StandardResponse> GetCart(string customerSalt)
		{
			return new StandardResponse()
			{
				IsSuccess = true,
				Payload = await db.Customers.AsNoTracking()
				.Where(x => x.Salt == customerSalt)
				.Include(x => x.Carts)
				.ThenInclude(x => x.Product)
				.Select(x => x.Carts)
				.SingleOrDefaultAsync(),
				Error = null
			};
		}
	}
}
