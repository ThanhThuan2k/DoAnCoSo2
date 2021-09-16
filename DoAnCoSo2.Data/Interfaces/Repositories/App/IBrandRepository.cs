using DoAnCoSo2.Data.Common;
using DoAnCoSo2.DTOs.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Interfaces.Repositories.App
{
	public interface IBrandRepository
	{
		Task<StandardResponse> GetAll();
		Task<StandardResponse> Create(Brand newBrand);
		Task<StandardResponse> Update(Brand update);
		bool IsExist(int id);
	}
}
