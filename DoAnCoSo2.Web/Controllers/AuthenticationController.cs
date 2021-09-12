using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.Auth;
using DoAnCoSo2.Data.RequestModel.Customer;
using DoAnCoSo2.Data.Services.CRUDService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Controllers
{
	public class AuthenticationController : BaseController
	{
		private readonly ICustomerRepository ICustomerRepository;
		public AuthenticationController(IWebHostEnvironment _host, CRUDService _service, ICustomerRepository _customerRepo)
			: base(_host, _service)
		{
			ICustomerRepository = _customerRepo;
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginRequestModel model)
		{
			StandardResponse result = await ICustomerRepository.Login(model);
			if (result.IsSuccess)
			{
				string token = result.Payload.ToString();
				Response.Cookies.Append("jwt", token);
				StandardResponse newResult = new StandardResponse()
				{
					IsSuccess = true,
					Payload = new
					{
						Token = token
					},
					Error = null
				};
				return Ok(result);
			}
			else
			{
				return Ok(result);
			}
		}

		[AllowAnonymous]
		[HttpPost("logout")]
		public IActionResult Logout()
		{
			Response.Cookies.Delete("jwt");
			return Ok(new StandardResponse()
			{
				IsSuccess = true,
				Error = null,
				Payload = null
			});
		}
	}
}
