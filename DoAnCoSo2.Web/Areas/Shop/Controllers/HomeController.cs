using DoAnCoSo2.Data.Interfaces.Repositories.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Shop.Controllers
{
	public class HomeController : ShopControllerBase
	{
		public HomeController(IWebHostEnvironment _host, IShopRepository _shopRepo)
		: base(_host, _shopRepo) {}

		[HttpGet("get")]
		public async Task<IActionResult> Get()
		{
			return Ok(true);
		}
	}
}
