using DoAnCoSo2.Data.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Attributes.NotUpdateAttribute
{
	public class NotUpdateAttribute : Attribute
	{
		public override string ToString()
		{
			return AppConstant.NOT_UPDATE_ATTRIBUTE;
		}
	}
}
