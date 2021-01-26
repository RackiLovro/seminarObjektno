using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using recepTour.Controllers;
using recepTour.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace RecepTourTesting
{
    

    [TestClass]
    public class RecipeContextTests
    {
        public RecepTourContext context;

        [TestInitialize]
        public void Setup()
        {
            var recipes = Utils.GetTestRecipes();
            var queryableRecipes = recipes.AsQueryable();

            var set = new Mock<DbSet<Recipe>>();
            set.As<IQueryable<Recipe>>().Setup(s => s.Expression).Returns(queryableRecipes.Expression);
            set.As<IQueryable<Recipe>>().Setup(s => s.ElementType).Returns(queryableRecipes.ElementType);
            set.As<IQueryable<Recipe>>().Setup(s => s.GetEnumerator()).Returns(queryableRecipes.GetEnumerator);
            set.As<IQueryable<Recipe>>().Setup(s => s.Provider).Returns(queryableRecipes.Provider);

            set.Setup(m => m.Add(It.IsAny<Recipe>())).Callback((Recipe recipe) => recipes.Add(recipe));
            set.Setup(m => m.Remove(It.IsAny<Recipe>())).Callback((Recipe recipe) => recipes.Remove(recipe));

            var mockContext = new Mock<RecepTourContext>();
            mockContext.Setup(m => m.Recipes).Returns(set.Object);
            context = mockContext.Object;
        }

        [TestMethod]
        public void Test_Recipes_AddRecipe()
        {
            var recipes = context.Recipes;

            var newRecipe = new Recipe
            {
                Id = 3,
                Title = "Novi",
                DiffLevelId = 2
            };

            recipes.Add(newRecipe);
            context.SaveChanges();

            Assert.AreEqual(context.Recipes.Count(), 4);
        }

        [TestMethod]
        public void Test_Recipes_DeleteRecipe()
        {
            var recipes = context.Recipes;
            var idToDelete = 2;

            var recipeToDelete = recipes.Where(r => r.Id == idToDelete).FirstOrDefault();

            Assert.IsNotNull(recipeToDelete);
            Assert.AreEqual(context.Recipes.Count(), 3);

            recipes.Remove(recipeToDelete);
            context.SaveChanges();

            Assert.AreEqual(context.Recipes.Count(), 2);
        }
    }

    public class Utils { 
        public static List<Recipe> GetTestRecipes()
        {
            var recipes = new List<Recipe>();
            recipes.Add(new Recipe
            {
                Id = 0,
                Title = "Cup noodles",
                DiffLevelId = 1
            });
            recipes.Add(new Recipe
            {
                Id = 1,
                Title = "Deep fried caviar",
                DiffLevelId = 3
            });
            recipes.Add(new Recipe
            {
                Id = 2,
                Title = "Pancakes",
                DiffLevelId = 1
            });

            return recipes;
        }
    }   
    
}
