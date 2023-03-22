using Microsoft.AspNetCore.Mvc;

namespace PRN211_ShoesStore.Controllers.Admin
{
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
