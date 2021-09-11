using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.App
{
	public class Order
	{
		public Order()
		{
			Customer = new Customer();
			OrderDetails = new HashSet<OrderDetail>();
		}
		public int Id { get; set; }
		public int CustomerID { get; set; }
		public DateTime OrderTime { get; set; }
		public DateTime? DeleteAt { get; set; }
		public DateTime? UpdateAt { get; set; }

		public Customer Customer { get; set; }
		public ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
