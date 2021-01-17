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
            var d3jgof5caojknsContext = _context.RecipeSteps.Include(r => r.Recipe);
            return View(await d3jgof5caojknsContext.ToListAsync());
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


        public IActionResult Create()
        {
            RecipeStep step = TempData.Get<RecipeStep>("step");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title", step.RecipeId);
            ViewData["StepNumber"] = step.StepNumber;
            TempData.Put("step", step);
            List<string> steps = new List<string>();
            steps = _context.RecipeSteps.Where(c => c.RecipeId == step.RecipeId).Select(c => c.Description).ToList();
            ViewData["steps"] = steps;
            return View(step);
        }

        //// GET: RecipeSteps/Create
        //public IActionResult Create()
        //{
        //    ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id");
        //    return View();
        //}

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
                RecipeStep step = TempData.Get<RecipeStep>("step");
                step.StepNumber++;
                TempData.Put("step", step);
                return RedirectToAction("Create", "RecipeSteps");
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title", recipeStep.RecipeId);
            return View(recipeStep);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Continue([Bind("Id,StepNumber,RecipeId,Description")] RecipeStep recipeStep)
        {
            RecipeGrocery grocery = new RecipeGrocery();
            grocery.RecipeId = (int)recipeStep.RecipeId;
            TempData.Put("grocery", grocery);
            return RedirectToAction("Create", "RecipeGroceries");
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
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title", recipeStep.RecipeId);
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
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Title", recipeStep.RecipeId);
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
