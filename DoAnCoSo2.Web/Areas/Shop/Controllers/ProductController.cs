using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.Web.Areas.Shop.ViewModel.Product;
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

namespace DoAnCoSo2.Web.Areas.Shop.Controllers
{
	public class ProductController : ShopControllerBase
	{
		private readonly IProductRepository IProductRepository;
		private readonly ISysErrorRepository ISysErrorRepository;
		private IConfiguration IConfiguration;
		public ProductController(IWebHostEnvironment _host, IProductRepository _productRepo, IConfiguration _config, ISysErrorRepository _errorRepo)
			: base(_host)
		{
			IProductRepository = _productRepo;
			ISysErrorRepository = _errorRepo;
			IConfiguration = _config;
		}

		[HttpGet("all")]
		[Authorize(Roles = "ShopAdmin")]
		public async Task<IActionResult> GetAll()
		{
			var currentShopAdmin = HttpContext.User.Identity as ClaimsIdentity;
			string shopUri = currentShopAdmin.FindFirst("shopuri").Value;
			if (shopUri != null)
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = await IProductRepository.GetAll(shopUri)
				});
			}
			else
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1504,
						ErrorMessage = ISysErrorRepository.GetName(1504)
					}
				});
			}
		}

		[HttpPost("create")]
		[Authorize(Roles = "ShopAdmin")]
		public async Task<IActionResult> Create([FromForm] CreateProductViewModel model)
		{
			string avatar = "";
			string avatarLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Product").GetSection("Avatar").Value;
			string describeLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Product").GetSection("Describe").Value;
			var root = Host.WebRootPath;
			if (model.Avatar != null)
			{
				var filename = Path.GetFileNameWithoutExtension(model.Avatar.FileName)
								+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
								+ Path.GetExtension(model.Avatar.FileName);
				if (!Directory.Exists(root + avatarLocation))
				{
					Directory.CreateDirectory(root + avatarLocation);
				}
				var relativePath = avatarLocation + filename;
				var path = root + relativePath;
				var x = new FileStream(path, FileMode.Create);
				model.Avatar.CopyTo(x);
				x.Dispose();
				GC.Collect();
				avatar = IConfiguration.GetSection("Domain").Value + relativePath;
			}

			var images = model.Images;
			var imageList = new List<Image>();
			if (images.Count > 0)
			{
				for (int i = 0; i < model.Images.Count; i++)
				{
					var image = images.ElementAt(i);
					var filename = Path.GetFileNameWithoutExtension(image.FileName)
									+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
									+ Path.GetExtension(image.FileName);
					if (!Directory.Exists(root + describeLocation))
					{
						Directory.CreateDirectory(root + describeLocation);
					}
					var relativePath = describeLocation + filename;
					var path = root + relativePath;
					var x = new FileStream(path, FileMode.Create);
					image.CopyTo(x);
					x.Dispose();
					GC.Collect();
					imageList.Add(new Image()
					{
						Url = IConfiguration.GetSection("Domain").Value + relativePath,
						CreateAt = DateTime.Now
					});
				}
			}

			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string shopUri = currentUser.FindFirst("shopuri").Value;

			Product newProduct = new Product()
			{
				ProductName = model.ProductName.Trim(),
				Price = model.Price ?? 0,
				MinimumPrice = model.MinimumPrice ?? 0,
				MaximumPrice = model.MaximumPrice ?? 0,
				QuantityOfInventory = model.QuantityOfInventory,
				Material = model.Material,
				Description = model.Description,
				CreateAt = DateTime.Now,
				Avatar = avatar,
				Like = 0,
				TotalEvaluated = 0,
				TotalSold = 0,
				Shop = await IProductRepository.GetShop(shopUri),
				Brand = await IProductRepository.GetBrand(model.BrandID),
				Category = await IProductRepository.GetCategory(model.CategoryID),
				Images = imageList
			};
			return Ok(await IProductRepository.Create(shopUri, newProduct));
		}

		[HttpPut("update")]
		[Authorize(Roles = "ShopAdmin")]
		public async Task<IActionResult> Update(UpdateProductViewModel update)
		{
			string avatar = "";
			var root = Host.WebRootPath;
			if (update.Avatar != null)
			{
				var filename = Path.GetFileNameWithoutExtension(update.Avatar.FileName)
								+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
								+ Path.GetExtension(update.Avatar.FileName);
				if (!Directory.Exists(root + "/Images/Shop/Avatar/"))
				{
					Directory.CreateDirectory(root + "/Images/Shop/Avatar/");
				}
				var relativePath = "/Images/Shop/Avatar/" + filename;
				var path = root + relativePath;
				var x = new FileStream(path, FileMode.Create);
				update.Avatar.CopyTo(x);
				x.Dispose();
				GC.Collect();
				avatar = IConfiguration.GetSection("Domain").Value + relativePath;
			}

			var images = update.Images;
			List<Image> imageList = new List<Image>();
			if (images.Count() > 0)
			{
				for (int i = 0; i < images.Count(); i++)
				{
					var image = images.ElementAt(i);
					var filename = Path.GetFileNameWithoutExtension(image.FileName)
									+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
									+ Path.GetExtension(image.FileName);
					if (!Directory.Exists(root + "/Images/Products/Describe/"))
					{
						Directory.CreateDirectory(root + "/Images/Products/Describe/");
					}
					var relativePath = "/Images/Products/Describe/" + filename;
					var path = root + relativePath;
					var x = new FileStream(path, FileMode.Create);
					image.CopyTo(x);
					x.Dispose();
					GC.Collect();
					Image newImage = new Image()
					{
						Url = IConfiguration.GetSection("Domain").Value + relativePath,
						CreateAt = DateTime.Now
					};
					imageList.Add(newImage);
				}
			}

			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string shopUri = currentUser.FindFirst("shopuri").Value;

			Product newProduct = new Product()
			{
				ProductName = update.ProductName.Trim(),
				Price = update.Price ?? 0,
				MinimumPrice = update.MinimumPrice ?? 0,
				MaximumPrice = update.MaximumPrice ?? 0,
				QuantityOfInventory = update.QuantityOfInventory,
				Material = update.Material,
				Description = update.Description,
				CreateAt = DateTime.Now,
				Avatar = avatar,
				Like = 0,
				TotalEvaluated = 0,
				TotalSold = 0,
				Shop = await IProductRepository.GetShop(shopUri),
				Brand = await IProductRepository.GetBrand(update.BrandID),
				Category = await IProductRepository.GetCategory(update.CategoryID),
				Images = imageList
			};
			return Ok(await IProductRepository.Update(newProduct, avatar, imageList));
		}
	}
}
