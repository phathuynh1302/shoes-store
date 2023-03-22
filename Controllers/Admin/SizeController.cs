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
//    [Route("admin/size")]
//    public class SizeController : Controller
//    {
//        private readonly AppDbContext _context;
//        private readonly ISizeService _sizeService;

//        public SizeController(AppDbContext context, ISizeService sizeService)
//        {
//            this._context = context;
//            _sizeService = sizeService;
//        }
//        public IActionResult Index()
//        {
//            List<Size> sizes = _sizeService.GetAllSize();
//            return View("/ViewsAdmin/Containers/Size.cshtml", sizes);
//        }

//        [HttpPost("add")]
//        [ValidateAntiForgeryToken]
//        public IActionResult AddSize([Bind("sizeNumber")] Size size)
//        {
//            if (ModelState.IsValid)
//            {
//                Size SizeEntity = new()
//                {
//                    sizeNumber = size.sizeNumber,
//                    status = true
//                };
//                if (_sizeService.GetSizeByName(SizeEntity.sizeNumber) == null)
//                {
//                    _sizeService.AddSize(SizeEntity);
//                    TempData["Message"] = "Size added successfully.";
//                }
//                else
//                {
//                    TempData["Error"] = $"Size sizeNumber: {SizeEntity.sizeNumber} already existed.";
//                }
//            }
//            else
//            {
//                TempData["Error"] = "An error occurred while adding the Size. Please try again later.";
//            }
//            return RedirectToAction("Index");
//        }
//        [HttpPost("edit")]
//        [ValidateAntiForgeryToken]
//        public IActionResult EditSize(Size Size)
//        {
//            if (ModelState.IsValid)
//            {
//                Size SizeEntity = new()
//                {
//                    sizeNumber = Size.sizeNumber,
//                    status = Size.status,
//                    id = Size.id
//                };
//                _sizeService.UpdateSize(SizeEntity);
//                TempData["Message"] = $"Size {SizeEntity.sizeNumber} edited successfully.";
//            }
//            else
//            {
//                TempData["Error"] = "An error occurred while editing the Size. Please try again later.";
//            }
//            return RedirectToAction("Index");
//        }

//        [HttpPost("delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteSize(int id)
//        {
//            Size Size = _sizeService.GetSizeBySizeId(id);
//            try
//            {
//                _sizeService.RemoveSize(Size);
//                TempData["Message"] = $"Size {Size.sizeNumber} deleted successfully.";
//            }
//            catch (Exception ex)
//            {
//                TempData["Message"] = "Cannot delete this Size";
//            }
//            return RedirectToAction("Index");
//        }
//    }
//}
