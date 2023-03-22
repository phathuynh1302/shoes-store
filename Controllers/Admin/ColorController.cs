//using Microsoft.AspNetCore.Mvc;
//using PRN211_ShoesStore.Models;
//using PRN211_ShoesStore.Models.Entity;
//using PRN211_ShoesStore.Repository.vH.Interface;
//using PRN211_ShoesStore.Service.vH.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace PRN211_ShoesStore.Controllers.Admin
//{
//    [Route("admin/Color")]
//    public class ColorController : Controller
//    {
//        private readonly AppDbContext _context;
//        private readonly IColorService _colorService;

//        public ColorController(AppDbContext context, IColorService colorService)
//        {
//            this._context = context;
//            _colorService = colorService;
//        }
//        public IActionResult Index()
//        {
//            List<Color> colors = _colorService.GetAllColor();
//            return View("/ViewsAdmin/Containers/Color.cshtml", colors);
//        }

//        [HttpPost("add")]
//        [ValidateAntiForgeryToken]
//        public IActionResult AddColor([Bind("Name")] Color color)
//        {
//            if (ModelState.IsValid)
//            {
//                Color colorEntity = new()
//                {
//                    Name = color.Name,
//                    status = true
//                };
//                if (_colorService.GetColorByName(colorEntity.Name) == null)
//                {
//                    _colorService.AddColor(colorEntity);
//                    TempData["Message"] = "Color added successfully.";
//                }
//                else
//                {
//                    TempData["Error"] = $"Color ColorNumber: {colorEntity.Name} already existed.";
//                }
//            }
//            else
//            {
//                TempData["Error"] = "An error occurred while adding the Color. Please try again later.";
//            }
//            return RedirectToAction("Index");
//        }
//        [HttpPost("edit")]
//        [ValidateAntiForgeryToken]
//        public IActionResult EditColor(Color color)
//        {
//            if (ModelState.IsValid)
//            {
//                Color ColorEntity = new()
//                {
//                    Name = color.Name,
//                    status = color.status,
//                    Id = color.Id
//                };
//                _colorService.UpdateColor(ColorEntity);
//                TempData["Message"] = $"Color {ColorEntity.Name} edited successfully.";
//            }
//            else
//            {
//                TempData["Error"] = "An error occurred while editing the Color. Please try again later.";
//            }
//            return RedirectToAction("Index");
//        }

//        [HttpPost("delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteColor(int id)
//        {
//            Color Color = _colorService.GetColorByColorId(id);
//            try
//            {
//                _colorService.RemoveColor(Color);
//                TempData["Message"] = $"Color {Color.Name} deleted successfully.";
//            }
//            catch (Exception ex)
//            {
//                TempData["Message"] = "Cannot delete this Color";
//            }
//            return RedirectToAction("Index");
//        }
//    }
//}
