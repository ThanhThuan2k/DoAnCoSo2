using DoAnCoSo2.Data.Constant;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Shop.Controllers
{
	[ApiController]
	[Area(AppConstant.SHOP_AREA_NAME)]
	[Route("api/[area]/[controller]")]
	public class ShopControllerBase : Controller
	{
		private IWebHostEnvironment Host;
		private IShopRepository IShopRepository;

		public ShopControllerBase(IWebHostEnvironment _host, IShopRepository _shopRepo)
		{
			Host = _host;
			IShopRepository = _shopRepo;
		}
	}
}
