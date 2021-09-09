using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Common
{
	public class StandardResponse
	{
		public bool IsSuccess { get; set; }
		public StandardError Error { get; set; }
		public object Payload { get; set; }
	}

	public class StandardError
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
