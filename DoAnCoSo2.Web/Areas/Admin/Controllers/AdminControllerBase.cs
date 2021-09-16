using DoAnCoSo2.Data.Constant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	[Route("api.v1/[controller]")]
	[Area(AppConstant.ADMIN_AREA_NAME)]
	[ApiController]
	public class AdminControllerBase : Controller
	{
		protected readonly IWebHostEnvironment Host;

		protected AdminControllerBase(IWebHostEnvironment _host)
		{
			Host = _host;
		}
	}
}
