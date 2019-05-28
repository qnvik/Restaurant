using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    public class Order
    {
        public int OrderID { get; set; }
        public int DishID { get; set; }
        public int kol { get; set; }


        public Dish Dishs { get; set; }
     
    }
}