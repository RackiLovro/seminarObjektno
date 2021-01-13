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
    public class GroceriesController : Controller
    {
        private readonly RecepTourContext _context;

        public GroceriesController(RecepTourContext context)
        {
            _context = context;
        }

        // GET: Groceries
        public async Task<IActionResult> Index()
        {
            var d3jgof5caojknsContext = _context.Groceries.Include(g => g.Type);
            return View(await d3jgof5caojknsContext.ToListAsync());
        }

        // GET: Groceries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocery = await _context.Groceries
                .Include(g => g.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grocery == null)
            {
                return NotFound();
            }

            return View(grocery);
        }

        // GET: Groceries/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.GroceryTypes, "Id", "Id");
            return View();
        }

        // POST: Groceries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeId")] Grocery grocery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grocery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.GroceryTypes, "Id", "Id", grocery.TypeId);
            return View(grocery);
        }

        // GET: Groceries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocery = await _context.Groceries.FindAsync(id);
            if (grocery == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.GroceryTypes, "Id", "Id", grocery.TypeId);
            return View(grocery);
        }

        // POST: Groceries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId")] Grocery grocery)
        {
            if (id != grocery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grocery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryExists(grocery.Id))
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
            ViewData["TypeId"] = new SelectList(_context.GroceryTypes, "Id", "Id", grocery.TypeId);
            return View(grocery);
        }

        // GET: Groceries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocery = await _context.Groceries
                .Include(g => g.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grocery == null)
            {
                return NotFound();
            }

            return View(grocery);
        }

        // POST: Groceries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grocery = await _context.Groceries.FindAsync(id);
            _context.Groceries.Remove(grocery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroceryExists(int id)
        {
            return _context.Groceries.Any(e => e.Id == id);
        }
    }
}
