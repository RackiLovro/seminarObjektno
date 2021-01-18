﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using recepTour.Models;

namespace recepTour.Migrations
{
    [DbContext(typeof(RecepTourContext))]
    partial class RecepTourContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:Collation", "en_US.UTF-8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("recepTour.Models.Grocery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_ID");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Groceries");
                });

            modelBuilder.Entity("recepTour.Models.GroceryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("TypeName")
                        .HasColumnType("character varying")
                        .HasColumnName("type_name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TypeName" }, "Grocery_types_type_name_key")
                        .IsUnique();

                    b.ToTable("Grocery_types");
                });

            modelBuilder.Entity("recepTour.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_ID");

                    b.Property<string>("Url")
                        .HasColumnType("character varying")
                        .HasColumnName("URL");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("recepTour.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("DiffLevelId")
                        .HasColumnType("integer")
                        .HasColumnName("diff_level_ID");

                    b.Property<string>("Title")
                        .HasColumnType("character varying")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("DiffLevelId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("recepTour.Models.RecipeDifficulty", b =>
                {
                    b.Property<int>("DiffLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("diff_level")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.HasKey("DiffLevel")
                        .HasName("Recipe_difficulties_pkey");

                    b.HasIndex(new[] { "Description" }, "Recipe_difficulties_description_key")
                        .IsUnique();

                    b.ToTable("Recipe_difficulties");
                });

            modelBuilder.Entity("recepTour.Models.RecipeGrocery", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_ID");

                    b.Property<int>("GroceryId")
                        .HasColumnType("integer")
                        .HasColumnName("grocery_ID");

                    b.Property<string>("Amount")
                        .HasColumnType("character varying")
                        .HasColumnName("amount");

                    b.HasKey("RecipeId", "GroceryId")
                        .HasName("Recipe_groceries_pkey");

                    b.HasIndex("GroceryId");

                    b.ToTable("Recipe_groceries");
                });

            modelBuilder.Entity("recepTour.Models.RecipeStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_ID");

                    b.Property<int?>("StepNumber")
                        .HasColumnType("integer")
                        .HasColumnName("step_number");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Recipe_steps");
                });

            modelBuilder.Entity("recepTour.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Email")
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<bool?>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("Nickname")
                        .HasColumnType("character varying")
                        .HasColumnName("nickname");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("character varying")
                        .HasColumnName("password_hash");

                    b.Property<string>("Status")
                        .HasColumnType("character varying")
                        .HasColumnName("status");

                    b.Property<int?>("UserTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("user_type_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserTypeId");

                    b.HasIndex(new[] { "Nickname" }, "Users_nickname_key")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("recepTour.Models.UserFavorite", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_ID");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_ID");

                    b.HasKey("UserId", "RecipeId")
                        .HasName("User_favorites_pkey");

                    b.HasIndex("RecipeId");

                    b.ToTable("User_favorites");
                });

            modelBuilder.Entity("recepTour.Models.UserRecipe", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_ID");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_ID");

                    b.HasKey("UserId", "RecipeId")
                        .HasName("User_recipes_pkey");

                    b.HasIndex("RecipeId");

                    b.ToTable("User_recipes");
                });

            modelBuilder.Entity("recepTour.Models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("UserType1")
                        .HasColumnType("character varying")
                        .HasColumnName("user_type");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserType1" }, "User_types_user_type_key")
                        .IsUnique();

                    b.ToTable("User_types");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("recepTour.Models.Grocery", b =>
                {
                    b.HasOne("recepTour.Models.GroceryType", "Type")
                        .WithMany("Groceries")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("Groceries_type_ID_fkey");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("recepTour.Models.Picture", b =>
                {
                    b.HasOne("recepTour.Models.Recipe", "Recipe")
                        .WithMany("Pictures")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("Pictures_recipe_ID_fkey");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("recepTour.Models.Recipe", b =>
                {
                    b.HasOne("recepTour.Models.RecipeDifficulty", "DiffLevel")
                        .WithMany("Recipes")
                        .HasForeignKey("DiffLevelId")
                        .HasConstraintName("Recipes_diff_level_ID_fkey");

                    b.Navigation("DiffLevel");
                });

            modelBuilder.Entity("recepTour.Models.RecipeGrocery", b =>
                {
                    b.HasOne("recepTour.Models.Grocery", "Grocery")
                        .WithMany("RecipeGroceries")
                        .HasForeignKey("GroceryId")
                        .HasConstraintName("Recipe_groceries_grocery_ID_fkey")
                        .IsRequired();

                    b.HasOne("recepTour.Models.Recipe", "Recipe")
                        .WithMany("RecipeGroceries")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("Recipe_groceries_recipe_ID_fkey")
                        .IsRequired();

                    b.Navigation("Grocery");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("recepTour.Models.RecipeStep", b =>
                {
                    b.HasOne("recepTour.Models.Recipe", "Recipe")
                        .WithMany("RecipeSteps")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("Recipe_steps_recipe_ID_fkey");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("recepTour.Models.User", b =>
                {
                    b.HasOne("recepTour.Models.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId")
                        .HasConstraintName("Users_user_type_ID_fkey");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("recepTour.Models.UserFavorite", b =>
                {
                    b.HasOne("recepTour.Models.Recipe", "Recipe")
                        .WithMany("UserFavorites")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("User_favorites_recipe_ID_fkey")
                        .IsRequired();

                    b.HasOne("recepTour.Models.User", "User")
                        .WithMany("UserFavorites")
                        .HasForeignKey("UserId")
                        .HasConstraintName("User_favorites_user_ID_fkey")
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("recepTour.Models.UserRecipe", b =>
                {
                    b.HasOne("recepTour.Models.Recipe", "Recipe")
                        .WithMany("UserRecipes")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("User_recipes_recipe_ID_fkey")
                        .IsRequired();

                    b.HasOne("recepTour.Models.User", "User")
                        .WithMany("UserRecipes")
                        .HasForeignKey("UserId")
                        .HasConstraintName("User_recipes_user_ID_fkey")
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("recepTour.Models.Grocery", b =>
                {
                    b.Navigation("RecipeGroceries");
                });

            modelBuilder.Entity("recepTour.Models.GroceryType", b =>
                {
                    b.Navigation("Groceries");
                });

            modelBuilder.Entity("recepTour.Models.Recipe", b =>
                {
                    b.Navigation("Pictures");

                    b.Navigation("RecipeGroceries");

                    b.Navigation("RecipeSteps");

                    b.Navigation("UserFavorites");

                    b.Navigation("UserRecipes");
                });

            modelBuilder.Entity("recepTour.Models.RecipeDifficulty", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("recepTour.Models.User", b =>
                {
                    b.Navigation("UserFavorites");

                    b.Navigation("UserRecipes");
                });

            modelBuilder.Entity("recepTour.Models.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
