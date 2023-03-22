using Microsoft.AspNetCore.Mvc;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;
using System;
using PRN211_ShoesStore.Utils.Interface;
using PRN211_ShoesStore.Utils.Locale;
using PRN211_ShoesStore.Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using PRN211_ShoesStore.Filter;
using System.Drawing;
using PRN211_ShoesStore.Repository.vH.Interface;
using Color = PRN211_ShoesStore.Models.Entity.Color;
using System.Collections;

namespace PRN211_ShoesStore.Controllers.Admin
{
    [MyAuthenFIlter("Admin")]
    [Route("Admin/Shoes")]
    public class ShoesController : Controller
    {
        private readonly IShoesService _shoesService;
        private readonly IUploadFileService _uploadFileService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly IRepository<Models.Entity.Size> _sizeRepository;
        private readonly IRepository<Models.Entity.SpecificallyShoesSize> _specificallyShoesSizeRepository;
        private readonly IRepository<Models.Entity.SpecificallyShoes> _specificallyShoesRepository;
        private readonly IRepository<Models.Entity.CategoryShoes> _categoryShoesRepository;
        private readonly IRepository<Models.Entity.ShoesColor> _shoesColorRepository;

        public ShoesController(IShoesService shoesService, IUploadFileService uploadFileService, IImageService imageService, ICategoryService categoryService, IColorService colorService, IRepository<Models.Entity.Size> sizeRepository, IRepository<SpecificallyShoesSize> specificallyShoesSizeRepository, IRepository<SpecificallyShoes> specificallyShoesRepository, IRepository<CategoryShoes> categoryShoesRepository, IRepository<ShoesColor> shoesColorRepository)
        {
            _shoesService = shoesService;
            _uploadFileService = uploadFileService;
            _imageService = imageService;
            _categoryService = categoryService;
            _colorService = colorService;
            _sizeRepository = sizeRepository;
            _specificallyShoesSizeRepository = specificallyShoesSizeRepository;
            _specificallyShoesRepository = specificallyShoesRepository;
            _categoryShoesRepository = categoryShoesRepository;
            _shoesColorRepository = shoesColorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var shoes = _shoesService.GetAllShoes();
            Dictionary<int, long> quantitySoldByShoesId = shoes.ToDictionary(s => s.id, s => s.SpecificallyShoes.SelectMany(cs => cs.OrderDetails).Sum(od => od.quantity));
            var categories = _categoryService.GetAllCategory();
            var colors = _colorService.GetAllColor();
            var sizes = _sizeRepository.GetAll().ToList();
            var viewModel = new ShoesViewDto
            {
                Shoes = shoes,
                Categories = categories,
                Colors = colors,
                Sizes = sizes,
                CreateShoesDto = new CreateShoesDto(),
                QuantitySoldByShoesId = quantitySoldByShoesId
            };
            return View("/ViewsAdmin/Shoes/Index.cshtml", viewModel);
        }

