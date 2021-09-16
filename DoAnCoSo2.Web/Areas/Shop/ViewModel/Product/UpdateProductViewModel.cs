using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Shop.ViewModel.Product
{
	public class UpdateProductViewModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public int? CategoryID { get; set; }
		public int? BrandID { get; set; }
		public float? Price { get; set; }
		public float? MinimumPrice { get; set; }
		public float? MaximumPrice { get; set; }
		public IFormFile Avatar { get; set; }
		public int QuantityOfInventory { get; set; }
		public string Material { get; set; }
		public string Description { get; set; }
		public ICollection<IFormFile> Images { get; set; }
	}
}
