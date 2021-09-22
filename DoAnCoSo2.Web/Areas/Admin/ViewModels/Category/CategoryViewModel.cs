using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.ViewModels.Category
{
	public class CategoryViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IFormFile Avatar { get; set; }
	}
}
