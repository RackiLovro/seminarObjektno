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
    public class RecipeDifficultiesController : Controller
    {
        private readonly RecepTourContext _context;

        public RecipeDifficultiesController(RecepTourContext context)
        {
            _context = context;
        }

        // GET: RecipeDifficulties
        public async Task<IActionResult> Index()
        {
            return View(await _context.RecipeDifficulties.ToListAsync());
        }

        // GET: RecipeDifficulties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeDifficulty = await _context.RecipeDifficulties
                .FirstOrDefaultAsync(m => m.DiffLevel == id);
            if (recipeDifficulty == null)
            {
                return NotFound();
            }

            return View(recipeDifficulty);
        }

        // GET: RecipeDifficulties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeDifficulties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiffLevel,Description")] RecipeDifficulty recipeDifficulty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeDifficulty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipeDifficulty);
        }

        // GET: RecipeDifficulties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeDifficulty = await _context.RecipeDifficulties.FindAsync(id);
            if (recipeDifficulty == null)
            {
                return NotFound();
            }
            return View(recipeDifficulty);
        }

        // POST: RecipeDifficulties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiffLevel,Description")] RecipeDifficulty recipeDifficulty)
        {
            if (id != recipeDifficulty.DiffLevel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeDifficulty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeDifficultyExists(recipeDifficulty.DiffLevel))
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
            return View(recipeDifficulty);
        }

        // GET: RecipeDifficulties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeDifficulty = await _context.RecipeDifficulties
                .FirstOrDefaultAsync(m => m.DiffLevel == id);
            if (recipeDifficulty == null)
            {
                return NotFound();
            }

            return View(recipeDifficulty);
        }

        // POST: RecipeDifficulties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeDifficulty = await _context.RecipeDifficulties.FindAsync(id);
            _context.RecipeDifficulties.Remove(recipeDifficulty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeDifficultyExists(int id)
        {
            return _context.RecipeDifficulties.Any(e => e.DiffLevel == id);
        }
    }
}
