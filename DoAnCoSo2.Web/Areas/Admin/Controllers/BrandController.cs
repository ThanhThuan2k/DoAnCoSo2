using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.Web.Areas.Admin.ViewModels.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	public class BrandController : AdminControllerBase
	{
		private IBrandRepository IBrandRepository;
		private IConfiguration IConfiguration;

		public BrandController(IWebHostEnvironment _env, IBrandRepository _brandRepo, IConfiguration _config)
			: base(_env)
		{
			IBrandRepository = _brandRepo;
			IConfiguration = _config;
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin, SysAdmin, ShopAdmin")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await IBrandRepository.GetAll());
		}

		[HttpPost("create")]
		[Authorize(Roles = "Admin, SysAdmin, ShopAdmin")]
		public async Task<IActionResult> Create([FromForm] BrandViewModel model)
		{
			string savePath = "";
			if (model.Avatar != null)
			{
				var root = Host.WebRootPath;
				string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Brand").GetSection("Avatar").Value;
				var filename = Path.GetFileNameWithoutExtension(model.Avatar.FileName)
								+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
								+ Path.GetExtension(model.Avatar.FileName);
				if (!Directory.Exists(root + imageLocation))
				{
					Directory.CreateDirectory(root + imageLocation);
				}
				var relativePath = imageLocation + filename;
				var path = root + relativePath;
				var x = new FileStream(path, FileMode.Create);
				model.Avatar.CopyTo(x);
				x.Dispose();
				GC.Collect();
				savePath = IConfiguration.GetSection("Domain").Value + imageLocation;
			}
			var newBrand = new Brand()
			{
				Name = model.Name,
				Avatar = savePath,
				CreateAt = DateTime.Now
			};
			var result = await IBrandRepository.Create(newBrand);
			return Ok(result);
		}

		[HttpPut("update")]
		[Authorize(Roles = "Admin, SysAdmin, ShopAdmin")]
		public async Task<IActionResult> Update([FromForm]BrandViewModel update)
		{
			string ava = "";
			if (update.Avatar != null)
			{
				var root = Host.WebRootPath;
				string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Brand").GetSection("Avatar").Value;
				var filename = Path.GetFileNameWithoutExtension(update.Avatar.FileName)
								+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
								+ Path.GetExtension(update.Avatar.FileName);
				if (!Directory.Exists(root + imageLocation))
				{
					Directory.CreateDirectory(root + imageLocation);
				}
				var relativePath = imageLocation + filename;
				var path = root + relativePath;
				var x = new FileStream(path, FileMode.Create);
				update.Avatar.CopyTo(x);
				ava = IConfiguration.GetSection("Domain").Value + relativePath;
				x.Dispose();
				GC.Collect();
			}
			var updateBrand = new Brand()
			{
				Id = update.Id,
				Name = update.Name,
				Avatar = ava,
				UpdateAt = DateTime.Now
			};
			return Ok(await IBrandRepository.Update(updateBrand));
		}
	}
}
