using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Controllers.Admin
{
    [Route("Admin/User")]
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly ILogger<HomeController> _logger;

        public UserController(IRepository<User> userRepository, ILogger<HomeController> logger, IRepository<OrderDetail> orderDetailRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _orderDetailRepository = orderDetailRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.GetAll(options: u => u.OrderByDescending(i => i.createDate).ToList(), includeProperties: "Orders");
            List<(User, long)> userWithProductSold = new List<(User, long)>();
            foreach (var user in users)
            {
                long countProduct = 0;
                foreach (var order in user.Orders)
                {
                    countProduct += _orderDetailRepository.GetAll(filter: o => o.orderId == order.orderId).Sum(x => x.quantity);
                }
                userWithProductSold.Add((user, countProduct));
            }
            return View("/ViewsAdmin/User/Index.cshtml", userWithProductSold);
        }
    }
}
