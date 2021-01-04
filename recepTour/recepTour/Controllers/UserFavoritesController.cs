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
    public class UserFavoritesController : Controller
    {
        private readonly RecepTourContext _context;

        public UserFavoritesController(RecepTourContext context)
        {
            _context = context;
        }

        // GET: UserFavorites
        public async Task<IActionResult> Index()
        {
            var d3jgof5caojknsContext = _context.UserFavorites.Include(u => u.Recipe).Include(u => u.User);
            return View(await d3jgof5caojknsContext.ToListAsync());
        }

        // GET: UserFavorites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavorite = await _context.UserFavorites
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userFavorite == null)
            {
                return NotFound();
            }

            return View(userFavorite);
        }

        // GET: UserFavorites/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserFavorites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RecipeId")] UserFavorite userFavorite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFavorite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", userFavorite.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userFavorite.UserId);
            return View(userFavorite);
        }

        // GET: UserFavorites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavorite = await _context.UserFavorites.FindAsync(id);
            if (userFavorite == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", userFavorite.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userFavorite.UserId);
            return View(userFavorite);
        }

        // POST: UserFavorites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,RecipeId")] UserFavorite userFavorite)
        {
            if (id != userFavorite.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFavorite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFavoriteExists(userFavorite.UserId))
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
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", userFavorite.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userFavorite.UserId);
            return View(userFavorite);
        }

        // GET: UserFavorites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFavorite = await _context.UserFavorites
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userFavorite == null)
            {
                return NotFound();
            }

            return View(userFavorite);
        }

        // POST: UserFavorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFavorite = await _context.UserFavorites.FindAsync(id);
            _context.UserFavorites.Remove(userFavorite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFavoriteExists(int id)
        {
            return _context.UserFavorites.Any(e => e.UserId == id);
        }
    }
}
