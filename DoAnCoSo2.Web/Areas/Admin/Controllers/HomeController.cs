using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Services.CRUDService;
using DoAnCoSo2.Web.Areas.Admin.ViewModels;
using DoAnCoSo2.Web.Areas.Admin.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	public class HomeController : AdminControllerBase
	{
		private readonly IAdminRepository IAdminRepository;
		private readonly ISysErrorRepository ISysErrorRepository;
		private readonly JwtService JwtService;
		private readonly CRUDService service;

		public HomeController(IWebHostEnvironment _host, IAdminRepository adminRepo, JwtService jwtService, ISysErrorRepository errorRepository, CRUDService _service)
			: base(_host)
		{
			IAdminRepository = adminRepo;
			JwtService = jwtService;
			ISysErrorRepository = errorRepository;
			service = _service;
		}

		#region Authentication
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			bool isExist = IAdminRepository.IsExistEmail(model.Email);
			if (!isExist)
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Email = model.Email
					},
					Error = new StandardError
					{
						ErrorCode = 1404,
						ErrorMessage = ISysErrorRepository.GetName(1404)
						// lỗi tài khoản không tồn tại
					}
				});
			}
			else
			{
				var admin = IAdminRepository.GetAdminByEmail(model.Email);
				if (BCrypt.Net.BCrypt.Verify(model.Password, admin.Password))
				{
					if (admin.IsBlock)
					{
						return Ok(new StandardResponse()
						{
							IsSuccess = false,
							Payload = new
							{
								Email = model.Email
							},
							Error = new StandardError()
							{
								ErrorCode = 1872,
								ErrorMessage = ISysErrorRepository.GetName(1872)
								// tài khoản tạm thời bị khóa
							}
						});
					}
					else
					{
						string jwt = JwtService.General(admin.Salt, admin);
						Response.Cookies.Append("jwt", jwt);
						await IAdminRepository.ResetAccessFailCount(admin.Email);
						//var user = HttpContext.User.Claims.Where(x => )
						return Ok(new StandardResponse()
						{
							IsSuccess = true,
							Error = null,
							Payload = new
							{
								Fullname = admin.FullName,
								Sex = admin.Sex
							}
						});
					}
				}
				else
				{
					await IAdminRepository.LoginFailPassword(admin.Email);
					return Ok(new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							Email = model.Email
						},
						Error = new StandardError()
						{
							ErrorCode = 1505,
							ErrorMessage = ISysErrorRepository.GetName(1505)
							// Lỗi sai mật khẩu
						}
					});
				}
			}
		}

		[HttpPost("logout")]
		public IActionResult Logout()
		{
			if (String.IsNullOrEmpty(Request.Cookies["jwt"]))
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Error = new StandardError()
					{
						ErrorCode = 1504,
						ErrorMessage = ISysErrorRepository.GetName(1504)
					}
				});
			}
			else
			{
				Response.Cookies.Delete("jwt");
				return Ok(new StandardResponse()
				{
					IsSuccess = true,
					Error = null,
					Payload = null
				});
			}
		}

		#endregion

		#region CRUD
		[HttpPost("create")]
		[AllowAnonymous]
		public async Task<IActionResult> Create(SignUpViewModel model)
		{
			if ((String.IsNullOrEmpty(model.Email.Trim()) && String.IsNullOrEmpty(model.PhoneNumber.Trim())) || String.IsNullOrEmpty(model.Password))
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = model,
					Error = new StandardError()
					{
						ErrorCode = 1287,
						ErrorMessage = ISysErrorRepository.GetName(1287)
					}
				});
			}
			else
			{
				if (IAdminRepository.IsExistEmail(model.Email))
				{
					return Ok(new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							Email = model.Email
						},
						Error = new StandardError()
						{
							ErrorCode = 1648,
							ErrorMessage = ISysErrorRepository.GetName(1648)
						}
					});
				}
				else if (IAdminRepository.IsExistPhoneNumber(model.PhoneNumber))
				{
					return Ok(new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							PhoneNumber = model.PhoneNumber
						},
						Error = new StandardError()
						{
							ErrorCode = 1649,
							ErrorMessage = ISysErrorRepository.GetName(1649)
						}
					});
				}
				else
				{
					string hashNewPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

					string randomString = "";
					do
					{
						randomString = PasswordHelper.CreateSalt(6, 8);
					} while (IAdminRepository.IsExistSalt(randomString));

					bool isCreateSuccess = await IAdminRepository.CreateAccount(model.Username, hashNewPassword, model.Fullname, model.Sex, model.PhoneNumber, model.Email, randomString);
					if (isCreateSuccess)
					{
						string jwt = JwtService.General(randomString);
						Response.Cookies.Append("jwt", jwt);
						return Ok(new StandardResponse()
						{
							IsSuccess = true,
							Error = null,
							Payload = new
							{
								Email = model.Email,
								Fullname = model.Fullname
							}
						});
					}
					else
					{
						return Ok(new StandardResponse()
						{
							IsSuccess = false,
							Payload = model,
							Error = new StandardError()
							{
								ErrorCode = 1209,
								ErrorMessage = ISysErrorRepository.GetName(1209)
							}
						});
					}
				}
			}
		}

		[HttpPut("update")]
		public async Task<IActionResult> Update(UpdateViewModel model)
		{
			model.UpdateAt = DateTime.Now;
			StandardResponse updateResult = await service.UpdateAsync<DoAnCoSo2.DTOs.Auth.Admin, UpdateViewModel>(model);
			return Ok(updateResult);
		}

		[HttpGet("get/{id}")]
		public async Task<IActionResult> Get(int id = 0)
		{
			var admin = await IAdminRepository.Get(id);
			if(admin == null)
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = id
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = ISysErrorRepository.GetName(1404)
					}
				});
			}
			else
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = true,
					Payload = admin,
					Error = null
				});
			}
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			StandardResponse response = await IAdminRepository.Delete(id);
			return Ok(response);
		}

		[HttpPut("unblock/{id}")]
		public async Task<IActionResult> Unblock(int id)
		{
			StandardResponse response = await IAdminRepository.Unblock(id);
			return Ok(response);
		}

		[HttpPut("block/{id}")]
		public async Task<IActionResult> Block(int id)
		{
			StandardResponse response = await IAdminRepository.Block(id);
			return Ok(response);
		}

		[HttpPut("restore/{id}")]
		public async Task<IActionResult> Restore(int id)
		{
			StandardResponse response = await IAdminRepository.RestoreDeleted(id);
			return Ok(response);
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			StandardResponse result = await IAdminRepository.GetAll();
			return Ok(result);
		}
		#endregion
	}
}
