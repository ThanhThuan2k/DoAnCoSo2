using DoAnCoSo2.Data.Common;
using DoAnCoSo2.Data.Interfaces.Repositories;
using DoAnCoSo2.Data.Interfaces.Repositories.App;
using DoAnCoSo2.Data.Services.CRUDService;
using DoAnCoSo2.Data.ViewModels.App;
using DoAnCoSo2.DTOs.App;
using DoAnCoSo2.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAnCoSo2.Web.Controllers
{
	public class OrderController : BaseController
	{
		private readonly IOrderRepository IOrderRepository;
		private readonly ISysErrorRepository ISysErrorRepository;
		public OrderController(IWebHostEnvironment _env, CRUDService _service, IOrderRepository _orderRepo, ISysErrorRepository _errorRepo)
			: base(_env, _service)
		{
			IOrderRepository = _orderRepo;
			ISysErrorRepository = _errorRepo;
		}

		[HttpPost("order")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Order()
		{
			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string customerSalt = currentUser.FindFirst("salt").Value;
			Customer thisCustomer = await IOrderRepository.GetCustomer(customerSalt);
			if (thisCustomer != null)
			{
				List<Cart> cartList = await IOrderRepository.GetAllCartAsync(thisCustomer.Id);
				if (cartList.Count > 0)
				{
					List<OrderDetail> orderDetailList = new List<OrderDetail>();
					for (int i = 0; i < cartList.Count; i++)
					{
						orderDetailList.Add(new OrderDetail()
						{
							Shop = cartList[i].Product.Shop,
							Product = cartList[i].Product,
							Quantity = cartList[i].Quantity,
							Color = cartList[i].Color,
							Status = IOrderRepository.GetOrderStatus(1),
							Paid = false,
							CreateAt = DateTime.Now
						});
					}
					Order newOrder = new Order()
					{
						Customer = thisCustomer,
						OrderTime = DateTime.Now,
						OrderDetails = orderDetailList
					};
					return Ok(await IOrderRepository.Create(newOrder));
				}
				else
				{
					return Ok(new StandardResponse()
					{
						IsSuccess = false,
						Payload = null,
						Error = new StandardError()
						{
							ErrorCode = 1408,
							ErrorMessage = ISysErrorRepository.GetName(1408)
						}
					});
				}
			}
			else
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = null,
					Error = new StandardError()
					{
						ErrorCode = 1404,
						ErrorMessage = ISysErrorRepository.GetName(1404)
					}
				});
			}
		}

		[HttpPost("addCart")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> AddCart(CartViewModel model)
		{
			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string customerSalt = currentUser.FindFirst("salt").Value;
			Customer thisCustomer = await IOrderRepository.GetCustomer(customerSalt);

			Cart newCart = new Cart()
			{
				Customer = thisCustomer,
				ProductId = model.ProductId,
				Quantity = model.Quantity,
				Color = model.Color,
				Classification = model.Classification
			};
			return Ok(await IOrderRepository.Create(newCart));
		}

		[HttpPost("orderThis")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> Order(CartViewModel model)
		{
			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string customerSalt = currentUser.FindFirst("salt").Value;

			Product product = await IOrderRepository.GetProduct(model.ProductId);
			if (product != null)
			{
				Customer customer = await IOrderRepository.GetCustomer(customerSalt);
				OrderDetail newOrderDetail = new OrderDetail()
				{
					Shop = product.Shop,
					Product = product,
					Quantity = model.Quantity,
					Color = model.Color,
					Classification = model.Classification,
					Status = IOrderRepository.GetOrderStatus(1),
					Paid = false,
					CreateAt = DateTime.Now
				};

				List<OrderDetail> orderDetailList = new List<OrderDetail>();
				orderDetailList.Add(newOrderDetail);

				Order newOrder = new Order()
				{
					Customer = customer,
					OrderTime = DateTime.Now,
					OrderDetails = orderDetailList
				};
				return Ok(await IOrderRepository.Create(newOrder));
			}
			else
			{
				return Ok(new StandardResponse()
				{
					IsSuccess = false,
					Payload = model,
					Error = new StandardError()
					{
						ErrorCode = 1786,
						ErrorMessage = ISysErrorRepository.GetName(1786)
					}
				});
			}
		}

		[HttpGet("allCart")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> GetCart()
		{
			var currentUser = HttpContext.User.Identity as ClaimsIdentity;
			string customerSalt = currentUser.FindFirst("salt").Value;

			return Ok(await IOrderRepository.GetCart(customerSalt));
		}
	}
}
