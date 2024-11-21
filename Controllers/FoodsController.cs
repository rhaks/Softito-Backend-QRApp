﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMenuUygulama.Data;
using QRMenuUygulama.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QRMenuUygulama.Controllers
{
    public class FoodsController : Controller
    {
        private readonly ApplicationContext _context;

        public FoodsController(ApplicationContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            IQueryable<Food> foods = _context.Foods!;
            int? userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                foods = foods.Where(f => f.StateId == 1);
            }
            ViewData["userId"] = userId;
            return View(foods.ToList());
        }

        public ActionResult Details(int id)
        {
            Food? food = _context.Foods!.Where(f => f.Id == id).Include(f => f.State).FirstOrDefault();

            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        public ViewResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,Price,Description,StateId,CategoryId")] Food food, IFormFile picture)
        {
            FileStream fileStream;
            if (ModelState.IsValid)
            {
                _context.Add(food);
                _context.SaveChanges();
                fileStream = new FileStream(food.Id.ToString() + ".jpg", FileMode.CreateNew);
                picture.CopyTo(fileStream);
                fileStream.Close();
                return RedirectToAction(nameof(Index));

            }
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", food.StateId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", food.CategoryId);
            return View(food);
        }

        public ActionResult Edit(int id)
        {
            Food? food = _context.Foods!.Find(id);

            if (food == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", food.StateId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", food.CategoryId);
            return View(food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name,Price,Description,StateId,CategoryId")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Update(food);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", food.StateId);
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", food.CategoryId);
            return View(food);
        }

        public ActionResult Delete(int id)
        {
            Food? food = _context.Foods!.Where(f => f.Id == id).Include(f => f.State).FirstOrDefault();

            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = _context.Foods!.Find(id)!;

            food.StateId = 0;
            _context.Foods.Update(food);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

