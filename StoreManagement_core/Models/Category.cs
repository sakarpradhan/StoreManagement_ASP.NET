using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CatId { get; set; }

        [Required]
        public String CatName { get; set; }
    }
}
