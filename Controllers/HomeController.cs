using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRN211_ShoesStore.Filter;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Models.DTO;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository;
using PRN211_ShoesStore.Service;
using PRN211_ShoesStore.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace PRN211_ShoesStore.Controllers
{

    public class HomeController : Controller
    {
        private IRepository<User> _userRepository;

        private UserService _userService;

        private RoleRepository roleRepository;

        private readonly IShoesService _shoesService;

        private readonly ICategoryService _categoryService;

        private readonly ILogger<HomeController> _logger;

        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger,
            IRepository<User> userRepository,
            RoleRepository _roleRepository,
            UserService userService,
            IShoesService shoesService,
            ICategoryService categoryService, ICartService cartService)
        {
            _logger = logger;
            _userRepository = userRepository;
            roleRepository = _roleRepository;
            _userService = userService;
            _shoesService = shoesService;
            _categoryService = categoryService;
            _cartService = cartService;
        }

        [MyAuthenFIlter("User")]
        public IActionResult Index(int pg = 1)
        {
            //TempData["Categories"] = JsonConvert.SerializeObject(_categoryService.GetCategories());
            //TempData["Colors"] = JsonConvert.SerializeObject(_colorService.GetAllColor());
            List<CartItemDetails> res = _cartService.GetCartItemDetails().ToList();
            if (res.Count > 0)
            {
                TempData["CartQuantity"] = res.Count;
            }
            else
            {
                TempData["CartQuantity"] = 0;
            }
            // Do something with the userId value
            var shoesList = _shoesService.GetShoes().ToList();
            const int pageSize = 4;
            if (pg < 1)
                pg = 1;

            int recsCount = shoesList.Count;
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var data = shoesList.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.pager = pager;

            return View(data);



        }

        public IActionResult AccessDenied() { return View(); }

        public IActionResult Login()
        {

            return View("Views/Home/Login.cshtml");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("Status");
            HttpContext.Session.Clear();

            return View("Views/Home/Login.cshtml");
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string username, string pwd, string confirmPwd, string phone, string email, string address)
        {

            bool flag = true;
            bool pwdEqual = String.Equals(pwd, confirmPwd, StringComparison.OrdinalIgnoreCase);
            if (pwdEqual == false)
            {
                TempData["ErrorPasswordIsNotMatch"] = "password and confirm password is not match.";
                flag = false;
            }

            if (ValidateForm.StartWithANumber(username) == true)
            {
                TempData["ErrorUsernamemailFormat"] = "Username can not start with a number.";
                flag = false;
            }

            if (ValidateForm.ContaintOnlyChar(firstName) == false)
            {
                TempData["ErrorFistName"] = "Firstname can not contain number.";
                flag = false;
            }

            if (ValidateForm.ContaintOnlyChar(lastName) == false)
            {
                TempData["ErrorlastName"] = "LastName can not contain number.";
                flag = false;
            }

            if (ValidateForm.IsValidEmail(email) == false)
            {
                TempData["ErrorEmailFormat"] = "Email is wrong format buikhoinguyen2001@gmail.com.";
                flag = false;
            }

            if (ValidateForm.StartWithANumber(email) == true)
            {
                TempData["ErrorEmailFormat"] = "Email can not start with a number.";
                flag = false;
            }

            if (ValidateForm.IsValidPhone(phone) == false)
            {
                TempData["ErrorPhoneFormat"] = "Phone must be a string number with 10 digits.";
                flag = false;
            }

            if (_userService.checkUsernameIsExisted(username) != null)
            {
                TempData["ErrorUsername"] = "Username is existed.";
                flag = false;
            }

            bool res = MailUtils.SendMail("nguyenbkse151446@fpt.edu.vn", email, "Mail Xac Nhan Email Da dang ky thanh cong", "Hello Mr/Mrs " + firstName);
            if (res == false)
            {
                TempData["ErrorEmailFormat"] = "Email is not existed";
                flag = false;
            }

            if (flag)
            {
                User user = _userService.Register(firstName + " " + lastName, username, pwd, phone, email, address);

                return View("Views/Home/Login.cshtml");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.login(username, password);
            if (user == null)
            {
                TempData["ErrorLogin"] = "Username or password is invalid.";
                return RedirectToAction("Login", "Home");
            }
            if (user.id > 0)
            {
                if (user.role.roleName.Equals("User"))
                {
                    HttpContext.Session.SetInt32("UserId", user.id);
                    HttpContext.Session.SetString("Role", user.role.roleName);
                    HttpContext.Session.SetString("Status", user.status.ToString());


                    return RedirectToAction("Index", "Home");
                }
                else if (user.role.roleName.Equals("Admin"))
                {
                    HttpContext.Session.SetInt32("UserId", user.id);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["ErrorLogin"] = "Your role is not permited.";
                }
            }
            TempData["ErrorLogin"] = "Username or password is invalid.";
            return RedirectToAction("Login", "Home");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}