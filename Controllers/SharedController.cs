using Microsoft.AspNetCore.Mvc;
using PRN211_ShoesStore.Service;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Controllers
{
	public class SharedController : Controller
	{
		public SharedController()
		{
			
		}

		public IActionResult _Layout()
		{
			List<string> categories = new List<string> { "Category 1", "Category 2", "Category 3" };
			ViewBag.Categories = categories;
			return View();
		}
	}
}
