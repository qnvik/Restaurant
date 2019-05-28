using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace restauran.Controllers
{
    public class MealsController : Controller
    {
        private readonly Context _context;

        public MealsController(Context context)
        {
            _context = context;
        }

        // GET: Meals
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            var students = from s in _context.Meal
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.name);
                    break;
              
                default:
                    students = students.OrderBy(s => s.name);
                    break;
            }
            return View(await students.AsNoTracking().ToListAsync());
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
        .Include(s => s.Enrollments)
            .ThenInclude(e => e.Restaurant)
            
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.MealID == id);

            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealID,name")] Meal meal)
        {
            try
            {
                
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(meal);
        }

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Meal.FirstOrDefaultAsync(s => s.MealID == id);
            if (await TryUpdateModelAsync<Meal>(
                studentToUpdate,
                "",
                s => s.name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        public ViewResult DltPopUp()
        {
            return View(@"~/Views/Home/_DltProhibited.cshtml");
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.MealID == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Meal.FindAsync(id);
            var a = (from o in _context.Dish where (o.MealID == dish.MealID) select o).Any();
            if (a)
            {
                return DltPopUp();
            }
            else
            {
                _context.Meal.Remove(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.MealID == id);
        }
    }
}
