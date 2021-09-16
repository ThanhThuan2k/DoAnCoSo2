using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.ViewModels.App;
using DoAnCoSo2.DTOs.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories.App
{
	public interface IProductRepository
	{
		Task<StandardResponse> Search(string searchString);
		Task<List<ProductViewModel>> GetAll(string shopUri);
		Task<StandardResponse> Details(int id);
		Task<Shop> GetShop(string shopUri);
		Task<Brand> GetBrand(int? brandId);
		Task<Category> GetCategory(int? categoryId);
		Task<StandardResponse> Create(string shopUri, Product newProduct);
		Task<StandardResponse> Update(Product update, string avatar, ICollection<Image> images);
	}
}
