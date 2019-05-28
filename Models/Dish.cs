using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class Dish
    {
        public int DishID { get; set; }
        public int MealID { get; set; }
        public int RestaurantID { get; set; }
        [Required]
        public string name { get; set; }

        [Range(0, 5000)]
        public int price { get; set; }


        public Meal Meal { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}