using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.DTOs.App
{
	public class Category2
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public int ParentCategoryId { get; set; }

		public Category Category { get; set; }
		public ICollection<Category3> Categories3 { get; set; }
	}
}
