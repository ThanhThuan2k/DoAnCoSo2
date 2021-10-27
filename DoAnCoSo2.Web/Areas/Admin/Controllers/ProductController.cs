using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
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
	public class ProductController : AdminControllerBase
	{
		private readonly IProductRepository IProductRepository;
		private readonly ISysErrorRepository ISysErrorRepository;
		public ProductController(IWebHostEnvironment _host, ISysErrorRepository _errorRepo, IProductRepository _productRepo)
			: base(_host)
		{
			IProductRepository = _productRepo;
			ISysErrorRepository = _errorRepo;
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin, SysAdmin, CreatorAdmin")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await IProductRepository.GetAll());
		}

		[HttpGet("get/{id?}")]
		[Authorize(Roles = "Admin, SysAdmin, CreatorAdmin")]
		public async Task<IActionResult> Get(int? id)
		{
			if (id == null)
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1476,
						ErrorMessage = ISysErrorRepository.GetName(1476)
					}
				});
			}
			else
			{
				return Ok(await IProductRepository.Details(id ?? 0));
			}
		}
	}
}
