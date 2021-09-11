using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.App
{
	public class AddressViewModel
	{
		public int Id { get; set; }
		public string FullAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string Receiver { get; set; }
		public bool IsDefault { get; set; }
		public DateTime CreateAt { get; set; }
	}
}
