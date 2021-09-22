using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.App
{
	public class ShopViewModel
	{
		public int Id { get; set; }
		public DateTime CreateDate { get; set; }
		public string Address { get; set; }
		public string Avatar { get; set; }
		public string ShopUri { get; set; }
		public string Name { get; set; }
		public int? Follower { get; set; }
		public string Description { get; set; }
		public DateTime? LastOnline { get; set; }
		public string Nickname { get; set; }
	}
}
