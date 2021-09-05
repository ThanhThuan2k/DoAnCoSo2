using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Sys;
using System;

namespace DoAnCoSo2.DTOs.Rel
{
	public class Product_Evaluated
	{
		public Product_Evaluated()
		{
			Product = new Product();
			Evaluated = new SysEvaluated();
		}
		public int ProductID { get; set; }
		public int EvaluatedID { get; set; }
		public int Quantity { get; set; }
		
		public Product Product { get; set; }
		public SysEvaluated Evaluated { get; set; }
	}
}
