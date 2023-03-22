using Microsoft.AspNetCore.Mvc;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;
using System;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models.DTO;
using PRN211_ShoesStore.Repository.vH;
using PRN211_ShoesStore.Filter;

namespace PRN211_ShoesStore.Controllers.Admin
{
    [MyAuthenFIlter("Admin")]
    [Route("Admin/Order")]

    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderRepository _orderRepository;

		public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IOrderRepository orderRepository)
		{
			_orderService = orderService;
			_orderDetailService = orderDetailService;
			_orderRepository = orderRepository;
		}

		public IActionResult Index()
        {
            var orders = _orderService.GetAllOrder();
            return View("/ViewsAdmin/Order/Index.cshtml", orders);
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetFirstOrDefault(filter: o => o.orderId == id, includeProperties: "user");
            if (order == null)
            {
                return NotFound();
            }
			var orderDetail = _orderDetailService.GetOrderDetailFromOrderIdIncludeShoes(id);
            var OrderDetailsViewDto = new OrderDetailsViewDto
            {
                Order = order,
                OrderDetails = orderDetail
            };
			return View("/ViewsAdmin/Order/Detail.cshtml", OrderDetailsViewDto);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateStatus(ChangeOrderStatusRequest request)
		{
			var order = _orderRepository.GetFirstOrDefault(o=>o.orderId==request.OrderId);
			if (order == null)
			{
				return NotFound();
			}
			order.status = request.NewStatus;
			_orderRepository.Update(order);
			TempData["Message"] = "You've change status of a order successfully.";
			TempData["IsSuccess"] = "true";
			return RedirectToAction("Details", new { id = request.OrderId });
		}
	}
}
