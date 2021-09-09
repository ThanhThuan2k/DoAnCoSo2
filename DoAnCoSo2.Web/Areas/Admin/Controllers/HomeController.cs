using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	public class HomeController : AdminControllerBase
	{
		public HomeController(IWebHostEnvironment _host) : base(_host) { }

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(true);
		}
	}
}
