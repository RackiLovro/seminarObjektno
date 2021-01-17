using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using recepTour.Models;

namespace recepTour.Controllers
{
    public class RecipeGroceriesController : Controller
    {
        private readonly RecepTourContext _context;

        public RecipeGroceriesController(RecepTourContext context)
        {
            _context = context;
        }

        // GET: RecipeGroceries
        public async Task<IActionResult> Index()
        {
            var d3jgof5caojknsContext = _context.RecipeGroceries.Include(r => r.Grocery).Include(r => r.Recipe);
            return View(await d3jgof5caojknsContext.ToListAsync());
        }

        // GET: RecipeGroceries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeGrocery = await _context.RecipeGroceries
                .Include(r => r.Grocery)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipeGrocery == null)
            {
                return NotFound();
            }

            return View(recipeGrocery);
        }

        // GET: RecipeGroceries/Create
        public IActionResult Create()
        {
            RecipeGrocery grocery = TempData.Get<RecipeGrocery>("grocery");
            ViewData["GroceryId"] = new SelectList(_context.Groceries, "Id", "Name");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title", grocery.RecipeId);
            TempData.Put("grocery", grocery);
            var groceries = from groc in _context.Groceries join recgroc in _context.RecipeGroceries on groc.Id equals recgroc.GroceryId where recgroc.RecipeId == grocery.RecipeId select new {groc.Name, recgroc.Amount };
            List<String> temp = new List<String>();
            foreach(var g in groceries)
            {
                temp.Add(g.Amount +" " + g.Name);
            }
            ViewData["groceries"] = temp;
            return View(grocery);
        }

        // POST: RecipeGroceries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,GroceryId,Amount")] RecipeGrocery recipeGrocery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeGrocery);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException) {
                    ModelState.AddModelError("", "Grocery is already in the recipe!");
                    RecipeGrocery grocery = TempData.Get<RecipeGrocery>("grocery");
                    ViewData["GroceryId"] = new SelectList(_context.Groceries, "Id", "Name");
                    ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title", grocery.RecipeId);
                    TempData.Put("grocery", grocery);
                    var groceries = from groc in _context.Groceries join recgroc in _context.RecipeGroceries on groc.Id equals recgroc.GroceryId where recgroc.RecipeId == grocery.RecipeId select new { groc.Name, recgroc.Amount };
                    List<String> temp = new List<String>();
                    foreach (var g in groceries)
                    {
                        temp.Add(g.Amount + " " + g.Name);
                    }
                    ViewData["groceries"] = temp;
                    return View(recipeGrocery);
                }
                return RedirectToAction("Create", "RecipeGroceries");
            }
            ViewData["GroceryId"] = new SelectList(_context.Groceries, "Id", "Name", recipeGrocery.GroceryId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title", recipeGrocery.RecipeId);
            return View(recipeGrocery);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Continue([Bind("RecipeId,GroceryId,Amount")] RecipeGrocery recipeGrocery)
        {
            return RedirectToAction("Create", "Pictures");
        }

        // GET: RecipeGroceries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeGrocery = await _context.RecipeGroceries.FindAsync(id);
            if (recipeGrocery == null)
            {
                return NotFound();
            }
            ViewData["GroceryId"] = new SelectList(_context.Groceries, "Id", "Id", recipeGrocery.GroceryId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeGrocery.RecipeId);
            return View(recipeGrocery);
        }

        // POST: RecipeGroceries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeId,GroceryId,Amount")] RecipeGrocery recipeGrocery)
        {
            if (id != recipeGrocery.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeGrocery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeGroceryExists(recipeGrocery.RecipeId))
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
            ViewData["GroceryId"] = new SelectList(_context.Groceries, "Id", "Id", recipeGrocery.GroceryId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeGrocery.RecipeId);
            return View(recipeGrocery);
        }

        // GET: RecipeGroceries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeGrocery = await _context.RecipeGroceries
                .Include(r => r.Grocery)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipeGrocery == null)
            {
                return NotFound();
            }

            return View(recipeGrocery);
        }

        // POST: RecipeGroceries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeGrocery = await _context.RecipeGroceries.FindAsync(id);
            _context.RecipeGroceries.Remove(recipeGrocery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeGroceryExists(int id)
        {
            return _context.RecipeGroceries.Any(e => e.RecipeId == id);
        }
    }
}
