using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.Auth;
using DoAnCoSo2.Data.RequestModel.Customer;
using DoAnCoSo2.Data.Services.CRUDService;
using DoAnCoSo2.Data.ViewModels.App;
using DoAnCoSo2.Data.ViewModels.Auth;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnCoSo2.Data.Repositories.Auth
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly DoAnCoSo2DbContext db;
		private readonly CRUDService Service;
		private ISysErrorRepository Error;
		private IConfiguration IConfiguration;

		public CustomerRepository(DoAnCoSo2DbContext _db, CRUDService _service, ISysErrorRepository _error, IConfiguration _config)
		{
			db = _db;
			Service = _service;
			Error = _error;
			IConfiguration = _config;
		}

		public async Task<StandardResponse> GetAll()
		{
			var result = await db.Customers.AsNoTracking()
				.Where(x => x.DeleteAt == null)
				.Include(x => x.Shop)
				.Include(x => x.Addresses)
				.Select(x => new CustomerViewModel()
				{
					Id = x.Id,
					Email = x.Email,
					FullName = x.FullName,
					Sex = x.Sex,
					PhoneNumber = x.PhoneNumber,
					Username = x.Username,
					DateOfBirth = x.DateOfBirth,
					Avatar = x.Avatar,
					CreateAt = x.CreateAt,
					Shop = new ViewModels.App.ShopViewModel()
					{
						Id = x.Shop.Id,
						CreateDate = x.Shop.CreateDate,
						Address = x.Shop.Address,
						Avatar = x.Shop.Avatar,
						ShopUri = x.Shop.ShopUri,
						Name = x.Shop.Name,
						Follower = x.Shop.Follower,
						Description = x.Shop.Description,
						LastOnline = x.Shop.LastOnline,
						Nickname = x.Shop.Nickname
					},
					Addresses = x.Addresses.Select(x => new AddressViewModel()
					{
						Id = x.Id,
						FullAddress = x.FullAddress,
						PhoneNumber = x.PhoneNumber,
						Receiver = x.Receiver,
						IsDefault = x.IsDefault,
						CreateAt = x.CreateAt
					}).ToList()
				})
				.ToListAsync();

			return new StandardResponse()
			{
				IsSuccess = true,
				Error = null,
				Payload = result
			};
		}

		public async Task<StandardResponse> Get(int id)
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
					return new StandardResponse()
					{
						IsSuccess = true,
						Payload = db.Customers.AsNoTracking().Where(x => x.Id == id)
							.Include(x => x.Shop)
							.Include(x => x.Addresses)
							.Select(x => new CustomerViewModel()
							{
								Id = x.Id,
								Email = x.Email,
								FullName = x.FullName,
								Sex = x.Sex,
								PhoneNumber = x.PhoneNumber,
								Username = x.Username,
								DateOfBirth = x.DateOfBirth,
								Avatar = x.Avatar,
								CreateAt = x.CreateAt,
								Shop = new ShopViewModel()
								{
									Id = x.Shop.Id,
									CreateDate = x.Shop.CreateDate,
									Address = x.Shop.Address,
									Avatar = x.Shop.Avatar,
									ShopUri = x.Shop.ShopUri
								},
								Addresses = x.Addresses.Select(x => new AddressViewModel()
								{
									Id = x.Id,
									FullAddress = x.FullAddress,
									PhoneNumber = x.PhoneNumber,
									Receiver = x.Receiver,
									IsDefault = x.IsDefault,
									CreateAt = x.CreateAt
								}).ToList()
							}).Single(),
						Error = null
					};
				}
			}
		}

		public bool IsExist(int id)
		{
			return db.Customers.Any(x => x.Id == id);
		}

		public bool IsActive(int id)
		{
			return db.Customers.Any(x => x.Id == id && x.DeleteAt == null);
		}

		public async Task<StandardResponse> Create(CustomerRequestModel model)
		{
			if (IsExistEmail(model.Email))
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Email = model.Email
					},
					Error = new StandardError()
					{
						ErrorCode = 1648,
						ErrorMessage = Error.GetName(1648)
					}
				};
			}
			else
			{
				if (IsExistPhoneNumber(model.PhoneNumber))
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							PhoneNumber = model.PhoneNumber
						},
						Error = new StandardError()
						{
							ErrorCode = 1649,
							ErrorMessage = Error.GetName(1649)
						}
					};
				}
				else
				{
					string salt = "";
					do
					{
						salt = PasswordHelper.CreateSalt(6, 8);
					} while (IsExistSalt(salt));
					Customer newCustomer = new Customer()
					{
						Email = model.Email,
						FullName = model.FullName,
						Sex = model.Sex,
						PhoneNumber = model.PhoneNumber,
						Username = model.PhoneNumber,
						Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
						DateOfBirth = model.DateOfBirth,
						CreateAt = DateTime.Now,
						Salt = salt
					};
					await db.Customers.AddAsync(newCustomer);
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

		public async Task<StandardResponse> Update(CustomerRequestModel model)
		{
			if (IsExist(model.Id))
			{
				if (IsActive(model.Id))
				{
					return await Service.UpdateAsync<Customer, CustomerRequestModel>(model);
				}
				else
				{
					Customer delCustomer = GetDeleted(model.Id);
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							Id = model.Id,
							Username = delCustomer.Username,
							DeleteDate = delCustomer.DeleteAt
						},
						Error = new StandardError()
						{
							ErrorCode = 1404,
							ErrorMessage = Error.GetName(1404)
						}
					};
				}
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = model.Id
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = Error.GetName(1404)
					}
				};
			}
		}

		public async Task<StandardResponse> Delete(int id)
		{
			if (IsActive(id))
			{
				bool result = await Service.DeleteAsync<Customer>(id);
				return new StandardResponse()
				{
					IsSuccess = result,
					Error = null,
					Payload = null
				};
			}
			else
			{
				Customer delCustomer = GetDeleted(id);
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						Id = id,
						Username = delCustomer.Username,
						DeleteDate = delCustomer.DeleteAt
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = Error.GetName(1404)
					}
				};
			}
		}

		public bool IsExistEmail(string email)
		{
			return db.Customers.Any(x => x.Email == email);
		}

		public bool IsExistPhoneNumber(string phone)
		{
			return db.Customers.Any(x => x.PhoneNumber == phone);
		}

		public bool IsExistSalt(string salt)
		{
			return db.Customers.Any(x => x.Salt == salt);
		}

		public Customer GetDeleted(int id)
		{
			return db.Customers.Single(x => x.Id == id);
		}

		public async Task<StandardResponse> Restore(int id)
		{
			if (IsExist(id))
			{
				if (!IsActive(id))
				{
					bool result = await Service.RestoreAsync<Customer>(id);
					return new StandardResponse()
					{
						IsSuccess = result,
						Error = null,
						Payload = null
					};
				}
				else
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
							ErrorMessage = Error.GetName(1435)
						}
					};
				}
			}
			else
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
						ErrorMessage = Error.GetName(1412)
					}
				};
			}
		}
		public async Task<StandardResponse> Login(LoginRequestModel model)
		{
			var customer = GetByPhoneNumber(model.PhoneNumber);
			if (customer == null)
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new
					{
						PhoneNumber = model.PhoneNumber
					},
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = Error.GetName(1404)
					}
				};
			}
			else
			{
				bool isRightPassword = BCrypt.Net.BCrypt.Verify(model.Password, customer.Password);
				if (!isRightPassword)
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = new
						{
							PhoneNumber = model.PhoneNumber,
							Password = model.Password
						},
						Error = new StandardError()
						{
							ErrorCode = 1505,
							ErrorMessage = Error.GetName(1505)
						}
					};
				}
				else
				{
					string token = JwtService.General(customer);
					return new StandardResponse()
					{
						IsSuccess = true,
						Payload = token,
						Error = null
					};
				}
			}
		}

		public Customer GetByPhoneNumber(string phoneNumber)
		{
			return db.Customers.SingleOrDefault(x => x.PhoneNumber == phoneNumber);
		}

		public async Task<StandardResponse> UploadAvatar(string adminSalt, string path)
		{
			Customer customer = await db.Customers.SingleOrDefaultAsync(x => x.Salt == adminSalt);
			string thisDomain = IConfiguration.GetSection("Domain").Value;
			if (customer != null)
			{
				customer.Avatar = thisDomain + path;
				await db.SaveChangesAsync();
				return new StandardResponse()
				{
					IsSuccess = true,
					Payload = new { URL = thisDomain + path },
					Error = null
				};
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = new { URL = thisDomain + path },
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = Error.GetName(1404)
					}
				};
			}
		}

		public async Task<StandardResponse> EmailAuthentication(string email)
		{
			if (email != null)
			{
				bool isExistEmail = await IsExistEmailAsync(email);
				if (isExistEmail)
				{
					Customer customer = await db.Customers
						.AsNoTracking()
						.Include(x => x.Shop)
						.Where(x => x.Email == email && x.DeleteAt == null)
						.SingleOrDefaultAsync();
					string token = JwtService.General(customer, "ShopAdmin");
					return new StandardResponse()
					{
						IsSuccess = true,
						Payload = new
						{
							token = token,
							email = customer.Email,
							fullName = customer.FullName,
							phoneNumber = customer.PhoneNumber,
							username = customer.Username,
							avatar = customer.Avatar,
							shopName = customer.Shop.Name,
							shopAvatar = customer.Shop.Avatar
						},
						Error = null
					};
				}
				else
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Payload = email,
						Error = new StandardError()
						{
							ErrorCode = 1404,
							ErrorMessage = Error.GetName(1404)
						}
					};
				}
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1287,
						ErrorMessage = Error.GetName(1287)
					}
				};
			}
		}

		public async Task<bool> IsExistEmailAsync(string email)
		{
			return await db.Customers.AnyAsync(x => x.Email == email && x.DeleteAt == null);
		}

		public async Task<StandardResponse> Login(SellerLoginViewModel model)
		{
			if (!String.IsNullOrEmpty(model.Email))
			{
				Customer customer = await db.Customers
					.AsNoTracking()
					.Include(x => x.Shop)
					.SingleOrDefaultAsync(x => x.Email == model.Email && x.DeleteAt == null);
				if (customer != null)
				{
					bool isRightPassword = BCrypt.Net.BCrypt.Verify(model.Password, customer.Password);
					if (isRightPassword)
					{
						if (customer.Shop == null)
						{
							return new StandardResponse()
							{
								IsSuccess = false,
								Payload = model,
								Error = new StandardError()
								{
									ErrorCode = 1221,
									ErrorMessage = Error.GetName(1221)
								}
							};
						}
						else
						{
							string token = JwtService.General(customer, "ShopAdmin");
							return new StandardResponse()
							{
								IsSuccess = true,
								Payload = new
								{
									token = token,
									email = customer.Email,
									fullName = customer.FullName,
									phoneNumber = customer.PhoneNumber,
									username = customer.Username,
									avatar = customer.Avatar,
									shopName = customer.Shop.Name,
									shopAvatar = customer.Shop.Avatar
								},
								Error = null
							};
						}
					}
					else
					{
						return new StandardResponse()
						{
							IsSuccess = false,
							Payload = model,
							Error = new StandardError()
							{
								ErrorCode = 1505,
								ErrorMessage = Error.GetName(1505)
							}
						};
					}
					return null;
				}
				else
				{
					return new StandardResponse()
					{
						IsSuccess = false,
						Error = new StandardError()
						{
							ErrorCode = 1404,
							ErrorMessage = Error.GetName(1404)
						},
						Payload = null
					};
				}
			}
			else
			{
				return new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1287,
						ErrorMessage = Error.GetName(1287)
					}
				};
			}
		}
	}
}
