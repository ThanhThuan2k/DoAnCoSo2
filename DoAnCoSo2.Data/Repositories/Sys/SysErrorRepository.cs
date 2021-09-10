using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.DTOs.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Repositories.Sys
{
	public class SysErrorRepository : ISysErrorRepository
	{
		private readonly DoAnCoSo2DbContext db;
		public SysErrorRepository(DoAnCoSo2DbContext _db)
		{
			db = _db;
		}

		public SysError Get(int code)
		{
			return db.SysErrors.Single(x => x.ErrorCode == code);
		}

		public string GetName(int code)
		{
			if (db.SysErrors.Any(x => x.ErrorCode == code))
			{
				return db.SysErrors.Where(x => x.ErrorCode == code).Select(x => x.ErrorName).SingleOrDefault();
			}
			else
			{
				return "Mã lỗi không xác định";
			}
		}
	}
}
