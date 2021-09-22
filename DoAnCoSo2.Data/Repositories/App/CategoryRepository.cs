using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.Services.CRUDService;
using DoAnCoSo2.DTOs.App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Repositories.App
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DoAnCoSo2DbContext db;
		private CRUDService Service;
		public CategoryRepository(DoAnCoSo2DbContext _db, CRUDService _service1)
		{
			db = _db;
			Service = _service1;
		}
		public async Task<StandardResponse> Create(Category category)
		{
			try
			{
				category.CreateAt = DateTime.Now;
				await db.Categories.AddAsync(category);
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = category,
					Error = null
				};
			}
			catch (Exception ex)
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Error = new StandardError()
					{
						ErrorCode = 0000,
						ErrorMessage = ex.Message
					},
					Payload = category
				};
			}
		}

		public async Task<StandardResponse> Update(Category update)
		{
			try
			{
				var thisCategory = Get(update.Id);
				if (!String.IsNullOrEmpty(update.Avatar))
				{
					thisCategory.Avatar = update.Avatar;
				}
				if (!String.IsNullOrEmpty(update.Name))
				{
					thisCategory.Name = update.Name;
				}
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = update,
					Error = null
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = update,
					Error = new StandardError()
					{
						ErrorCode = 0000,
						ErrorMessage = ex.Message
					}
				};
			}
		}

		public async Task<StandardResponse> GetAll()
		{
			return new StandardResponse()
			{
				IsSuccess = true,
				Payload = await db.Categories
				.Where(x => x.DeleteAt == null)
				.Select(x => new
				{
					id = x.Id,
					name = x.Name,
					avatar = x.Avatar,
					createAt = x.CreateAt,
					tongSanPham = x.Products.Count
				})
				.ToListAsync(),
				Error = null
			};
		}

		public Category Get(int id)
		{
			return db.Categories.SingleOrDefault(x => x.Id == id && x.DeleteAt == null);
		}
	}
}
