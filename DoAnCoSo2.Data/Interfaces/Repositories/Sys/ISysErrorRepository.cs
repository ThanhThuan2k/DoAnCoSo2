using DoAnCoSo2.DTOs.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories
{
	public interface ISysErrorRepository
	{
		SysError Get(int code);
		string GetName(int code);
	}
}
