using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.App
{
	public class Brand
	{
		public Brand()
		{
			Products = new HashSet<Product>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime UpdateAt { get; set; }
		public DateTime DeleteAt { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
