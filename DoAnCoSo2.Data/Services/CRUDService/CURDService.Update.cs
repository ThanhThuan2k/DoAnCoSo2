using DoAnCoSo2.Data.Common;
using Microsoft.Data.SqlClient;
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
		public virtual async Task<StandardResponse> UpdateAsync<TTable, TModel>(TModel model)
			where TTable : class where TModel : class
		{
			var keyUpdate = model.GetType().GetProperty(KEY)?.GetValue(model).ToString();
			if (!String.IsNullOrEmpty(keyUpdate))
			{
				try
				{
					var query = "UPDATE " + this.GetTableName<TTable>() +
						" SET " + GetUpdatedFields<TModel>() +
						" WHERE " + KEY + "=" + keyUpdate;

					List<SqlParameter> GetSqlParams()
					{
						var listParams = this.GetFieldsAsList<TModel>();
						var sqlParams = new List<SqlParameter>();
						foreach (var item in listParams)
						{
							var param = new SqlParameter();
							var paramValue = model.GetType().GetProperty(item)?.GetValue(model);
							param.ParameterName = item;
							param.Value = paramValue ?? DBNull.Value;
							sqlParams.Add(param);
						}
						return sqlParams;
					}
					await db.Database.ExecuteSqlRawAsync(query, GetSqlParams());
					return new StandardResponse()
					{
						IsSuccess = true,
						Error = null,
						Payload = model
					};
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = model,
						Error = new StandardError()
						{
							ErrorCode = 1124,
							ErrorMessage = db.SysErrors.Where(x => x.ErrorCode == 1124).Select(x => x.ErrorName).SingleOrDefault()
						}
					};
				}
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = model,
					Error = new StandardError()
					{
						ErrorCode = 1125,
						ErrorMessage = db.SysErrors.Where(x => x.ErrorCode == 1125).Select(x => x.ErrorName).SingleOrDefault()
					}
				};
			}
		}

		public virtual async Task<bool> RestoreAsync<TTable>(int id) where TTable : class
		{
			try
			{
				var query = "UPDATE " + this.GetTableName<TTable>()
							+ " SET " + RESTORE_QUERY
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
