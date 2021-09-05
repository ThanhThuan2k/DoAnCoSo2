using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Rel;
using System;
using System.Collections.Generic;

namespace DoAnCoSo2.DTOs.Sys
{
	public class SysEvaluated
	{
		public int Id { get; set; }
		public int Value { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public ICollection<Product_Evaluated> Products { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}
