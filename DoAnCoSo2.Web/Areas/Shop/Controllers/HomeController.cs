using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.RequestModel.Shop;
using DoAnCoSo2.Data.Services.CRUDService;

namespace DoAnCoSo2.Web.Areas.Shop.Controllers
{
	public class HomeController : ShopControllerBase
	{
		private IShopRepository IShopRepository;
		private CRUDService Service;
		public HomeController(IWebHostEnvironment _host, IShopRepository _shopRepo, CRUDService service)
		: base(_host)
		{
			IShopRepository = _shopRepo;
			Service = service;
		}

		[HttpGet("get")]
		[Authorize(Roles = "ShopAdmin")]
		public async Task<IActionResult> Get()
		{
			var currentCustomer = HttpContext.User.Identity as ClaimsIdentity;
			if (currentCustomer != null)
			{
				string shopUri = currentCustomer.FindFirst("shopuri").Value;
				var shop = await IShopRepository.GetShopByUri(shopUri);
				return Ok(new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = Service.CastTo<ShopViewModel>(shop)
				});
			}
			return Ok(new StandardResponse()
			{
				IsSuccess = false,
				Error = null,
				Payload = null
			});
		}

		[HttpPost("login")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Login()
		{
			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string salt = currentUser.FindFirst("salt").Value;
			Customer customer = await IShopRepository.GetCustomer(salt);
			string token = JwtService.General(customer, "ShopAdmin");
			Response.Cookies.Append("jwt", token);
			return Ok(new StandardResponse()
			{
				IsSuccess = true,
				Error = null,
				Payload = new
				{
					Shop = Service.CastTo<ShopViewModel>(customer.Shop),
					Token = token
				}
			});
		}

		[HttpPost("register")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Register(RegisterShopRequestModel shop)
		{
			var currentCustomer = HttpContext.User.Identity as ClaimsIdentity;
			string salt = currentCustomer.FindFirst("salt").Value;
			var createResult = await IShopRepository.CreateShop(salt, shop);
			return Ok(createResult);
		}
	}
}
