using DoAnCoSo2.Data.ViewModels.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.Auth
{
	public class CustomerViewModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string Sex { get; set; }
		public string PhoneNumber { get; set; }
		public string Username { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string Avatar { get; set; }
		public DateTime? CreateAt { get; set; }
		public ShopViewModel Shop { get; set; }
		public ICollection<AddressViewModel> Addresses { get; set; }
	}
}
