using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Service;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Controllers
{
	public class OrderController : Controller
	{

		private readonly IOrderService _orderService;
		private readonly ICartService _cartService;
		public OrderController(IOrderService orderService, ICartService cartService)
		{
			_orderService = orderService;
			_cartService = cartService;
		}

		public IActionResult CheckOut()
		{
			var userId = HttpContext.Session.GetInt32("UserId");

			List<CartItemDetails> check = _orderService.checkQuantity((int)userId);
			if(check.Count() == 0)
			{
				Order newOrder = _orderService.CreateOrder((int)userId, 0);
				//List<CartItemDetails> res = _cartService.GetCartItemDetails().ToList();
				foreach (var a in check)
				{
					_orderService.CreateOrderDetail(a.Quantity, (double)a.Price, a.specificallyShoesId, newOrder.orderId);
				}
				return View();
			}
			TempData["Errormsg"] = "Some Item in your cart are out of quatity";
			return RedirectToAction("Index", "Cart", new { area = "" });
		}

		public IActionResult OrderIndex()
		{
			return View();
		}

	}
}
