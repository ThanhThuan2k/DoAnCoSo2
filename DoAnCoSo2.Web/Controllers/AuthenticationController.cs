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
			return Ok(await ICustomerRepository.Login(model));
		}
	}
}
