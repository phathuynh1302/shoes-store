using Microsoft.AspNetCore.Mvc;
using PRN211_ShoesStore.Filter;

namespace PRN211_ShoesStore.Controllers.Admin
{
    [MyAuthenFIlter("Admin")]
    [Route("/Admin")]
    public class AdminController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("/ViewsAdmin/Home/Index.cshtml");
        }
    }
}
