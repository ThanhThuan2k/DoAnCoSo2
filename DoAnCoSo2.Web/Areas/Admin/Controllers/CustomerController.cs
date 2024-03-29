﻿using DoAnCoSo2.Data;
using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories.Auth;
using DoAnCoSo2.Data.Services.CRUDService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	public class CustomerController : AdminControllerBase
	{
		private readonly DoAnCoSo2DbContext db;
		private readonly CRUDService service;
		private readonly ICustomerRepository ICustomerRepository;

		public CustomerController(IWebHostEnvironment _host, DoAnCoSo2DbContext _db, CRUDService _service, ICustomerRepository _repo)
			: base(_host)
		{
			db = _db;
			service = _service;
			ICustomerRepository = _repo;
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin, SysAdmin, SecurityAdmin, CreatorAdmin")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await ICustomerRepository.GetAll());
		}

		[HttpGet("get/{id}")]
		[Authorize(Roles = "Admin, SysAdmin, SecurityAdmin, CreatorAdmin")]
		public async Task<IActionResult> Get(int id)
		{
			StandardResponse response = await ICustomerRepository.Get(id);
			return Ok(response);
		}

		[HttpPut("restore/{id}")]
		[Authorize(Roles = "Admin, SysAdmin, SecurityAdmin")]
		public async Task<IActionResult> Restore(int id)
		{
			return Ok(await ICustomerRepository.Restore(id));
		}
	}
}
