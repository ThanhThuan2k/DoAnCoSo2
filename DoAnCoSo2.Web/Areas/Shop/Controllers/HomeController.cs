using DoAnCoSo2.Data.Interfaces.Repositories.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Shop.Controllers
{
	public class HomeController : ShopControllerBase
	{
		public HomeController(IWebHostEnvironment _host, IShopRepository _shopRepo)
		: base(_host, _shopRepo) {}

		[HttpGet("get")]
		[Authorize(Roles = "ShopAdmin")]
		public async Task<IActionResult> Get()
		{
			var currentCustomer = HttpContext.User.Identity as ClaimsIdentity;
			if(currentCustomer != null)
			{
				string shopUri = currentCustomer.FindFirst("shopuri").Value;
			}
			return Ok(true);
		}
	}
}
