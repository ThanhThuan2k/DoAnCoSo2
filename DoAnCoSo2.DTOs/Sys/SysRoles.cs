using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.Sys
{
	public class SysRoles
	{
		public SysRoles()
		{
			Admins = new HashSet<Admin>();
		}

		public int Id { get; set; }
		public string Role { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public DateTime? UpdateAt { get; set; }

		public ICollection<Admin> Admins { get; set; }
	}
}
