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
    public class GroceryTypesController : Controller
    {
        private readonly RecepTourContext _context;

        public GroceryTypesController(RecepTourContext context)
        {
            _context = context;
        }

        // GET: GroceryTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroceryTypes.ToListAsync());
        }

        // GET: GroceryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryType = await _context.GroceryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groceryType == null)
            {
                return NotFound();
            }

            return View(groceryType);
        }

        // GET: GroceryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroceryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] GroceryType groceryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groceryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groceryType);
        }

        // GET: GroceryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryType = await _context.GroceryTypes.FindAsync(id);
            if (groceryType == null)
            {
                return NotFound();
            }
            return View(groceryType);
        }

        // POST: GroceryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] GroceryType groceryType)
        {
            if (id != groceryType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groceryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryTypeExists(groceryType.Id))
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
            return View(groceryType);
        }

        // GET: GroceryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryType = await _context.GroceryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groceryType == null)
            {
                return NotFound();
            }

            return View(groceryType);
        }

        // POST: GroceryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groceryType = await _context.GroceryTypes.FindAsync(id);
            _context.GroceryTypes.Remove(groceryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroceryTypeExists(int id)
        {
            return _context.GroceryTypes.Any(e => e.Id == id);
        }
    }
}
