using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.Auth;
using DoAnCoSo2.Data.ViewModels.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Shop.Controllers
{
	public class AuthenticationController : ShopControllerBase
	{
		private readonly ICustomerRepository ICustomerRepository;
		public AuthenticationController(IWebHostEnvironment _host, ICustomerRepository _customerRepo)
			: base(_host)
		{
			ICustomerRepository = _customerRepo;
		}

		[HttpPost("email-authentication")]
		public async Task<IActionResult> EmailAuthentication(string email)
		{
			return Ok(await ICustomerRepository.EmailAuthentication(email));
		}

		[HttpPost("seller-login")]
		public async Task<IActionResult> SellerLogin(SellerLoginViewModel model)
		{
			return Ok(await ICustomerRepository.Login(model));
		}
	}
}
