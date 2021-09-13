using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.App
{
	public class CommentViewModel
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime PostAt { get; set; }
		public bool IsHidden { get; set; }
		public int Like { get; set; }
		public int Unlike { get; set; }
	}
}
