using DoAnCoSo2.Data.ViewModels.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.Auth
{
	public class AdminViewModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string FullName { get; set; }
		public string Sex { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public bool IsBlock { get; set; }
		public string Avatar { get; set; }
		public DateTime? CreateAt { get; set; }
		public bool IsTwoFactorEnabled { get; set; }
		public DateTime LastOnlineTime { get; set; }
		public bool IsValidEmail { get; set; }
		public bool IsValidPhoneNumber { get; set; }
		public SysRolesViewModel Role { get; set; }
	}
}
