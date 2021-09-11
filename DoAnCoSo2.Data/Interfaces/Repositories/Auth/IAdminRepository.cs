using DoAnCoSo2.Data.Common;
using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories
{
	public interface IAdminRepository
	{
		Admin GetAdminByEmail(string email);
		bool IsExistEmail(string email);
		Task LoginFailPassword(string email);
		Task ResetAccessFailCount(string email);
		Task<bool> CreateAccount(string username, string hashedPassword, string fullName, string sex, string phoneNumber, string email, string salt);
		bool IsExistSalt(string salt);
		bool IsExistPhoneNumber(string phoneNumber);
		Task<object> Get(int id);
		Task<StandardResponse> Delete(int id);
		bool IsExist(int id);
		bool IsActive(int id);
		Admin GetById(int id);
		Admin GetDeleted(int id);
		bool IsBlock(int id);
		Task<StandardResponse> Unblock(int id);
		Admin GetBlocked(int id);
		Task<StandardResponse> Block(int id);
		Task<StandardResponse> RestoreDeleted(int id);
		Task<StandardResponse> GetAll();
		Task<StandardResponse> BlockYourself(string salt);
	}
}
