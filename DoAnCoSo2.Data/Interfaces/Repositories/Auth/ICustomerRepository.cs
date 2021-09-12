using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.RequestModel.Customer;
using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories.Auth
{
	public interface ICustomerRepository
	{
		Task<StandardResponse> GetAll();
		Task<StandardResponse> Get(int id);
		bool IsExist(int id);
		bool IsActive(int id);
		Task<StandardResponse> Create(CustomerRequestModel model);
		Task<StandardResponse> Update(CustomerRequestModel model);
		Task<StandardResponse> Delete(int id);
		Customer GetDeleted(int id);
		bool IsExistEmail(string email);
		bool IsExistPhoneNumber(string phone);
		bool IsExistSalt(string salt);
		Task<StandardResponse> Restore(int id);
		Task<StandardResponse> Login(LoginRequestModel model);
		Customer GetByPhoneNumber(string phone);
	}
}
