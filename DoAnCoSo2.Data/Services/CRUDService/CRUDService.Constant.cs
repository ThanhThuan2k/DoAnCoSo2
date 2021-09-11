using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Services.CRUDService
{
	public partial class CRUDService : IDisposable
	{
		public const string KEY = "Id";
		protected string[] NAMESPACE = new[] { "DoAnCoSo2" };
		protected const string NOT_DELETED_CONDITION = "DeleteAt == NULL";
		protected const string DELETE_QUERY = "DeleteAt = GETDATE()";
		protected const string RESTORE_QUERY = "DeleteAt = NULL";
	}
}
