using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.ViewModels.Admin
{
	public class SignUpViewModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Fullname { get; set; }
		public string Sex { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
	}
}
