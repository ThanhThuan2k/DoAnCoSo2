using DoAnCoSo2.Data.Interfaces.Repositories.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Shop.Controllers
{
	public class CategoryController : ShopControllerBase
	{
		private readonly ICategoryRepository ICategoryRepository;
		public CategoryController(IWebHostEnvironment host, ICategoryRepository categoryRepo) 
			: base(host)
		{
			ICategoryRepository = categoryRepo;
		}

		[HttpGet("all")]
		[Authorize(Roles = "SysAdmin, ShopAdmin, Admin")]
		public async Task<IActionResult> All()
		{
			return Ok(await ICategoryRepository.GetAll());
		}
	}
}
