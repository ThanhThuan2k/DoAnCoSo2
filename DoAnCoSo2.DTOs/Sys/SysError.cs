using System;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo2.DTOs.Sys
{
	public class SysError
	{
		public int Id { get; set; }
		public int ErrorCode { get; set; }
		public string ErrorName { get; set; }
		public DateTime? CreateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
		public DateTime? UpdateAt { get; set; }
	}
}
