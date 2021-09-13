using DoAnCoSo2.Data.Common;
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
		public ProductRepository(DoAnCoSo2DbContext _db)
		{
			db = _db;
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
					Price = x.Price,
					MinimumPrice = x.MinimumPrice,
					MaximumPrice = x.MaximumPrice,
					TotalEvaluated = x.TotalEvaluated,
					Like = x.Like,
					TotalSold = x.TotalSold,
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
	}
}
