using DoAnCoSo2.DTOs.App;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.Sys
{
	public class SysOrderStatus
	{
		public SysOrderStatus()
		{
			OrderDetails = new HashSet<OrderDetail>();
		}

		public int Id { get; set; }
		public string Status { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
