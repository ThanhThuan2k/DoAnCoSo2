using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.Auth;
using DoAnCoSo2.Data.RequestModel.Customer;
using DoAnCoSo2.Data.Services.CRUDService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		[HttpPost("upload")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			var root = Host.WebRootPath;
			var filename = Path.GetFileNameWithoutExtension(file.FileName)
							+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
							+ Path.GetExtension(file.FileName);
			if (!Directory.Exists(root + "/Images/Customer/Avatar/"))
			{
				Directory.CreateDirectory(root + "/Images/Customer/Avatar/");
			}
			var relativePath = "/Images/Customer/Avatar/" + filename;
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
