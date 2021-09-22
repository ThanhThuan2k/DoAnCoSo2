using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.Services.CRUDService;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using DoAnCoSo2.Web.ViewModel;
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
	[ApiController]
	[Route("api.v3/[controller]")]
	public class ShopController : Controller
	{
		private readonly IWebHostEnvironment Host;
		private readonly IShopRepository IShopRepository;
		private readonly IConfiguration IConfiguration;
		private readonly ISysErrorRepository ISysErrorRepository;
		public ShopController(IWebHostEnvironment _host, IConfiguration _config, ISysErrorRepository _errorRepo, IShopRepository _shopRepo)
		{
			Host = _host;
			IShopRepository = _shopRepo;
			IConfiguration = _config;
			ISysErrorRepository = _errorRepo;
		}

		[HttpGet("search")]
		public async Task<IActionResult> Search(string search)
		{
			return Ok(new StandardResponse()
			{
				IsSuccess = true,
				Error = null,
				Payload = await IShopRepository.Search(search)
			});
		}

		[HttpPost("create")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> CreateShop([FromForm] CreateShopViewModel model)
		{
			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string customerSalt = currentUser.FindFirst("salt").Value;
			string avatar = "";

			if (model.Avatar != null)
			{
				var root = Host.WebRootPath;
				string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Shop").GetSection("Avatar").Value;
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
				avatar = path;
			}

			Shop newShop = new Shop()
			{
				CreateAt = DateTime.Now,
				CreateDate = DateTime.Now,
				Address = model.Address,
				Avatar = avatar,
				ShopUri = PasswordHelper.RandomNumber(8, 8),
				Name = model.Name,
				Follower = 0,
				Description = model.Description,
				LastOnline = DateTime.Now,
				Nickname = model.Nickname,
				Customer = await IShopRepository.GetCustomerWithTracking(customerSalt)
			};
			return Ok(IShopRepository.CreateShop(newShop));
		}

		[HttpPut("update-avatar")]
		[Authorize(Roles = "ShopAdmin")]
		public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarViewModel avatar)
		{
			if (avatar.Avatar != null)
			{
				var currentUser = HttpContext.User.Identity as ClaimsIdentity;
				string shopUri = currentUser.FindFirst("shopuri").Value;
				var root = Host.WebRootPath;
				string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Shop").GetSection("Avatar").Value;
				var filename = Path.GetFileNameWithoutExtension(avatar.Avatar.FileName)
								+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
								+ Path.GetExtension(avatar.Avatar.FileName);
				if (!Directory.Exists(root + imageLocation))
				{
					Directory.CreateDirectory(root + imageLocation);
				}
				var relativePath = imageLocation + filename;
				var path = root + relativePath;
				var x = new FileStream(path, FileMode.Create);
				avatar.Avatar.CopyTo(x);
				x.Dispose();
				GC.Collect();
				return Ok(await IShopRepository.UploadAvatar(shopUri, relativePath));
			}
			else
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1287,
						ErrorMessage = ISysErrorRepository.GetName(1287)
					}
				});
			}
		}

	}
}
