using DoAnCoSo2.DTOs.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.DTOs.App
{
	public class Comment
	{
		public Comment()
		{
			RootComment = new Comment();
			ReplyComments = new HashSet<Comment>();
			Images = new HashSet<Image>();
		}

		public int Id { get; set; }
		public int CustomerID { get; set; }
		public string Content { get; set; }
		public int? SysEvaluated { get; set; }
		public DateTime PostAt { get; set; }
		public bool IsHidden { get; set; }
		public int? ProductID { get; set; }
		public int Like { get; set; }
		public int Unlike { get; set; }
		public int EvaluatedID { get; set; }
		public int? ReplyCommentID { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime UpdateAt { get; set; }
		public DateTime DeleteAt { get; set; }

		public Comment RootComment { get; set; }
		public ICollection<Comment> ReplyComments { get; set; }
		public SysEvaluated Evaluated { get; set; }
		public Product Product { get; set; }
		public ICollection<Image> Images { get; set; }
	}
}
