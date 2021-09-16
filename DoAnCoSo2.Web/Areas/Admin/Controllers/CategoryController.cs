using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Web.Areas.Admin.ViewModels.Category;
using DoAnCoSo2.DTOs.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	public class CategoryController : AdminControllerBase
	{
		private IConfiguration IConfiguration;
		private readonly ICategoryRepository ICategoryRepository;
		public CategoryController(IWebHostEnvironment _env, IConfiguration _config, ICategoryRepository _categoryRepo)
			: base(_env)
		{
			IConfiguration = _config;
			ICategoryRepository = _categoryRepo;
		}

		[HttpPost("create")]
		[Authorize(Roles = "Admin, SysAdmin, CreatorAdmin")]
		public async Task<IActionResult> Create([FromForm] CategoryViewModel category)
		{
			string savePath = "";
			if (category.Avatar != null)
			{
				var root = Host.WebRootPath;
				string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Category").GetSection("Avatar").Value;
				var filename = Path.GetFileNameWithoutExtension(category.Avatar.FileName)
								+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
								+ Path.GetExtension(category.Avatar.FileName);
				if (!Directory.Exists(root + imageLocation))
				{
					Directory.CreateDirectory(root + imageLocation);
				}
				var relativePath = imageLocation + filename;
				var path = root + relativePath;
				var x = new FileStream(path, FileMode.Create);
				category.Avatar.CopyTo(x);
				x.Dispose();
				GC.Collect();
				savePath = IConfiguration.GetSection("Domain").Value + imageLocation;
			}
			var newCategory = new Category()
			{
				Name = category.Name,
				Avatar = savePath,
				CreateAt = DateTime.Now
			};
			var result = await ICategoryRepository.Create(newCategory);
			return Ok(result);
		}

		[HttpPut("update")]
		[Authorize(Roles = "Admin, SysAdmin, CreatorAdmin")]
		public async Task<IActionResult> Update([FromForm] CategoryViewModel update)
		{
			string ava = "";
			if (update.Avatar != null)
			{
				var root = Host.WebRootPath;
				string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Category").GetSection("Avatar").Value;
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
				ava = IConfiguration.GetSection("Domain").Value + imageLocation;
				x.Dispose();
				GC.Collect();
			}
			var updateCategory = new Category()
			{
				Name = update.Name,
				Avatar = ava,
				UpdateAt = DateTime.Now
			};
			return Ok(await ICategoryRepository.Update(updateCategory));
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin, SysAdmin")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await ICategoryRepository.GetAll());
		}
	}
}
