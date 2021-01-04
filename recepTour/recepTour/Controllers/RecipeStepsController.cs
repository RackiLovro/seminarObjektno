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
    public class RecipeStepsController : Controller
    {
        private readonly RecepTourContext _context;

        public RecipeStepsController(RecepTourContext context)
        {
            _context = context;
        }

        // GET: RecipeSteps
        public async Task<IActionResult> Index()
        {
            var recipeSteps = _context.RecipeSteps.Select(rs => new
            {
                rs.Id,
                rs.StepNumber,
                rs.RecipeId,
                rs.Description
            });

            return Json(await recipeSteps.ToListAsync(), new JsonSerializerOptions());
        }

        // GET: RecipeSteps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeStep = await _context.RecipeSteps
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeStep == null)
            {
                return NotFound();
            }

            return View(recipeStep);
        }

        // GET: RecipeSteps/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id");
            return View();
        }

        // POST: RecipeSteps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StepNumber,RecipeId,Description")] RecipeStep recipeStep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeStep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeStep.RecipeId);
            return View(recipeStep);
        }

        // GET: RecipeSteps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeStep = await _context.RecipeSteps.FindAsync(id);
            if (recipeStep == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeStep.RecipeId);
            return View(recipeStep);
        }

        // POST: RecipeSteps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StepNumber,RecipeId,Description")] RecipeStep recipeStep)
        {
            if (id != recipeStep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeStep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeStepExists(recipeStep.Id))
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
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeStep.RecipeId);
            return View(recipeStep);
        }

        // GET: RecipeSteps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeStep = await _context.RecipeSteps
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeStep == null)
            {
                return NotFound();
            }

            return View(recipeStep);
        }

        // POST: RecipeSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeStep = await _context.RecipeSteps.FindAsync(id);
            _context.RecipeSteps.Remove(recipeStep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeStepExists(int id)
        {
            return _context.RecipeSteps.Any(e => e.Id == id);
        }
    }
}
