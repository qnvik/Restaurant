using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ContosoUniversity.Models
{
    public class Owner
    {

        public int OwnerID { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth")]
        public DateTime Birth { get; set; }


        public ICollection<Restaurant> Restaurants { get; set; }


    }
}
