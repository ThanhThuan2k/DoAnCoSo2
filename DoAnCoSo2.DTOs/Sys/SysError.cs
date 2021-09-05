using System;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo2.DTOs.Sys
{
	public class SysError
	{
		[Key]
		public int Id { get; set; }
		public string ErrorName { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public DateTime? UpdateAt { get; set; }
	}
}
