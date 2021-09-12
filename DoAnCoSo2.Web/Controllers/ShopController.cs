using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.Services.CRUDService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Controllers
{
	public class ShopController : BaseController
	{
		private readonly IShopRepository IShopRepository;
		public ShopController(IWebHostEnvironment _host, CRUDService _service, IShopRepository _shopRepo)
			: base(_host, _service)
		{
			IShopRepository = _shopRepo;
		}

		[HttpGet("search")]
		public async Task<IActionResult> Search(string search)
		{
			return Ok(new StandardResponse()
			{
				IsSuccess = true,
				Error = null,
				Payload = await IShopRepository.Search(search)
			});
		}
	}
}
