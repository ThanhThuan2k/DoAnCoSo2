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
	public class OrderController : AdminControllerBase
	{
		private readonly IOrderRepository IOrderRepository;
		public OrderController(IWebHostEnvironment _host, IOrderRepository _orderRepo)
			: base(_host)
		{
			IOrderRepository = _orderRepo;
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin, SysAdmin")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await IOrderRepository.GetAll());
		}
	}
}
