using DoAnCoSo2.Data.Common;
using DoAnCoSo2.DTOs.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories.App
{
	public interface ICategoryRepository
	{
		Task<StandardResponse> Create(Category category);
		Task<StandardResponse> Update(Category update);
		Task<StandardResponse> GetAll();
	}
}
