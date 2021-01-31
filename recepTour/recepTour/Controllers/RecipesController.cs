using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using recepTour.Models;
using recepTour.ViewModels;

namespace recepTour.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RecepTourContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RecipesController(RecepTourContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AllRecipes()
        {
            var recipes = from r in _context.Recipes
                          join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                          select new RecipeViewModel
                          {
                              Id = r.Id,
                              Title = r.Title,
                              DiffLevelName = r.DiffLevel.Description,
                              User = ur.User.Nickname
                          };

            return View("Index", await recipes.ToListAsync());
        }

        // GET: Recipes
        public async Task<IActionResult> Index(string title)
        {
            var recipes = from r in _context.Recipes join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                          select new RecipeViewModel
                          {
                              Id = r.Id,
                              Title = r.Title,
                              DiffLevelName = r.DiffLevel.Description,
                              User = ur.User.Nickname
                          };

            if (!String.IsNullOrEmpty(title))
            {
                recipes = recipes.Where(r => r.Title.ToUpper().Contains(title.ToUpper()));
            }
            return View(await recipes.ToListAsync());
        }

        // GET: Recipes
        public async Task<IActionResult> ByUser(string userName)
        {
            var recipes = from r in _context.Recipes
                          join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                          select new RecipeViewModel
                          {
                              Id = r.Id,
                              Title = r.Title,
                              DiffLevelName = r.DiffLevel.Description,
                              User = ur.User.Nickname
                          };

            if(!String.IsNullOrEmpty(userName))
            {
                //var userId = _context.Users.Where(u => userName.Equals(u.Nickname)).FirstOrDefaultAsync().Result.Id;
                recipes = recipes.Where(r => r.User.ToUpper().Contains(userName.ToUpper()));
            }
            return View("Index", await recipes.ToListAsync());
        }

        // GET: Recipes
        public async Task<IActionResult> ByIngredients(string[] ingredients)
        {
            /*
            var includeGroceries = from g in _context.Groceries
                                   where ingredients.Contains(g.Id.ToString())
                                   select g;
           
            var recipes = from r in _context.Recipes
                          join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                          join rg in _context.RecipeGroceries on r.Id equals rg.RecipeId
                          join g in _context.Groceries on rg.GroceryId equals g.Id
                          where includeGroceries.Contains(g)
                          select new RecipeViewModel
                          {
                              Id = r.Id,
                              Title = r.Title,
                              DiffLevelName = r.DiffLevel.Description,
                              User = ur.User.Nickname
                          };
            */

            var ingredientsWeHave = new List<string>();
            foreach(var ing in ingredients)
            {
                var id = Int32.Parse(ing);
                ingredientsWeHave.Add(_context.Groceries.Find(id).Name);
            }
            Dictionary<int, List<string>> recipesGroceriesMap = new Dictionary<int, List<string>>();
            var availableRecipes = new List<int>();
            // Fill recipe IDs
            foreach(var recipe in await _context.Recipes.ToListAsync())
            {
                recipesGroceriesMap.Add(recipe.Id, new List<string>());
            }

            // Fill groceries
            foreach(var recipeGrocery in await _context.RecipeGroceries.ToListAsync())
            {
                var groceryName = (from rg in _context.RecipeGroceries join g in _context.Groceries on recipeGrocery.GroceryId equals g.Id select g.Name).FirstOrDefault();
                recipesGroceriesMap[recipeGrocery.RecipeId].Add(groceryName);
            }

            foreach(var entry in recipesGroceriesMap)
            {
                bool enough = !entry.Value.Except(ingredientsWeHave).Any();
                if(enough)
                {
                    availableRecipes.Add(entry.Key);
                }
            }

            var reps = from r in _context.Recipes
                       where availableRecipes.Contains(r.Id)
                       join ur in _context.UserRecipes on r.Id equals ur.RecipeId
                       select new RecipeViewModel
                        {
                            Id = r.Id,
                            Title = r.Title,
                            DiffLevelName = r.DiffLevel.Description,
                            User = ur.User.Nickname
                        };

            return View("Index", await reps.Distinct().ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.DiffLevel)
                .FirstOrDefaultAsync(m => m.Id == id);

            recipe.RecipeSteps = _context.RecipeSteps
                .Where(m => m.RecipeId == recipe.Id).ToHashSet<RecipeStep>();

            recipe.RecipeGroceries = _context.RecipeGroceries
                .Include(c => c.Grocery)
                .Where(m => m.RecipeId == recipe.Id)
                .ToHashSet<RecipeGrocery>();

            recipe.UserRecipes = _context.UserRecipes
                .Include(c => c.User)
                .Where(m => m.RecipeId == recipe.Id)
                .ToHashSet<UserRecipe>();

            recipe.Pictures = _context.Pictures
                .Where(c => c.RecipeId == recipe.Id).ToHashSet<Picture>();

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["DiffLevelId"] = new SelectList(_context.RecipeDifficulties, "DiffLevel", "Description");
                return View();
            }
            else {
                ModelState.AddModelError("", "You have to register if you want to add a recipe!");
                return View();
            }
        }

        // GET: Recipes/My/userId
        public async Task<IActionResult> My(int? userId, string? title)
        {
            var recipes = from r in _context.Recipes join ur in _context.UserRecipes on r.Id equals ur.RecipeId where ur.UserId == userId select r;
            recipes = recipes.Include(r => r.DiffLevel);

            if(!String.IsNullOrEmpty(title))
            {
                recipes = recipes.Where(r => r.Title.Contains(title));
            }

            return View(await recipes.ToListAsync());
        }


        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DiffLevelId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                RecipeStep step = new RecipeStep();
                step.RecipeId = recipe.Id;
                step.StepNumber = 1;
                TempData.Put("step", step);
                return RedirectToAction("Create", "RecipeSteps");
            }
            ViewData["DiffLevelId"] = new SelectList(_context.RecipeDifficulties, "DiffLevel", "Description", recipe.DiffLevelId);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                int loggedUserId = Convert.ToInt32(_userManager.FindByNameAsync(User.Identity.Name).Result.Id);

                if (_context.UserRecipes.Where(u => u.UserId == loggedUserId).Count() == 0)
                {
                    ModelState.AddModelError("", "You can't change details for a recipe you don't own!");
                    return View();
                }

                var recipe = await _context.Recipes.FindAsync(id);
                if (recipe == null)
                {
                    return NotFound();
                }
                ViewData["DiffLevelId"] = new SelectList(_context.RecipeDifficulties, "DiffLevel", "Description", recipe.DiffLevelId);
                return View(recipe);
            }

                            ModelState.AddModelError("", "Unauthorized action");
                return View();
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DiffLevelId")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
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
            ViewData["DiffLevelId"] = new SelectList(_context.RecipeDifficulties, "DiffLevel", "Description", recipe.DiffLevelId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.DiffLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}



public static class TempDataExtensions
{
    public static void Put<T>(this Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonConvert.SerializeObject(value);
    }

    public static T Get<T>(this Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataDictionary tempData, string key) where T : class
    {
        object o;
        tempData.TryGetValue(key, out o);
        return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
    }
}
