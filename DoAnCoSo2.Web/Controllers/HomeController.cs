using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.Auth;
using DoAnCoSo2.Data.RequestModel.Customer;
using DoAnCoSo2.Data.Services.CRUDService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ICustomerRepository ICustomerRepository;
		private readonly IConfiguration IConfiguration;
		public HomeController(IWebHostEnvironment _host, CRUDService _service, ICustomerRepository _customerRepo, IConfiguration _config)
			: base(_host, _service)
		{
			ICustomerRepository = _customerRepo;
			IConfiguration = _config;
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

		[HttpPost("upload")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			var root = Host.WebRootPath;
			string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Customer").GetSection("Avatar").Value;
			var filename = Path.GetFileNameWithoutExtension(file.FileName)
							+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
							+ Path.GetExtension(file.FileName);
			if (!Directory.Exists(root + imageLocation))
			{
				Directory.CreateDirectory(root + imageLocation);
			}
			var relativePath = imageLocation + filename;
			var path = root + relativePath;
			var x = new FileStream(path, FileMode.Create);
			file.CopyTo(x);
			x.Dispose();
			GC.Collect();

			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string salt = currentUser.FindFirst("salt").Value;

			var result = await ICustomerRepository.UploadAvatar(salt, relativePath);
			return Ok(result);
		}
	}
}
