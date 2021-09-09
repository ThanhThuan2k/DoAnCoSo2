﻿using DoAnCoSo2.Data.Constant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	[Route("api/[area]/[controller]")]
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