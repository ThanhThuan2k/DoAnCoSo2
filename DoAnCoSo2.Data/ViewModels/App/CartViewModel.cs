using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.App
{
	public class CartViewModel
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public string Color { get; set; }
		public string Classification { get; set; }
	}
}
