
using Microsoft.AspNetCore.Mvc;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository;
using PRN211_ShoesStore.Service;

namespace PRN211_ShoesStore.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;

        private RoleRepository _roleRepository; 

        public UserController(UserService userService, RoleRepository roleRepository)
        {
            _userService = userService;
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*[HttpGet]
        public IActionResult Search(string name)
        {
            return RedirectToAction("ShowShoes  ", _userService.Search(name));
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShowShoes()
        {
            return View(_userService.ShowShoes());
        }*/
    }
}
