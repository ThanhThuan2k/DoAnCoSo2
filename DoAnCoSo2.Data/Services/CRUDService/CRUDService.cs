using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Services.CRUDService
{
	public partial class CRUDService : IDisposable
	{
		protected readonly DoAnCoSo2DbContext db;
		public CRUDService()
		{
			db = new DoAnCoSo2DbContext();
		}

		public CRUDService(DoAnCoSo2DbContext _db)
		{
			db = _db;
		}

		public DoAnCoSo2DbContext GetContext()
		{
			return db;
		}

		protected virtual void Dispose(bool dispose)
		{
			if (dispose)
			{
				db.DisposeAsync();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
