﻿using DoAnCoSo2.Data.Configuration.App;
using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.App
{
	public class Shop
	{
		public Shop()
		{
			NotificationsSent = new HashSet<Notification>();
			NotificationsReceived = new HashSet<Notification>();
			Products = new HashSet<Product>();
			OrderDetails = new HashSet<OrderDetail>();
			Messages_Shop = new HashSet<Customer_Shop_Message>();
		}

		public int Id { get; set; }
		public int? OwnerID { get; set; }
		public DateTime CreateDate { get; set; }
		public string Address { get; set; }
		public string Avatar { get; set; }
		public string ShopUri { get; set; }
		public string Name { get; set; }
		public int? Follower { get; set; }
		public string Description { get; set; }
		public DateTime? LastOnline { get; set; }
		public string Nickname { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }

		public Customer Customer { get; set; }
		public ICollection<Product> Products { get; set; }
		public ICollection<Notification> NotificationsSent { get; set; }
		public ICollection<Notification> NotificationsReceived { get; set; }
		public ICollection<OrderDetail> OrderDetails { get; set; }
		public ICollection<Customer_Shop_Message> Messages_Shop { get; set; }
	}
}
