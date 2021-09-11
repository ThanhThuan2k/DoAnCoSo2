using DoAnCoSo2.DTOs.Rel;
using DoAnCoSo2.DTOs.Sys;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.App
{
	public class Product
	{
		public Product()
		{
			Shop = new Shop();
			Category = new Category();
			Brand = new Brand();
			Images = new HashSet<Image>();
			Evaluated = new HashSet<Product_Evaluated>();
			Comments = new HashSet<Comment>();
			OrderDetails = new HashSet<OrderDetail>();
		}
		public int Id { get; set; }
		public string ProductName { get; set; }
		public int? CategoryID { get; set; }
		public int? BrandID { get; set; }
		public float? Price { get; set; }
		public float? MinimumPrice { get; set; }
		public float? MaximumPrice { get; set; }
		public int? TotalEvaluated { get; set; }
		public int Like { get; set; }
		public int? TotalSold { get; set; }
		public int QuantityOfInventory { get; set; }
		public string Material { get; set; }
		public int ShopID { get; set; }
		public string Description { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }

		public Shop Shop { get; set; }
		public Brand Brand { get; set; }
		public Category Category { get; set; }
		public ICollection<Image> Images { get; set; }
		public ICollection<Product_Evaluated> Evaluated { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
