using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Configuration.App
{
	public class Customer_Shop_Message
	{
		public Customer_Shop_Message()
		{
			Customer = new Customer();
			Shop = new Shop();
		}
		public int Id { get; set; }
		public string MessageContent { get; set; }
		public DateTime SendTime { get; set; }
		public bool? IsSeen { get; set; }
		public int CustomerID { get; set; }
		public int ShopID { get; set; }

		public Customer Customer { get; set; }
		public Shop Shop { get; set; }
	}
}
