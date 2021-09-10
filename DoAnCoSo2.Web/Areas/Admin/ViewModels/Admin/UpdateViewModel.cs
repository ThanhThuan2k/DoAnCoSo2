using DoAnCoSo2.Data.Attributes.NotUpdateAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.ViewModels.Admin
{
	public class UpdateViewModel
	{
		public int Id { get; set; }
		public string Username { get; set; }

		[NotUpdate]
		public string Password { get; set; }
		public string Fullname { get; set; }
		public string Sex { get; set; }

		[NotUpdate]
		public string PhoneNumber { get; set; }

		[NotUpdate]
		public string Email { get; set; }
		public DateTime UpdateAt { get; set; }
	}
}
