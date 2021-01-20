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
            var userFavorites = _context.UserFavorites.Include(u => u.Recipe).Include(u => u.User);
            return View(await userFavorites.ToListAsync());
        }

        // GET: UserFavorites/userId
        public async Task<IActionResult> Favorites(int? id)
        {
            var userFavorites = _context.UserFavorites.Where(uf => uf.UserId == id).Include(uf => uf.Recipe);
            return View(await userFavorites.ToListAsync());
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
        public async Task<IActionResult> Create(int? userId, int? recipeId)
        {
            if (userId != null && recipeId != null)
            {
                var userFavorite = new UserFavorite
                {
                    UserId = (int)userId,
                    RecipeId = (int)recipeId
                };
                _context.Add(userFavorite);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Recipes");
                //ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", userFavorite.RecipeId);
                //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userFavorite.UserId);
                //return View(userFavorite);
            } else
            {
                return RedirectToAction("Index", "Recipes");
            }
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

        /*
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
        }*/

        // GET: UserFavorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? userId, string? recipeId)
        {
            int userIdInt = Int32.Parse(userId);
            int recipeIdInt = Int32.Parse(recipeId);
            var userFavorite = await _context.UserFavorites.Where(uf => uf.UserId == userIdInt && uf.RecipeId == recipeIdInt).FirstOrDefaultAsync();
            _context.UserFavorites.Remove(userFavorite);
            await _context.SaveChangesAsync();
            return RedirectToAction("Favorites", "UserFavorites", new { id = userIdInt});
        }

        private bool UserFavoriteExists(int id)
        {
            return _context.UserFavorites.Any(e => e.UserId == id);
        }
    }
}
