using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ProdId { get; set; }

        [Required]
        public String ProdName { get; set; }

        public virtual Category Cat { get; set; }

        [ForeignKey("Cat")]
        public int CatId { get; set; }

        public String ProdCode { get; set; }

        public String ProdDesc { get; set; }

    }


}
