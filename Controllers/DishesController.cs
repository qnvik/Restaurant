using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace restauran.Data
{
    public class DishesController : Controller
    {
        private readonly Context _context;

        public DishesController(Context context)
        {
            _context = context;
        }

        // GET: Dishes
        public async Task<IActionResult> Index(string sortOrder, string searchString, string searchString1)
        {
            var context = _context.Dish.Include(d => d.Meal).Include(d => d.Restaurant);


                ViewData["NameSortParm1"] = String.IsNullOrEmpty(sortOrder) ? "date_desc1" : "1";
                ViewData["NameSortParm2"] = String.IsNullOrEmpty(sortOrder) ? "date_desc2" : "2";
                ViewData["NameSortParm3"] = String.IsNullOrEmpty(sortOrder) ? "date_desc3" : "3";
                ViewData["NameSortParm4"] = String.IsNullOrEmpty(sortOrder) ? "date_desc4" : "4";
                ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilter1"] = searchString1;
            var students = from s in context
                               select s;
            if (!String.IsNullOrEmpty(searchString1))
            {
                students = students.Where(s => s.Restaurant.Name.Contains(searchString1));
            }
            if (!String.IsNullOrEmpty(searchString))
                {
                    students = students.Where(s => s.name.Contains(searchString));
                }
                switch (sortOrder)
                {

                    case "1":
                        students = students.OrderBy(s => s.name);
                        break;
                    case "2":
                        students = students.OrderBy(s => s.price);
                        break;
                    case "3":
                        students = students.OrderBy(s => s.Meal.name);
                        break;
                    case "4":
                        students = students.OrderBy(s => s.Restaurant.Name);
                        break;
                    case "date_desc1":
                        students = students.OrderByDescending(s => s.name);
                        break;
                    case "date_desc2":
                        students = students.OrderByDescending(s => s.price);
                        break;
                    case "date_desc3":
                        students = students.OrderByDescending(s => s.Meal.name);
                        break;
                    case "date_desc4":
                        students = students.OrderByDescending(s => s.Restaurant.Name);
                        break;
                    default:
                        students = students.OrderBy(s => s.name);
                        break;
                }
                return View(await students.AsNoTracking().ToListAsync());
            }
        

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish
                .Include(s => s.Orders)
                .Include(d => d.Meal)
                .Include(d => d.Restaurant)
                .FirstOrDefaultAsync(m => m.DishID == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            ViewData["MealID"] = new SelectList(_context.Meal, "MealID", "MealID");
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "RestaurantID");
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishID,MealID,RestaurantID,name,price")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealID"] = new SelectList(_context.Meal, "MealID", "MealID", dish.MealID);
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "RestaurantID", dish.RestaurantID);
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewData["MealID"] = new SelectList(_context.Meal, "MealID", "name", dish.MealID);
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "Name", dish.RestaurantID);
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishID,MealID,RestaurantID,name,price")] Dish dish)
        {
            if (id != dish.DishID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishID))
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
            ViewData["MealID"] = new SelectList(_context.Meal, "MealID", "MealID", dish.MealID);
            ViewData["RestaurantID"] = new SelectList(_context.Restaurant, "RestaurantID", "RestaurantID", dish.RestaurantID);
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dish
                .Include(d => d.Meal)
                .Include(d => d.Restaurant)
                .FirstOrDefaultAsync(m => m.DishID == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dish.FindAsync(id);
            _context.Dish.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dish.Any(e => e.DishID == id);
        }
    }
}
