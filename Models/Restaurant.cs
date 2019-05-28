using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Restaurant
    {
       
        public int RestaurantID { get; set; }
        [Required]
        public string Name { get; set; }

        public int OwnerID { get; set; }

        public Owner Owner { get; set; }
        public ICollection<Dish> Enrollments { get; set; }
    }
    
}
