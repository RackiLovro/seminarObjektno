INSERT INTO public."User_types"("ID", "user_type")
VALUES (1, 'admin'),
(2, 'user');

INSERT INTO public."Users"("ID", "email", "nickname", "password_hash", "status", "email_confirmed", "user_type_ID")
VALUES (1, 'klara@objektno.hr', 'klara', '7FB0CEE85B6101868D6E11396BBB5259', 'chilling', true, 1),
(2, 'lovro@objektno.hr', 'lovro', '15937CB4A65BFFF539E655128437A2C1', 'not working', false, 2),
(3, 'leon@objektno.hr', 'leon', '5C443B2003676FA5E8966030CE3A86EA', 'maybe working', true, 2);

INSERT INTO public."Grocery_types"("ID", "type_name")
VALUES (1, 'povrće'),
(2, 'voće'),
(3, 'začini');

INSERT INTO public."Groceries"("ID", "name", "type_ID")
VALUES (1, 'brokula', 1),
(2, 'jabuka', 2),
(3, 'sol', 3),
(4, 'papar', 3),
(5, 'naranča', 2);

INSERT INTO public."Recipe_difficulties"("diff_level", "description")
VALUES (1, 'easy'),
(2, 'medium'),
(3, 'hard');

INSERT INTO public."Recipes"("ID", "title", "diff_level_ID")
VALUES (1, 'gulaš', 2),
(2, 'voćna salata', 1);

INSERT INTO public."User_recipes"("user_ID", "recipe_ID")
VALUES (1,1),
(2,2);

INSERT INTO public."User_favorites"("user_ID", "recipe_ID")
VALUES (2,1),
(3, 1);

INSERT INTO public."Recipe_groceries"("recipe_ID", "grocery_ID", "amount")
VALUES (1, 1, '0.5 kg'),
(2, 2, '2'),
(2, 5, '2');

INSERT INTO public."Recipe_steps"("ID", "step_number", "recipe_ID", "description")
VALUES (1, 1, 2, 'Nareži voće'),
(2, 2, 2, 'Pomiješaj voće');


