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
    public class UserRecipesController : Controller
    {
        private readonly d3jgof5caojknsContext _context;

        public UserRecipesController(d3jgof5caojknsContext context)
        {
            _context = context;
        }

        // GET: UserRecipes
        public async Task<IActionResult> Index()
        {
            var d3jgof5caojknsContext = _context.UserRecipes.Include(u => u.Recipe).Include(u => u.User);
            return View(await d3jgof5caojknsContext.ToListAsync());
        }

        // GET: UserRecipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRecipe = await _context.UserRecipes
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userRecipe == null)
            {
                return NotFound();
            }

            return View(userRecipe);
        }

        // GET: UserRecipes/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserRecipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RecipeId")] UserRecipe userRecipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", userRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRecipe.UserId);
            return View(userRecipe);
        }

        // GET: UserRecipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRecipe = await _context.UserRecipes.FindAsync(id);
            if (userRecipe == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", userRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRecipe.UserId);
            return View(userRecipe);
        }

        // POST: UserRecipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,RecipeId")] UserRecipe userRecipe)
        {
            if (id != userRecipe.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRecipeExists(userRecipe.UserId))
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
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", userRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userRecipe.UserId);
            return View(userRecipe);
        }

        // GET: UserRecipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRecipe = await _context.UserRecipes
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userRecipe == null)
            {
                return NotFound();
            }

            return View(userRecipe);
        }

        // POST: UserRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRecipe = await _context.UserRecipes.FindAsync(id);
            _context.UserRecipes.Remove(userRecipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRecipeExists(int id)
        {
            return _context.UserRecipes.Any(e => e.UserId == id);
        }
    }
}
