using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
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
	public class ProductController : ShopControllerBase
	{
		private readonly IProductRepository IProductRepository;
		private readonly ISysErrorRepository ISysErrorRepository;
		public ProductController(IWebHostEnvironment _host, IProductRepository _productRepo, ISysErrorRepository _errorRepo)
			: base(_host)
		{
			IProductRepository = _productRepo;
			ISysErrorRepository = _errorRepo;
		}

		[HttpGet("all")]
		[Authorize(Roles = "ShopAdmin")]
		public async Task<IActionResult> GetAll()
		{
			var currentShopAdmin = HttpContext.User.Identity as ClaimsIdentity;
			string shopUri = currentShopAdmin.FindFirst("shopuri").Value;
			if(shopUri != null)
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = await IProductRepository.GetAll(shopUri)
				});
			}
			else
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1504,
						ErrorMessage = ISysErrorRepository.GetName(1504)
					}
				});
			}
		}
	}
}
