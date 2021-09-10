using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.ViewModels.Sys
{
	public class SysRolesViewModel
	{
		public int Id { get; set; }
		public string Role { get; set; }
		public int RoleCode { get; set; }
		public DateTime? CreateAt { get; set; }
	}
}
