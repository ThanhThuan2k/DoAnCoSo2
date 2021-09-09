using DoAnCoSo2.Data.Configuration.App;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Sys;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.Auth
{
	public class Admin
	{
		public Admin()
		{
			Role = new SysRoles();
			NotificationsSent = new HashSet<Notification>();
			NotificationsReceived = new HashSet<Notification>();
		}

		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
		public string Sex { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public bool IsBlock { get; set; }
		public int? RoleID { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public bool IsTwoFactorEnabled { get; set; }
		public DateTime LastOnlineTime { get; set; }
		public int AccessFailCount { get; set; }

		public SysRoles Role { get; set; }
		public ICollection<Notification> NotificationsSent { get; set; }
		public ICollection<Notification> NotificationsReceived { get; set; }
		public ICollection<Customer_Admin_Message> Messages_Customer { get; set; }
	}
}
