using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Configuration.App
{
	public class Customer_Admin_Message
	{
		public int Id { get; set; }
		public string MessageContent { get; set; }
		public DateTime SendTime { get; set; }
		public bool? IsSeen { get; set; }
		public int CustomerID { get; set; }
		public int AdminID { get; set; }

		public Customer Customer { get; set; }
		public Admin Admin { get; set; }
	}
}
