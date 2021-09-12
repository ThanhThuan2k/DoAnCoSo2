using DoAnCoSo2.Data.Interfaces.Repositories.App;
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
	public class ProductController : BaseController
	{
		private readonly IProductRepository IProductRepository;
		public ProductController(IWebHostEnvironment _host, CRUDService _service, IProductRepository _productRepo)
		: base(_host, _service)
		{
			IProductRepository = _productRepo;
		}

		[HttpGet("search")]
		[AllowAnonymous]
		public async Task<IActionResult> Search(string searchString)
		{
			return Ok(await IProductRepository.Search(searchString));
		}
	}
}
