using ContosoUniversity.Models;
using System;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Meal.Any())
            {
                return;   // DB has been seeded
            }

            var meals = new Meal[]
            {
            new Meal{MealID=1,name="Суші"},
            new Meal{MealID=2,name="Сет"},
            new Meal{MealID=3,name="Суп"},
            new Meal{MealID=4,name="Лапша"},
            new Meal{MealID=5,name="Паста"},
            new Meal{MealID=6,name="Равіолі"},
            new Meal{MealID=7,name="Піца"},
            new Meal{MealID=8,name="Салат"}
            };
            foreach (Meal s in meals)
            {
                context.Meal.Add(s);
            }
            context.SaveChanges();

            var rest = new Restaurant[]
            {
            new Restaurant{RestaurantID=1,Name="Муракамі"},
            new Restaurant{RestaurantID=2,Name="Євразія"},
            new Restaurant{RestaurantID=3,Name="ЖЗЛ"},
            new Restaurant{RestaurantID=4,Name="Улюблений дядько"},
            new Restaurant{RestaurantID=5,Name="Беліні"},
            new Restaurant{RestaurantID=6,Name="Токі"},
            new Restaurant{RestaurantID=7,Name="Заппа"}
            };
            foreach (Restaurant c in rest)
            {
                context.Restaurant.Add(c);
            }
            context.SaveChanges();

            var dish = new Dish[]
            {
            new Dish{DishID=1,MealID=1,RestaurantID=1, name="Каліфорнія", price=205},
            new Dish{DishID=2,MealID=1,RestaurantID=2, name="Філадельфія", price=190},
            new Dish{DishID=3,MealID=2,RestaurantID=1, name="Японське око", price=300},
            new Dish{DishID=4,MealID=2,RestaurantID=2, name="Дракон великий", price=450},
            new Dish{DishID=5,MealID=3,RestaurantID=6, name="Овочевий суп", price=100},
            new Dish{DishID=6,MealID=3,RestaurantID=6, name="Крем-суп грибний", price=90},
            new Dish{DishID=7,MealID=4,RestaurantID=4, name="Лапша з морепродуктами", price=230},
            new Dish{DishID=8,MealID=5,RestaurantID=3, name="Карбонара", price=223},
            new Dish{DishID=9,MealID=5,RestaurantID=5, name="Теллятеле", price=110},
            new Dish{DishID=10,MealID=6,RestaurantID=3, name="Равіолі с лососем", price=450},
            new Dish{DishID=11,MealID=7,RestaurantID=7, name="Чотири сири", price=610},
            new Dish{DishID=12,MealID=8,RestaurantID=4, name="Цезар з куркою", price=399},
            };
            foreach (Dish e in dish)
            {
                context.Dish.Add(e);
            }
            context.SaveChanges();
        }
    }
}