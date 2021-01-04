using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
            var recipeGroceries = _context.RecipeGroceries.Select(rg => new
            {
                rg.RecipeId,
                rg.GroceryId,
                rg.Amount
            });

            return Json(await recipeGroceries.ToListAsync(), new JsonSerializerOptions());
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
            ViewData["GroceryId"] = new SelectList(_context.Groceries, "Id", "Id");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id");
            return View();
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
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroceryId"] = new SelectList(_context.Groceries, "Id", "Id", recipeGrocery.GroceryId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeGrocery.RecipeId);
            return View(recipeGrocery);
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
