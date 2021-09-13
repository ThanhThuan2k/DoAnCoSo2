using DoAnCoSo2.Data.Services.CRUDService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Controllers
{
	[ApiController]
	[Route("api.v2/[controller]")]
	public class BaseController : Controller
	{
		protected readonly IWebHostEnvironment Host;
		protected readonly CRUDService service;
		public BaseController(IWebHostEnvironment _host, CRUDService _service)
		{
			Host = _host;
			service = _service;
		}
	}
}
