using DoAnCoSo2.Data.Common;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using DoAnCoSo2.DTOs.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories.App
{
	public interface IOrderRepository
	{
		Task<StandardResponse> Order(string customerSalt);
		Task<Customer> GetCustomer(string customerSalt);
		Task<StandardResponse> Create(Cart newCart);
		Task<List<Cart>> GetAllCartAsync(int customerId);
		SysOrderStatus GetOrderStatus(int statusId);
		Task<StandardResponse> Create(Order newOrder);
		Task<Product> GetProduct(int productId);
		Task<StandardResponse> GetCart(string customerSalt);
	}
}
