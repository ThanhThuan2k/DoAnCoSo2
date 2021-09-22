using DoAnCoSo2.Data.Interfaces.Repositories.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	public class ShopController : AdminControllerBase
	{
		private readonly IShopRepository IShopRepository;
		public ShopController(IWebHostEnvironment _host, IShopRepository _shopRepo)
			: base(_host)
		{
			IShopRepository = _shopRepo;
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin, SysAdmin")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await IShopRepository.GetAll());
		}
	}
}
