using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
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
	}
}
