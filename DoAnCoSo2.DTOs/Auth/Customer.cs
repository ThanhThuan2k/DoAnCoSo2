using DoAnCoSo2.Data.Configuration.App;
using DoAnCoSo2.DTOs.App;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.Auth
{
	public class Customer
	{
		public Customer()
		{
			Addresses = new HashSet<Address>();
			NotificationsSent = new HashSet<Notification>();
			NotificationsReceived = new HashSet<Notification>();
			Orders = new HashSet<Order>();
			Messages_Shop = new HashSet<Customer_Shop_Message>();
			Messages_Admin = new HashSet<Customer_Admin_Message>();
			Carts = new HashSet<Cart>();
		}

		public int Id { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string Sex { get; set; }
		public string PhoneNumber { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string Avatar { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }

		public Shop Shop { get; set; }
		public ICollection<Address> Addresses { get; set; }
		public ICollection<Notification> NotificationsSent { get; set; }
		public ICollection<Notification> NotificationsReceived { get; set; }
		public ICollection<Order> Orders { get; set; }
		public ICollection<Customer_Shop_Message> Messages_Shop { get; set; }
		public ICollection<Customer_Admin_Message> Messages_Admin { get; set; }
		public ICollection<Cart> Carts { get; set; }
	}
}
