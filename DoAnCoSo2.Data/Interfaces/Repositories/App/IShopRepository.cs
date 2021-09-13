using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.RequestModel.Shop;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories.App
{
	public interface IShopRepository
	{
		Task<Customer> GetCustomer(string salt);
		Task<Shop> GetShopByUri(string uri);
		Task<StandardResponse> CreateShop(string customerSalt, RegisterShopRequestModel shop);
		bool IsExistShop(int customerId);
		Task<List<Shop>> Search(string searchString);
		Task<StandardResponse> UploadAvatar(string salt, string path);
	}
}
