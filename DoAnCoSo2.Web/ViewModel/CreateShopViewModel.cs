using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.ViewModel
{
	public class CreateShopViewModel
	{
		public IFormFile Avatar { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Nickname { get; set; }
		public string Address { get; set; }
	}
}
