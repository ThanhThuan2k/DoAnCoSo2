using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.App
{
	public class ProductViewModel
	{	
		public int Id { get; set; }
		public string ProductName { get; set; }
		public float? Price { get; set; }
		public float? MinimumPrice { get; set; }
		public float? MaximumPrice { get; set; }
		public int? TotalEvaluated { get; set; }
		public int Like { get; set; }
		public int? TotalSold { get; set; }
		public int QuantityOfInventory { get; set; }
		public string Material { get; set; }
		public string Description { get; set; }
		public DateTime CreateAt { get; set; }
		public BrandViewModel Brand { get; set; }
		public CategoryViewModel Category { get; set; }
		public ICollection<ImageViewModel> Images { get; set; }
	}
}
