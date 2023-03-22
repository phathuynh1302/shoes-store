using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN211_ShoesStore.Filter;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Service;
using PRN211_ShoesStore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Controllers
{
    [MyAuthenFIlter("User")]
    public class OrderController : Controller
	{

		private readonly IOrderService _orderService;
		private readonly ICartService _cartService;
		private readonly UserService _userService;
        public OrderController(IOrderService orderService, ICartService cartService, UserService userService)
		{
			_orderService = orderService;
			_cartService = cartService;
            _userService = userService;

        }

		public IActionResult CheckOut(int cartItemId, decimal totalPrice)
		{
            int? userId = HttpContext.Session.GetInt32("UserId");
			
			User user = _userService.GetUserById(userId.Value);
			try
			{
                _orderService.CheckOut(userId, cartItemId, totalPrice);
				MailUtils.SendMail("gmail", "pwd", user.email, "Xác nhận thanh toán thành công", "Cảm ơn bạn đã đặt đơn hàng mệnh giá " + totalPrice + " đơn hàng của bạn đang trong thời gian xác nhận.");
				return View();
            }
			catch (Exception ex)
			{
				TempData["Errormsg"] = ex.Message;
			}
			
			/*if(check.Count() == 0)
			{
				return View();
			}*/
			//TempData["Errormsg"] = "Some Item in your cart are out of quatity";
			
			return RedirectToAction("Index", "Cart");
		}

		public IActionResult OrderIndex()
		{
            List<CartItemDetails> res = _cartService.GetCartItemDetails().ToList();
            if (res.Count > 0)
            {
                TempData["CartQuantity"] = res.Count;
            }
            else
            {
                TempData["CartQuantity"] = 0;
            }
            return View();
		}

		public IActionResult ViewOrder()
		{
            int? userId = HttpContext.Session.GetInt32("UserId");
			List<OrderDetail> orderDetails = _orderService.ViewOrder(userId.Value);
			return View(orderDetails);
        }
	}
}
