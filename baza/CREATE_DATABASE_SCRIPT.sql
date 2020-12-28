CREATE TABLE "Users" (
  "ID" SERIAL PRIMARY KEY,
  "email" varchar,
  "nickname" varchar UNIQUE,
  "password_hash" varchar,
  "status" varchar,
  "email_confirmed" bool,
  "user_type_ID" int
);

CREATE TABLE "User_types" (
  "ID" SERIAL PRIMARY KEY,
  "user_type" varchar UNIQUE
);

CREATE TABLE "Pictures" (
  "ID" SERIAL PRIMARY KEY,
  "URL" varchar,
  "recipe_ID" int
);

CREATE TABLE "Recipes" (
  "ID" SERIAL PRIMARY KEY,
  "title" varchar,
  "diff_level_ID" int
);

CREATE TABLE "Recipe_difficulties" (
  "diff_level" SERIAL PRIMARY KEY,
  "description" varchar UNIQUE
);

CREATE TABLE "User_favorites" (
  "user_ID" int,
  "recipe_ID" int,
  PRIMARY KEY ("user_ID", "recipe_ID")
);

CREATE TABLE "User_recipes" (
  "user_ID" int,
  "recipe_ID" int,
  PRIMARY KEY ("user_ID", "recipe_ID")
);

CREATE TABLE "Recipe_steps" (
  "ID" SERIAL PRIMARY KEY,
  "step_number" int,
  "description" varchar,
  "recipe_ID" int
);

CREATE TABLE "Recipe_groceries" (
  "recipe_ID" int,
  "grocery_ID" int,
  "amount" varchar,
  PRIMARY KEY ("recipe_ID", "grocery_ID")
);

CREATE TABLE "Groceries" (
  "ID" SERIAL PRIMARY KEY,
  "name" varchar,
  "type_ID" int
);

CREATE TABLE "Grocery_types" (
  "ID" SERIAL PRIMARY KEY,
  "type_name" varchar UNIQUE
);

ALTER TABLE "Users" ADD FOREIGN KEY ("user_type_ID") REFERENCES "User_types" ("ID");

ALTER TABLE "Pictures" ADD FOREIGN KEY ("recipe_ID") REFERENCES "Recipes" ("ID");

ALTER TABLE "Recipes" ADD FOREIGN KEY ("diff_level_ID") REFERENCES "Recipe_difficulties" ("diff_level");

ALTER TABLE "User_favorites" ADD FOREIGN KEY ("user_ID") REFERENCES "Users" ("ID");

ALTER TABLE "User_favorites" ADD FOREIGN KEY ("recipe_ID") REFERENCES "Recipes" ("ID");

ALTER TABLE "User_recipes" ADD FOREIGN KEY ("user_ID") REFERENCES "Users" ("ID");

ALTER TABLE "User_recipes" ADD FOREIGN KEY ("recipe_ID") REFERENCES "Recipes" ("ID");

ALTER TABLE "Recipe_steps" ADD FOREIGN KEY ("recipe_ID") REFERENCES "Recipes" ("ID");

ALTER TABLE "Recipe_groceries" ADD FOREIGN KEY ("recipe_ID") REFERENCES "Recipes" ("ID");

ALTER TABLE "Recipe_groceries" ADD FOREIGN KEY ("grocery_ID") REFERENCES "Groceries" ("ID");

ALTER TABLE "Groceries" ADD FOREIGN KEY ("type_ID") REFERENCES "Grocery_types" ("ID");

