using DoAnCoSo2.DTOs.Auth;
using System;

namespace DoAnCoSo2.DTOs.App
{
	public class Address
	{
		public int Id { get; set; }
		public int? CustomerID { get; set; }
		public string FullAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string Receiver { get; set; }
		public bool IsDefault { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }

		public Customer Customer { get; set; }
	}
}