        [HttpPost("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShoesAsync([Bind("Name, ShoesDetails, LaunchDate, Price, File, Colors, Categories")] CreateShoesDto shoes)
        {
            if (ModelState.IsValid)
            {
                string[] colorIds = Request.Form["Colors"].ToArray();
                shoes.Colors = new List<Models.Entity.Color>();
                foreach (string colorId in colorIds)
                {
                    int id = int.Parse(colorId);
                    Color color = _colorService.GetColorByColorId(id);
                    shoes.Colors.Add(color);
                }
                string[] categoryIds = Request.Form["Categories"].ToArray();
                shoes.Categories = new List<Category>();
                foreach (string categoryId in categoryIds)
                {
                    int id = int.Parse(categoryId);
                    Category category = _categoryService.GetCategoryByCategoryId(id);
                    shoes.Categories.Add(category);
                }
                if (!this._uploadFileService.CheckFileSize(shoes.File, 5))
                {
                    TempData["Error"] = CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE;
                    return RedirectToAction("Index");
                }

                if (!this._uploadFileService.CheckFileExtension(shoes.File, new string[] { "svg", "png", "jpg" }))
                {
                    TempData["Error"] = CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION;
                    return RedirectToAction("Index");
                }
                var imageUrl = this._uploadFileService.Upload(shoes.File);
                //var image = new Models.Entity.Image { ImageUrl = imageUrl };
                //this._imageService.AddImage(image);
                List<CategoryShoes> categoryShoes = shoes.Categories.Select(c => new CategoryShoes { category = c, categoryId = c.id }).ToList();
                List<ShoesColor> shoesColors = shoes.Colors.Select(c => new ShoesColor { color = c, colorId = c.Id }).ToList();
                Shoes ShoesEntity = new()
                {
                    name = shoes.Name,
                    shoesDetails = shoes.ShoesDetails,
                    launchDate = shoes.LaunchDate,
                    price = shoes.Price,
                    status = true,
                    image = imageUrl,
                    CategoryShoes = categoryShoes,
                    ShoesColors = shoesColors
                };
                if (_shoesService.GetShoesByName(ShoesEntity.name) == null)
                {
                    _shoesService.AddShoes(ShoesEntity);
                    TempData["Message"] = $"Shoes {ShoesEntity.name} added successfully.";
                }
                else
                {
                    TempData["Error"] = $"Shoes name: {ShoesEntity.name} already existed.";
                }
            }
            else
            {
                TempData["Error"] = "An error occurred while adding the Shoes. Please try again later.";
            }
            return RedirectToAction("Index");
        }


        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditShoes(CreateShoesDto shoes)
        {
            if (ModelState.IsValid)
            {
                string[] colorIds = Request.Form["Colors"].ToArray();
                shoes.Colors = new List<Color>();
                foreach (string colorId in colorIds)
                {
                    int id = int.Parse(colorId);
                    Color color = _colorService.GetColorByColorId(id);
                    shoes.Colors.Add(color);
                }
                string[] categoryIds = Request.Form["Categories"].ToArray();
                shoes.Categories = new List<Category>();
                foreach (string categoryId in categoryIds)
                {
                    int id = int.Parse(categoryId);
                    Category category = _categoryService.GetCategoryByCategoryId(id);
                    shoes.Categories.Add(category);
                }
                var imageUrl = shoes.ShoesImage;
                if (shoes.File != null && shoes.File.Length != 0)
                {
                    if (!this._uploadFileService.CheckFileSize(shoes.File, 5))
                    {
                        TempData["Error"] = CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE;
                        return RedirectToAction("Index");
                    }

                    if (!this._uploadFileService.CheckFileExtension(shoes.File, new string[] { "svg", "png", "jpg", "jpeg" }))
                    {
                        TempData["Error"] = CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION;
                        return RedirectToAction("Index");
                    }
                    imageUrl = this._uploadFileService.Upload(shoes.File);
                    //Models.Entity.Image image = new Models.Entity.Image { ImageUrl = imageUrl };
                    //this._imageService.AddImage(image);
                }
                List<CategoryShoes> categoryShoes = shoes.Categories.Select(c => new CategoryShoes { category = c }).ToList();
                List<ShoesColor> shoesColors = shoes.Colors.Select(c => new ShoesColor { color = c }).ToList();
                Shoes ShoesEntity = new()
                {
                    id = shoes.id,
                    name = shoes.Name,
                    shoesDetails = shoes.ShoesDetails,
                    launchDate = shoes.LaunchDate,
                    price = shoes.Price,
                    status = true,
                    image = imageUrl,
                    CategoryShoes = categoryShoes,
                    ShoesColors = shoesColors
                };
                _shoesService.UpdateShoes(ShoesEntity);
                TempData["Message"] = $"Shoes {ShoesEntity.name} edited successfully.";
            }
            else
            {
                TempData["Error"] = "An error occurred while editing the Shoes. Please try again later.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteShoes(int id)
        {
            Shoes Shoes = _shoesService.GetShoesByShoesId(id);
            try
            {
                _shoesService.RemoveShoes(Shoes);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Cannot delete this Shoes";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("SpecificShoes/Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSpecificShoes(CreateSpecificShoesDto createSpecificShoesDto)
        {
            if (ModelState.IsValid)
            {
                string colorId = Request.Form["Color"];
                int id = int.Parse(colorId);
                Models.Entity.Color color = _colorService.GetColorByColorId(id);
                createSpecificShoesDto.Color = color;
                string sizeId = Request.Form["Size"];
                id = int.Parse(sizeId);
                Models.Entity.Size size = _sizeRepository.GetFirstOrDefault(s => s.id == id);
                Shoes shoes = _shoesService.GetShoesByShoesId(createSpecificShoesDto.ShoesId);
                SpecificallyShoes specificallyShoes = new()
                {
                    name = createSpecificShoesDto.Name,
                    quantity = createSpecificShoesDto.Quantity,
                    price = createSpecificShoesDto.Price,
                    status = true,
                    shoesId = createSpecificShoesDto.ShoesId,
                    ColorSpecificallyShoes = new List<ColorSpecificallyShoes>() { new ColorSpecificallyShoes() { color = color, colorId = color.Id } },
                    SpecificallyShoesSize = new List<SpecificallyShoesSize>() { new SpecificallyShoesSize() { size = size, sizeId = size.id } }
                };
                List<SpecificallyShoes> specificShoesListByShoesID = _specificallyShoesRepository
                    .GetAll(filter: ss => ss.shoesId == shoes.id, includeProperties: "ColorSpecificallyShoes.color,SpecificallyShoesSize.size")
                    .ToList();
                _specificallyShoesRepository.Add(specificallyShoes);
                shoes.quantity = _specificallyShoesRepository.GetAll(filter: i => i.shoesId == shoes.id).Sum(ss => ss.quantity);
                List<ShoesColor> shoesColors = _shoesColorRepository.GetAll(filter: i => i.shoesId == shoes.id, includeProperties: "color").Select(c => new ShoesColor { color = c.color }).ToList();
                List<CategoryShoes> categoryShoes = _categoryShoesRepository.GetAll(filter: i => i.shoesId == shoes.id, includeProperties: "category").Select(cate => new CategoryShoes { category = cate.category }).ToList();
                _shoesService.UpdateShoes(shoes, categoryShoes, shoesColors);
                TempData["Message"] = $"Specific Shoes {specificallyShoes.name} added successfully.";
            }
            else
            {
                TempData["Error"] = "An error occurred while adding the Shoes. Please try again later.";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("SpecificShoes/Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateSpecificShoesDto createSpecificShoesDto)
        {
            if (ModelState.IsValid)
            {
                string colorId = Request.Form["Color"];
                int id = int.Parse(colorId);
                Models.Entity.Color color = _colorService.GetColorByColorId(id);
                createSpecificShoesDto.Color = color;
                string sizeId = Request.Form["Size"];
                id = int.Parse(sizeId);
                Models.Entity.Size size = _sizeRepository.GetFirstOrDefault(s => s.id == id);
                Shoes shoes = _shoesService.GetShoesByShoesId(createSpecificShoesDto.ShoesId);
                SpecificallyShoes specificallyShoes = new()
                {
                    id = createSpecificShoesDto.id,
                    name = createSpecificShoesDto.Name,
                    quantity = createSpecificShoesDto.Quantity,
                    price = createSpecificShoesDto.Price,
                    status = createSpecificShoesDto.Status,
                    shoesId = createSpecificShoesDto.ShoesId,
                    ColorSpecificallyShoes = new List<ColorSpecificallyShoes>() { new ColorSpecificallyShoes() { color = color, colorId = color.Id } },
                    SpecificallyShoesSize = new List<SpecificallyShoesSize>() { new SpecificallyShoesSize() { size = size, sizeId = size.id } }
                };
                _specificallyShoesRepository.Update(specificallyShoes);
                TempData["Message"] = $"Specific Shoes {specificallyShoes.name} added successfully.";
            }
            else
            {
                TempData["Error"] = "An error occurred while adding the Shoes. Please try again later.";
            }
            return RedirectToAction("Index");
        }
    }
}
