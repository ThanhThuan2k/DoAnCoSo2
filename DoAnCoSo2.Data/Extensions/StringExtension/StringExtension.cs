using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Extensions.StringExtension
{
	public static class StringExtension
	{
		public static bool IsNullOrEmpty(this string text)
		{
			return String.IsNullOrEmpty(text);
		}
	}
}
