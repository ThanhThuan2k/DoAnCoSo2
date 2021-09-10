using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo2.Data.Constant;

namespace DoAnCoSo2.Data.Services.CRUDService
{
	public partial class CRUDService : IDisposable
	{
		protected virtual string GetTableName<TTable>() where TTable : class
		{
			Type entityType = typeof(TTable);
			var modelType = Microsoft.EntityFrameworkCore.ModelExtensions.FindEntityType(db.Model, entityType);
			string tableName = modelType.GetTableName();
			if (String.IsNullOrEmpty(tableName))
			{
				throw new Exception($"Class {entityType.Name} không mapping với bảng nào trong database");
			}
			return tableName;
		}

		protected virtual string GetUpdatedFields<TObject>()
		{
			string query = "";
			var type = typeof(TObject);
			bool isNotUpdate = false;
			foreach(var item in type.GetProperties())
			{
				var attributes = item.GetCustomAttributes(true);
				if (item.Name == KEY)
					continue; // ko update khóa
				foreach(var attr in attributes)
				{
					if(attr.ToString() == AppConstant.NOT_UPDATE_ATTRIBUTE)
					{
						isNotUpdate = true;
					}
				}
				if (isNotUpdate)
				{
					isNotUpdate = !isNotUpdate;
					continue;
				}
				query += item.Name + "=@" + item.Name + ",";
			}
			return query.Remove(query.Length - 1, 1);
		}

		protected virtual List<string> GetFieldsAsList<TObject>()
		{
			List<string> properties = new List<string>();
			var type = typeof(TObject);

			bool isNotUpdate = false;
			foreach(var item in type.GetProperties())
			{
				var attributes = item.GetCustomAttributes(true);
				if (item.Name == KEY)
					continue; // ko update khóa
				foreach(var attr in attributes)
				{
					if(attr.ToString() == AppConstant.NOT_UPDATE_ATTRIBUTE)
					{
						isNotUpdate = true;
					}
				}
				if (isNotUpdate)
				{
					isNotUpdate = !isNotUpdate;
					continue;
				}
				properties.Add(item.Name);
			}
			return properties;
		}
	}
}
