using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Services.CRUDService;
using DoAnCoSo2.Data.ViewModels.Auth;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Repositories.Auth
{
	public class AdminRepository : IAdminRepository
	{
		private readonly DoAnCoSo2DbContext db;

		public AdminRepository(DoAnCoSo2DbContext _db)
		{
			db = _db;
		}

		public Admin GetAdminByEmail(string email)
		{
			return db.Admins.AsNoTracking().Include(x => x.Role)
				.Single(x => x.Email == email);
		}

		public bool IsExistEmail(string email)
		{
			var admin = db.Admins.SingleOrDefault(x => x.Email == email);
			if (admin == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public async Task LoginFailPassword(string email)
		{
			var admin = GetAdminByEmail(email);
			if (admin != null)
			{
				admin.AccessFailCount++;
				admin.UpdateAt = DateTime.Now;
				if (admin.AccessFailCount > 3)
				{
					admin.AccessFailCount = 0;
					admin.IsBlock = true;
				}
				await db.SaveChangesAsync();
			}
		}

		public async Task ResetAccessFailCount(string email)
		{
			var admin = GetAdminByEmail(email);
			if (admin != null)
			{
				admin.AccessFailCount = 0;
				await db.SaveChangesAsync();
			}
		}

		public async Task<bool> CreateAccount(string username, string hashedPassword, string fullName, string sex, string phoneNumber, string email, string salt)
		{
			try
			{
				await db.Admins.AddAsync(new Admin()
				{
					Username = username,
					Password = hashedPassword,
					FullName = fullName,
					Sex = sex,
					PhoneNumber = phoneNumber,
					Email = email,
					Role = await db.SysRoles.SingleOrDefaultAsync(x => x.RoleCode == 5),
					Salt = salt
				});
				await db.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		public bool IsExistSalt(string salt)
		{
			return db.Admins.Any(x => x.Salt == salt);
		}

		public bool IsExistPhoneNumber(string phoneNumber)
		{
			return db.Admins.Any(x => x.PhoneNumber == phoneNumber);
		}

		public async Task<object> Get(int id)
		{

			return await db.Admins.AsNoTracking()
				.Where(x => x.Id == id)
				.Select(x => new
				{
					Username = x.Username,
					Email = x.Email,
					FullName = x.FullName,
					PhoneNumber = x.PhoneNumber,
					IsTwoFactorEnable = x.IsTwoFactorEnabled,
					IsBlock = x.IsBlock,
					Avatar = x.Avatar,
					CreateDate = x.CreateAt,
					IsValidEmail = x.IsValidEmail,
					IsValidPhoneNumber = x.IsValidPhoneNumber,
					LastOnline = x.LastOnlineTime,
					Sex = x.Sex,
					Role = new
					{
						RoleCode = x.Role.RoleCode,
						RoleName = x.Role.Role
					}
				}).SingleOrDefaultAsync();
		}

		public async Task<StandardResponse> Delete(int id)
		{
			bool isExist = IsExist(id);
			if (!isExist)
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = id
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1404).Select(x => x.ErrorName).SingleAsync()
					}
				};
			}
			else
			{
				bool isActive = IsActive(id);
				if (!isActive)
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							Id = id,
							DeleteDate = GetDeleted(id).DeleteAt
						},
						Error = new StandardError()
						{
							ErrorCode = 1412,
							ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1412).Select(x => x.ErrorName).SingleAsync()
						}
					};
				}
				else
				{
					var admin = GetById(id);
					admin.DeleteAt = DateTime.Now;
					await db.SaveChangesAsync();
					return new StandardResponse()
					{
						IsSuccess = true,
						Error = null,
						Payload = null
					};
				}
			}
		}

		public bool IsExist(int id)
		{
			return db.Admins.Any(x => x.Id == id);
		}

		public bool IsActive(int id)
		{
			return db.Admins.Any(x => x.Id == id && x.DeleteAt == null);
		}

		public Admin GetById(int id)
		{
			return db.Admins.SingleOrDefault(x => x.Id == id && x.DeleteAt == null);
		}

		public Admin GetDeleted(int id)
		{
			return db.Admins.SingleOrDefault(x => x.Id == id && x.DeleteAt != null);
		}

		public bool IsBlock(int id)
		{
			return db.Admins.Any(x => x.Id == id && x.IsBlock == true);
		}

		public async Task<StandardResponse> Unblock(int id)
		{
			if (!IsExist(id))
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = id
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1404).Select(x => x.ErrorName).SingleOrDefaultAsync()
					}
				};
			}
			else
			{
				if (!IsActive(id))
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							Id = id
						},
						Error = new StandardError()
						{
							ErrorCode = 1412,
							ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1412).Select(x => x.ErrorName).SingleOrDefaultAsync()
						}
					};
				}
				else
				{
					if (!IsBlock(id))
					{
						return new StandardResponse()
						{
							IsSuccess = false,
							Payload = new
							{
								Id = id
							},
							Error = new StandardError()
							{
								ErrorCode = 1453,
								ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1453).Select(x => x.ErrorName).SingleOrDefaultAsync()
							}
						};
					}
					else
					{
						var admin = GetBlocked(id);
						admin.IsBlock = false;
						await db.SaveChangesAsync();

						return new StandardResponse()
						{
							IsSuccess = true,
							Payload = null,
							Error = null
						};
					}
				}
			}
		}

		public Admin GetBlocked(int id)
		{
			return db.Admins.SingleOrDefault(x => x.Id == id && x.IsBlock == true);
		}

		public async Task<StandardResponse> Block(int id)
		{
			if (!IsExist(id))
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = id
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1404).Select(x => x.ErrorName).SingleOrDefaultAsync()
					}
				};
			}
			else
			{
				if (!IsActive(id))
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							Id = id
						},
						Error = new StandardError()
						{
							ErrorCode = 1412,
							ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1412).Select(x => x.ErrorName).SingleOrDefaultAsync()
						}
					};
				}
				else
				{
					if (!IsBlock(id))
					{
						return new StandardResponse()
						{
							IsSuccess = false,
							Payload = new
							{
								Id = id
							},
							Error = new StandardError()
							{
								ErrorCode = 1453,
								ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1453).Select(x => x.ErrorName).SingleOrDefaultAsync()
							}
						};
					}
					else
					{
						var admin = GetById(id);
						admin.IsBlock = true;
						await db.SaveChangesAsync();

						return new StandardResponse()
						{
							IsSuccess = true,
							Payload = null,
							Error = null
						};
					}
				}
			}
		}

		public async Task<StandardResponse> RestoreDeleted(int id)
		{
			if (!IsExist(id))
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = id
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1404).Select(x => x.ErrorName).SingleOrDefaultAsync()
					}
				};
			}
			else
			{
				if (IsActive(id))
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							Id = id
						},
						Error = new StandardError()
						{
							ErrorCode = 1435,
							ErrorMessage = await db.SysErrors.Where(x => x.ErrorCode == 1435).Select(x => x.ErrorName).SingleOrDefaultAsync()
						}
					};
				}
				else
				{
					var admin = GetDeleted(id);
					admin.DeleteAt = null;
					await db.SaveChangesAsync();

					return new StandardResponse()
					{
						IsSuccess = true,
						Payload = null,
						Error = null
					};
				}
			}
		}

		public async Task<StandardResponse> GetAll()
		{
			var result = await db.Admins
				.AsNoTracking()
				.Where(x => x.DeleteAt == null)
				.Select(x => new AdminViewModel()
				{
					Id = x.Id,
					Username = x.Username,
					FullName = x.FullName,
					Sex = x.Sex,
					PhoneNumber = x.PhoneNumber,
					Email = x.Email,
					IsBlock = x.IsBlock,
					Avatar = x.Avatar,
					CreateAt = x.CreateAt,
					IsTwoFactorEnabled = x.IsTwoFactorEnabled,
					LastOnlineTime = x.LastOnlineTime,
					IsValidEmail = x.IsValidEmail,
					IsValidPhoneNumber = x.IsValidPhoneNumber,
					Role = new ViewModels.Sys.SysRolesViewModel()
					{
						RoleCode = x.Role.RoleCode,
						Role = x.Role.Role
					}
				})
				.ToListAsync();
			return new StandardResponse()
			{
				IsSuccess = true,
				Payload = result,
				Error = null
			};
		}

		public async Task<StandardResponse> BlockYourself(string salt)
		{
			return null;
		}
	}
}
