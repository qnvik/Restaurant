using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Meal
    {
        public int MealID { get; set; }
        [Required]
        public string name { get; set; }
      

        public ICollection<Dish> Enrollments { get; set; }
    }
}

