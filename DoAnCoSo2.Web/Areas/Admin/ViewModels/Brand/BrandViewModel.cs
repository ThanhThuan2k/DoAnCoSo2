using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.ViewModels.Brand
{
	public class BrandViewModel
	{
		public string Name { get; set; }
		public IFormFile Avatar { get; set; }
	}
}
