﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace recepTour.Models
{
    public partial class RecepTourContext : DbContext
    {
        public RecepTourContext()
        {
        }

        public RecepTourContext(DbContextOptions<RecepTourContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Grocery> Groceries { get; set; }
        public virtual DbSet<GroceryType> GroceryTypes { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeDifficulty> RecipeDifficulties { get; set; }
        public virtual DbSet<RecipeGrocery> RecipeGroceries { get; set; }
        public virtual DbSet<RecipeStep> RecipeSteps { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFavorite> UserFavorites { get; set; }
        public virtual DbSet<UserRecipe> UserRecipes { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("host=ec2-54-220-229-215.eu-west-1.compute.amazonaws.com;port=5432;database=d3jgof5caojkns;username=mcdgxeedppzugi;password=56fd5710c4601eaa2c022a7a28332a5983e48c97af7cc3cc39b6b8a69da9b9b1;pooling=True;trust server certificate=True;ssl mode=Require");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Grocery>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.TypeId).HasColumnName("type_ID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Groceries)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("Groceries_type_ID_fkey");
            });

            modelBuilder.Entity<GroceryType>(entity =>
            {
                entity.ToTable("Grocery_types");

                entity.HasIndex(e => e.TypeName, "Grocery_types_type_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName)
                    .HasColumnType("character varying")
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_ID");

                entity.Property(e => e.Url)
                    .HasColumnType("character varying")
                    .HasColumnName("URL");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("Pictures_recipe_ID_fkey");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiffLevelId).HasColumnName("diff_level_ID");

                entity.Property(e => e.Title)
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.HasOne(d => d.DiffLevel)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.DiffLevelId)
                    .HasConstraintName("Recipes_diff_level_ID_fkey");
            });

            modelBuilder.Entity<RecipeDifficulty>(entity =>
            {
                entity.HasKey(e => e.DiffLevel)
                    .HasName("Recipe_difficulties_pkey");

                entity.ToTable("Recipe_difficulties");

                entity.HasIndex(e => e.Description, "Recipe_difficulties_description_key")
                    .IsUnique();

                entity.Property(e => e.DiffLevel).HasColumnName("diff_level");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");
            });

            modelBuilder.Entity<RecipeGrocery>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.GroceryId })
                    .HasName("Recipe_groceries_pkey");

                entity.ToTable("Recipe_groceries");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_ID");

                entity.Property(e => e.GroceryId).HasColumnName("grocery_ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("character varying")
                    .HasColumnName("amount");

                entity.HasOne(d => d.Grocery)
                    .WithMany(p => p.RecipeGroceries)
                    .HasForeignKey(d => d.GroceryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Recipe_groceries_grocery_ID_fkey");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeGroceries)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Recipe_groceries_recipe_ID_fkey");
            });

            modelBuilder.Entity<RecipeStep>(entity =>
            {
                entity.ToTable("Recipe_steps");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_ID");

                entity.Property(e => e.StepNumber).HasColumnName("step_number");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeSteps)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("Recipe_steps_recipe_ID_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Nickname, "Users_nickname_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.EmailConfirmed).HasColumnName("email_confirmed");

                entity.Property(e => e.Nickname)
                    .HasColumnType("character varying")
                    .HasColumnName("nickname");

                entity.Property(e => e.PasswordHash)
                    .HasColumnType("character varying")
                    .HasColumnName("password_hash");

                entity.Property(e => e.Status)
                    .HasColumnType("character varying")
                    .HasColumnName("status");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_ID");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("Users_user_type_ID_fkey");
            });

            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RecipeId })
                    .HasName("User_favorites_pkey");

                entity.ToTable("User_favorites");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_ID");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.UserFavorites)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_favorites_recipe_ID_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFavorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_favorites_user_ID_fkey");
            });

            modelBuilder.Entity<UserRecipe>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RecipeId })
                    .HasName("User_recipes_pkey");

                entity.ToTable("User_recipes");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_ID");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.UserRecipes)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_recipes_recipe_ID_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRecipes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_recipes_user_ID_fkey");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("User_types");

                entity.HasIndex(e => e.UserType1, "User_types_user_type_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserType1)
                    .HasColumnType("character varying")
                    .HasColumnName("user_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
