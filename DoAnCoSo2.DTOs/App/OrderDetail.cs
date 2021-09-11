using DoAnCoSo2.DTOs.Sys;
using System;

namespace DoAnCoSo2.DTOs.App
{
	public class OrderDetail
	{
		public OrderDetail()
		{
			Status = new SysOrderStatus();
			Order = new Order();
			Shop = new Shop();
			Product = new Product();
		}
		public int? OrderID { get; set; }
		public int? ShopID { get; set; }
		public int? ProductID { get; set; }
		public int Quantity { get; set; }
		public int ColorID { get; set; }
		public int? StatusID { get; set; }
		public bool Paid { get; set; }
		public DateTime PaidAt { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public DateTime? UpdateAt { get; set; }

		public SysOrderStatus Status { get; set; }
		public Order Order { get; set; }
		public Shop Shop { get; set; }
		public Product Product { get; set; }
	}
}
