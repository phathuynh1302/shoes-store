﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN211_ShoesStore.Filter;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Models.DTO;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Service;

namespace PRN211_ShoesStore.Controllers
{
	[MyAuthenFIlter("User")]
	public class ShoesController : Controller
	{
		private readonly AppDbContext _context = new AppDbContext();
		private readonly IShoesService _shoesService;
		private readonly ICartService _cartService;
		public ShoesController(IShoesService shoesService, ICartService cartService)
		{
			_shoesService = shoesService;
			_cartService = cartService;
		}

		// GET: Shoes - da duoc chinh sua
		public IActionResult Index(int pg = 1)
		{
			List<CartItemDetails> res = _cartService.GetCartItemDetails().ToList();
			if (res.Count > 0)
			{
				TempData["CartQuantity"] = res.Count;
			}
			else
			{
				TempData["CartQuantity"] = 0;
			}
			var shoesList = _shoesService.GetShoes().ToList();
			const int pageSize = 8;
			if (pg < 1)
				pg = 1;

			int recsCount = shoesList.Count;
			var pager = new Pager(recsCount, pg, pageSize);
			int recSkip = (pg - 1) * pageSize;

			var data = shoesList.Skip(recSkip).Take(pager.PageSize).ToList();

			this.ViewBag.pager = pager;



			return View(data);
		}
		//public IActionResult SortShoeByCategory()
		//{
		//    //return View(_shoesService.GetShoes());
		//    return View(_shoesService.GetShoesByCategoryId(1));
		//}

		// GET: Shoes/Details/5 - da chinh sua
		public async Task<IActionResult> Details(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var shoes = _shoesService.GetShoesById(id);
			if (shoes == null)
			{
				return NotFound();
			}

			return View(shoes);
		}

		public ActionResult Search(string searchTerm)
		{
			// Perform a search for the specified searchTerm
			// and return the results to the view

			if (searchTerm == null)
			{
				return View("Index");
			}

			var results = _shoesService.GetShoes()
				.Where(p => p.name.Contains(searchTerm))
				.ToList();

			return View("Index", results);
		}


		// GET: Shoes/Create
		public IActionResult Create()
		{
			return View();
		}


		//chua chinh sua
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("id,name,price,shoesDetails,launchDate,status,quantity")] Shoes shoes)
		{
			if (ModelState.IsValid)
			{
				_context.Add(shoes);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(shoes);
		}

		//chua chinh sua
		// GET: Shoes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var shoes = await _context.shoes.FindAsync(id);
			if (shoes == null)
			{
				return NotFound();
			}
			return View(shoes);
		}

		//chua chinh sua
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("id,name,price,shoesDetails,launchDate,status,quantity")] Shoes shoes)
		{
			if (id != shoes.id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(shoes);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ShoesExists(shoes.id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(shoes);
		}

		//da chinh sua
		private bool ShoesExists(int id)
		{
			var shoes = _shoesService.GetShoesById(id);
			if (shoes == null)
			{
				return false;
			}
			return true;

		}
	}
}