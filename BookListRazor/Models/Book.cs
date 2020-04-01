using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Models
{
    public class Book
    {
        [Key]   // Creates an ID value automatically
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public String Author { get; set; }

        public String ISBN { get; set; }    // After adding a new property we need a new migration
    }
}
