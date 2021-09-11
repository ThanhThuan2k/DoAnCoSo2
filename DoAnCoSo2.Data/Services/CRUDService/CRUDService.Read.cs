using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo2.Data.Services.CRUDService
{
	public partial class CRUDService : IDisposable
	{
		public virtual async Task<TModel> Get<TTable, TModel>(int id)
					where TModel : class where TTable : class
		{
			dynamic data = new object();
			await Task.Run(() =>
			{
				data = db.Set<TTable>()
					.AsNoTracking()
					.Where(KEY + " == " + id + " && " + NOT_DELETED_CONDITION)
					.Select(this.GetSelectedFields<TModel>())
					.SingleOrDefault();
			});

			return CastTo<TModel>(data);
		}
	}
}
