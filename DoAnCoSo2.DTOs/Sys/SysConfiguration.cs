using System;

namespace DoAnCoSo2.DTOs.Sys
{
	public class SysConfiguration
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public DateTime CreateAt { get; set; }
		public DateTime? UpdateAt { get; set; }
		public DateTime? DeleteAt { get; set; }
	}
}
