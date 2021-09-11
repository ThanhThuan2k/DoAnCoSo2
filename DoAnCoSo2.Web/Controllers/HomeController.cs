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
	public class HomeController : BaseController
	{
		private readonly ICustomerRepository ICustomerRepository;
		public HomeController(IWebHostEnvironment _host, CRUDService _service, ICustomerRepository _customerRepo)
			: base(_host, _service)
		{
			ICustomerRepository = _customerRepo;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register(CustomerRequestModel model)
		{
			return Ok(await ICustomerRepository.Create(model));
		}

		[HttpPut("update")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Update(CustomerRequestModel model)
		{
			return Ok(await ICustomerRepository.Update(model));
		}

		[HttpDelete("delete/{id}")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok(await ICustomerRepository.Delete(id));
		}
	}
}
