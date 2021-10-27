using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.ViewModels.App;
using DoAnCoSo2.DTOs.App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Repositories.App
{
	public class ProductRepository : IProductRepository
	{
		private readonly DoAnCoSo2DbContext db;
		private ISysErrorRepository ISysErrorRepository;

		public ProductRepository(DoAnCoSo2DbContext _db, ISysErrorRepository _error)
		{
			db = _db;
			ISysErrorRepository = _error;
		}

		public async Task<StandardResponse> Search(string searchString)
		{
			var result = await db.Products.AsNoTracking()
				.Where(x => x.ProductName.Contains(searchString) && x.DeleteAt == null)
				.Include(x => x.Evaluated)
				.ToListAsync();
			return new StandardResponse()
			{
				IsSuccess = true,
				Error = null,
				Payload = result
			};
		}

		public async Task<List<ProductViewModel>> GetAll(string shopUri)
		{
			return await db.Shops.AsNoTracking()
				.Where(x => x.ShopUri == shopUri && x.DeleteAt == null)
				.Include(x => x.Products)
				.ThenInclude(x => x.Brand)
				.Select(x => x.Products.Select(x => new ProductViewModel()
				{
					Id = x.Id,
					ProductName = x.ProductName,
					Price = x.Price ?? 0,
					MinimumPrice = x.MinimumPrice ?? 0,
					MaximumPrice = x.MaximumPrice ?? 0,
					TotalEvaluated = x.TotalEvaluated ?? 0,
					Avatar = x.Avatar,
					Like = x.Like,
					TotalSold = x.TotalSold ?? 0,
					QuantityOfInventory = x.QuantityOfInventory,
					Material = x.Material,
					Description = x.Description,
					CreateAt = x.CreateAt,
					Brand = new BrandViewModel()
					{
						Id = x.Brand.Id,
						Name = x.Brand.Name,
						Avatar = x.Brand.Avatar,
						CreateAt = x.Brand.CreateAt
					},
					Category = new CategoryViewModel()
					{
						Id = x.Category.Id,
						Name = x.Category.Name,
						Avatar = x.Category.Avatar,
						CreateAt = x.Category.CreateAt
					},
					Images = x.Images.Select(x => new ImageViewModel()
					{
						Id = x.Id,
						Url = x.Url,
						CreateAt = x.CreateAt
					}).ToList()
				}).ToList()
				).SingleOrDefaultAsync();
		}

		public bool IsExist(int productID)
		{
			return db.Products.Any(x => x.Id == productID && x.DeleteAt == null);
		}

		public async Task<StandardResponse> Details(int productId)
		{
			if (IsExist(productId))
			{
				var products = await db.Products.AsNoTracking()
					.Where(x => x.Id == productId)
					.Include(x => x.Brand)
					.Include(x => x.Category)
					.Include(x => x.Images)
					.Include(x => x.Evaluated)
					.Include(x => x.Comments)
					.Select(x => new ProductViewModel()
					{
						Id = x.Id,
						ProductName = x.ProductName,
						Price = x.Price ?? 0,
						MinimumPrice = x.MinimumPrice ?? 0,
						MaximumPrice = x.MaximumPrice ?? 0,
						TotalEvaluated = x.TotalEvaluated ?? 0,
						Like = x.Like,
						TotalSold = x.TotalSold ?? 0,
						Avatar = x.Avatar,
						QuantityOfInventory = x.QuantityOfInventory,
						Material = x.Material,
						Description = x.Description,
						CreateAt = x.CreateAt,
						Brand = new BrandViewModel()
						{
							Id = x.Brand.Id,
							Name = x.Brand.Name,
							Avatar = x.Brand.Avatar,
							CreateAt = x.CreateAt
						},
						Category = new CategoryViewModel()
						{
							Id = x.Category.Id,
							Name = x.Category.Name,
							Avatar = x.Category.Avatar,
							CreateAt = x.Category.CreateAt
						},
						Images = x.Images.Select(x => new ImageViewModel()
						{
							Id = x.Id,
							Url = x.Url,
							CreateAt = x.CreateAt
						}).ToList(),
						Comments = x.Comments.Select(x => new CommentViewModel()
						{
							Id = x.Id,
							Content = x.Content,
							PostAt = x.PostAt,
							IsHidden = x.IsHidden,
							Like = x.Like,
							Unlike = x.Unlike
						}).ToList()
					}).SingleOrDefaultAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = products,
					Error = null
				};
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = productId
					},
					Error = new StandardError()
					{
						ErrorCode = 1786,
						ErrorMessage = ISysErrorRepository.GetName(1786)
					}
				};
			}
		}

		public async Task<Shop> GetShop(string shopUri)
		{
			return await db.Shops.SingleOrDefaultAsync(x => x.ShopUri == shopUri && x.DeleteAt == null);
		}

		public async Task<Brand> GetBrand(int? brandId)
		{
			if (brandId == null)
			{
				return null;
			}
			else
			{
				return await db.Brands.SingleOrDefaultAsync(x => x.Id == brandId && x.DeleteAt == null);
			}
		}

		public async Task<Category> GetCategory(int? categoryId)
		{
			return categoryId == null ? null : await db.Categories.SingleOrDefaultAsync(x => x.Id == categoryId && x.DeleteAt == null);
		}

		public async Task<StandardResponse> Create(string shopUri, Product newProduct)
		{
			try
			{
				Shop thisShop = await GetShop(shopUri);
				newProduct.Shop = thisShop;
				await db.Products.AddAsync(newProduct);
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = newProduct
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = newProduct,
					Error = new StandardError()
					{
						ErrorCode = 0000,
						ErrorMessage = ex.Message
					}
				};
			}
		}

		public async Task<StandardResponse> Update(Product product, string avatar, ICollection<Image> images)
		{
			Product updateProduct = await db.Products.FindAsync(product.Id);
			if (updateProduct != null)
			{
				updateProduct.ProductName = product.ProductName;
				updateProduct.CategoryID = product.CategoryID;
				updateProduct.BrandID = product.BrandID;
				updateProduct.Price = product.Price;
				updateProduct.MinimumPrice = product.MinimumPrice;
				updateProduct.MaximumPrice = product.MaximumPrice;
				updateProduct.Avatar = avatar;
				updateProduct.QuantityOfInventory = product.QuantityOfInventory;
				updateProduct.Material = product.Material;
				updateProduct.Description = product.Description;
				updateProduct.Images = images;

				try
				{
					await db.SaveChangesAsync();
					return new StandardResponse()
					{
						IsSuccess = true,
						Payload = new
						{
							Status = "Thêm sản phẩm thành công"
						},
						Error = null
					};
				}
				catch (Exception ex)
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = product,
						Error = new StandardError()
						{
							ErrorCode = 0000,
							ErrorMessage = ex.Message
						}
					};
				}
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = product,
					Error = new StandardError()
					{
						ErrorCode = 1786,
						ErrorMessage = ISysErrorRepository.GetName(1786)
					}
				};
			}
		}
		
		public async Task<StandardResponse> GetAll()
		{
			return new StandardResponse()
			{
				IsSuccess = true,
				Error = null,
				Payload = await db.Products.AsNoTracking()
					.Where(x => x.DeleteAt == null)
					.Include(x => x.Shop)
					.Include(x => x.Brand)
					.Include(x => x.Category)
					.Include(x => x.Images)
					.Include(x => x.Evaluated)
					.OrderByDescending(x => x.TotalSold)
					.ToListAsync()
			};
		}
	}
}
