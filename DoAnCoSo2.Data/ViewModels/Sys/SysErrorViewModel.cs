using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.Sys
{
	public class SysErrorViewModel
	{
		public int ErrorCode { get; set; }
		public string ErrorName { get; set; }
		public DateTime CreateAt { get; set; }
	}
}
