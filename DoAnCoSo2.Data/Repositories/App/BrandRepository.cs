using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.Repositories.Sys;
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
	public class BrandRepository : IBrandRepository
	{
		private readonly DoAnCoSo2DbContext db;
		private readonly CRUDService Service;
		private readonly SysErrorRepository SysErrorRepository;

		public BrandRepository()
		{
			db = new DoAnCoSo2DbContext();
			Service = new CRUDService();
			SysErrorRepository = new SysErrorRepository(db);
		}

		public async Task<StandardResponse> Create(Brand newBrand)
		{
			try
			{
				await db.Brands.AddAsync(newBrand);
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = null
				};
			}
			catch(Exception ex)
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = newBrand,
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
				Error = null,
				Payload = await db.Brands.Where(x => x.DeleteAt == null).ToListAsync()
			};
		}

		public async Task<StandardResponse> Update(Brand update)
		{
			if (IsExist(update.Id))
			{
				try
				{
					await Service.UpdateAsync<Brand, Brand>(update);
					return new StandardResponse()
					{
						IsSuccess = true,
						Payload = update,
						Error = null
					};
				}
				catch(Exception ex)
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = null,
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
					Payload = update,
					Error = new StandardError()
					{
						ErrorCode = 1495,
						ErrorMessage = SysErrorRepository.GetName(1495)
					}
				};
			}
		}

		public bool IsExist(int id)
		{
			return db.Brands.Any(x => x.Id == id && x.DeleteAt == null);
		}
	}
}
