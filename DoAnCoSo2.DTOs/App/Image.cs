using System;

namespace DoAnCoSo2.DTOs.App
{
	public class Image
	{
		public Image()
		{
			Product = new Product();
			Comment = new Comment();
		}
		public int Id { get; set; }
		public string Url { get; set; }
		public int ProductID { get; set; }
		public int CommentID { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? DeleteAt { get; set; }

		public Product Product { get; set; }
		public Comment Comment { get; set; }
	}
}
