using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Services.CRUDService;
using DoAnCoSo2.Web.Areas.Admin.ViewModels;
using DoAnCoSo2.Web.Areas.Admin.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Areas.Admin.Controllers
{
	public class HomeController : AdminControllerBase
	{
		private readonly IAdminRepository IAdminRepository;
		private readonly ISysErrorRepository ISysErrorRepository;
		private readonly JwtService JwtService;
		private readonly CRUDService service;
		private readonly IConfiguration IConfiguration;

		public HomeController(IWebHostEnvironment _host, IAdminRepository adminRepo, JwtService jwtService, ISysErrorRepository errorRepository, CRUDService _service, IConfiguration _config)
			: base(_host)
		{
			IAdminRepository = adminRepo;
			JwtService = jwtService;
			ISysErrorRepository = errorRepository;
			service = _service;
			IConfiguration = _config;
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
						string jwt = JwtService.General(admin);
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
								Sex = admin.Sex,
								Token = jwt
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
		[AllowAnonymous]
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
		[Authorize(Roles = "SysAdmin, CreatorAdmin")]
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
		[Authorize(Roles = "Admin, SysAdmin, SecurityAdmin, CreatorAdmin")]
		public async Task<IActionResult> Update(UpdateViewModel model)
		{
			model.UpdateAt = DateTime.Now;
			StandardResponse updateResult = await service.UpdateAsync<DoAnCoSo2.DTOs.Auth.Admin, UpdateViewModel>(model);
			return Ok(updateResult);
		}

		[HttpGet("get/{id}")]
		[Authorize(Roles = "Admin, SysAdmin, SecurityAdmin, CreatorAdmin")]
		public async Task<IActionResult> Get(int id = 0)
		{
			var admin = await IAdminRepository.Get(id);
			if (admin == null)
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
		[Authorize(Roles = "SysAdmin, SecurityAdmin")]
		public async Task<IActionResult> Delete(int id)
		{
			StandardResponse response = await IAdminRepository.Delete(id);
			return Ok(response);
		}

		[HttpPut("unblock/{id}")]
		[Authorize(Roles = "SysAdmin, SecurityAdmin")]
		public async Task<IActionResult> Unblock(int id)
		{
			StandardResponse response = await IAdminRepository.Unblock(id);
			return Ok(response);
		}

		[HttpPut("block/{id}")]
		[Authorize(Roles = "SysAdmin, SecurityAdmin")]
		public async Task<IActionResult> Block(int id)
		{
			return Ok(await IAdminRepository.Block(id));
		}

		[HttpPut("blockyourself")]
		[Authorize(Roles = "Admin, SysAdmin, SecurityAdmin, CreatorAdmin")]
		public async Task<IActionResult> BlockYourself(DateTime? blockTo)
		{
			if (blockTo == null)
			{
				blockTo = DateTime.Today.AddDays(1);
			}
			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			if (currentUser != null)
			{
				string salt = currentUser.FindFirst("salt").Value;
				return Ok(true);
			}
			else
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1504,
						ErrorMessage = ISysErrorRepository.GetName(1504)
					}
				});
			}
		}

		[HttpPut("restore/{id}")]
		[Authorize(Roles = "SysAdmin")]
		public async Task<IActionResult> Restore(int id)
		{
			StandardResponse response = await IAdminRepository.RestoreDeleted(id);
			return Ok(response);
		}

		[HttpGet("all")]
		[Authorize(Roles = "Admin, SysAdmin")]
		public async Task<IActionResult> GetAll()
		{
			StandardResponse result = await IAdminRepository.GetAll();
			return Ok(result);
		}

		[HttpPost("upload")]
		[Authorize(Roles = "Admin, SysAdmin, CreatorAdmin")]
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			var root = Host.WebRootPath;
			string imageLocation = IConfiguration.GetSection("ImagesLocation").GetSection("Admin").GetSection("Avatar").Value;
			var filename = Path.GetFileNameWithoutExtension(file.FileName)
							+ DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
							+ Path.GetExtension(file.FileName);
			if (!Directory.Exists(root + imageLocation))
			{
				Directory.CreateDirectory(root + imageLocation);
			}
			var relativePath = imageLocation + filename;
			var path = root + relativePath;
			var x = new FileStream(path, FileMode.Create);
			file.CopyTo(x);
			x.Dispose();
			GC.Collect();

			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string salt = currentUser.FindFirst("salt").Value;

			var result = await IAdminRepository.UploadAvatar(salt, relativePath);
			return Ok(result);
		}
		#endregion
	}
}
