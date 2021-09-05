using DoAnCoSo2.DTOs.Auth;
using DoAnCoSo2.DTOs.Sys;
using System;

namespace DoAnCoSo2.DTOs.App
{
	public class Notification
	{
		public Notification()
		{
			FromAdmin = new Admin();
			ToAdmin = new Admin();
			FromShop = new Shop();
			ToShop = new Shop();
			FromCustomer = new Customer();
			ToCustomer = new Customer();
			SysNotification = new SysNotification();
		}

		public int Id { get; set; }
		public int NotificationID { get; set; }
		public int? FromCustomerID { get; set; }
		public int? ToCustomerID { get; set; }
		public int? FromAdminID { get; set; }
		public int? ToAdminID { get; set; }
		public int? FromShopID { get; set; }
		public int? ToShopID { get; set; }
		public string NotificationHeader { get; set; }
		public string NotificationBody { get; set; }
		public bool IsRead { get; set; }
		public string Url { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime UpdateAt { get; set; }
		public DateTime DeleteAt { get; set; }
		
		public Shop FromShop { get; set; }
		public Shop ToShop { get; set; }
		public Admin FromAdmin { get; set; }
		public Admin ToAdmin { get; set; }
		public Customer FromCustomer { get; set; }
		public Customer ToCustomer { get; set; }
		public SysNotification SysNotification { get; set; }
	}
}
