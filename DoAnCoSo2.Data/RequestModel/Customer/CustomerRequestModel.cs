﻿using DoAnCoSo2.Data.Attributes.NotUpdateAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.RequestModel.Customer
{
	public class CustomerRequestModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string Sex { get; set; }
		public string PhoneNumber { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime? DateOfBirth { get; set; }
	}
}
