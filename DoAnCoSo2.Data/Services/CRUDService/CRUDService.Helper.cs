using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.Data.Extensions.StringExtension;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Collections;

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

		public virtual string GetSelectedFields<TObject>(string prefix = "")
		{
			string query = "";
			if (prefix.IsNullOrEmpty())
			{
				query = "new {";
			}
			var type = typeof(TObject);

			bool isNotUpdate = false;
			foreach (var item in type.GetProperties())
			{
				var attrs = item.GetCustomAttributes(true);
				foreach (var at in attrs)
				{
					if (at.ToString() == AppConstant.NOT_UPDATE_ATTRIBUTE) isNotUpdate = true;
				}
				if (isNotUpdate)
				{
					isNotUpdate = !isNotUpdate;
					continue;
				}

				if (item.PropertyType.IsGenericType)
				{
					var _type = item.PropertyType.GetGenericTypeDefinition();
					if (_type == typeof(IEnumerable<>) || _type == typeof(ICollection<>))
					{
						var dPropType = type.GetProperty(item.Name).PropertyType.GetGenericArguments().Single();
						MethodInfo method = typeof(CRUDService).GetMethod("GetSelectedFields");
						MethodInfo generic = method.MakeGenericMethod(dPropType);
						query += $"{item.Name}.Select({generic.Invoke(this, new object[] { "" })}) as {item.Name},";
						continue;
					}
				}

				if (NAMESPACE.Any(x => item.PropertyType.FullName.IndexOf(x) == 0))
				{
					var dPropType = type.GetProperty(item.Name).PropertyType;
					MethodInfo method = typeof(CRUDService).GetMethod("GetSelectedFields");
					MethodInfo generic = method.MakeGenericMethod(dPropType);
					query += generic.Invoke(this, new[] { $"{item.Name}" });
					continue;
				}
				var column = prefix.IsNullOrEmpty() ? item.Name : $"{prefix}.{item.Name} as _{prefix}_{item.Name}";
				query += column + ",";
			}

			if (prefix.IsNullOrEmpty())
			{
				if (query[^1] == ',') query = query.Remove(query.Length - 1, 1);
				query += "}";
			}
			return query;
		}

		public virtual async Task<ICollection<object>> GetAllTable<TTable, TModel>(Expression<Func<TTable, bool>> condition)
			where TTable : class where TModel : class
		{
			return await db.Set<TTable>()
				.Where(condition)
				.Select(this.GetSelectedFields<TModel>())
				.ToDynamicListAsync();
		}

		public virtual TModel CastTo<TModel>(object data, bool replaceWithNull = true) where TModel : class
		{
			if (data == null) return null;
			var sType = data.GetType();
			var dType = typeof(TModel);

			var dObject = Activator.CreateInstance<TModel>();

			bool isNotUpdate = false;
			foreach (var item in sType.GetProperties())
			{
				var attrs = item.GetCustomAttributes(true);

				foreach (var at in attrs)
				{
					if (at.ToString() == AppConstant.NOT_UPDATE_ATTRIBUTE) isNotUpdate = true;
				}
				if (isNotUpdate)
				{
					isNotUpdate = !isNotUpdate;
					continue;
				}
				object value = null;
				try
				{
					value = item.GetValue(data, null);
				}
				catch
				{
					continue;
				}
				if (value == null && !replaceWithNull)
				{
					continue;
				}

				if (item.PropertyType.IsGenericType)
				{
					var type = item.PropertyType.GetGenericTypeDefinition();
					if (type == typeof(ICollection<>) || type == typeof(IEnumerable<>))
					{
						if ((int)value.GetType().GetProperty("Count").GetValue(value) > 0)
						{
							var dPropType = dType.GetProperty(item.Name).PropertyType.GetGenericArguments().Single();
							MethodInfo method = typeof(CRUDService).GetMethod("CastToListWithEnum");
							MethodInfo generic = method.MakeGenericMethod(dPropType);
							dType.GetProperty(item.Name)?.SetValue(dObject, generic.Invoke(this, new[] { value }));
						}
						continue;
					}
				}

				dType.GetProperty(item.Name)?.SetValue(dObject, value);
			}

			var joinedColumn = sType.GetProperties()
				.Where(x => x.Name.StartsWith("_"))
				.GroupBy(y => y.Name.Split('_')[1]);
			foreach (var list in joinedColumn)
			{
				object prop = null;
				foreach (var item in list)
				{
					var propertyNames = item.Name.Remove(0, 1).Split('_');
					if (prop == null) prop = Activator.CreateInstance(dType.GetProperty(propertyNames[0]).PropertyType);
					prop.GetType().GetProperty(propertyNames[1]).SetValue(prop, sType.GetProperty(item.Name).GetValue(data));
				}
				dType.GetProperty(list.Key)?.SetValue(dObject, prop);
			}
			return dObject;
		}

		public virtual IEnumerable CastToListWithEnum<TModel>(IEnumerable list) where TModel : class
		{
			var result = new List<TModel>();
			foreach (var item in list)
			{
				result.Add(CastTo<TModel>(item));
			}
			return result;
		}
	}
}
