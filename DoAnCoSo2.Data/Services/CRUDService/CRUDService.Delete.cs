using DoAnCoSo2.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Services.CRUDService
{
	public partial class CRUDService : IDisposable
	{
		public virtual async Task<bool> DeleteAsync<TTable>(int id) where TTable : class
		{
			try
			{
				var query = "UPDATE " + this.GetTableName<TTable>()
							+ " SET " + DELETE_QUERY
							+ " WHERE " + KEY + "=" + id;

				await db.Database.ExecuteSqlRawAsync(query);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
	}
}
