using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class SalesDetail
    {
        [Key]
        [Required]
        public int SalesDetailId { get; set; }

        public virtual Sales Sale { get; set; }

        [ForeignKey("Sale")]
        public int SalesId { get; set; }

        public virtual Product Prod { get; set; }

        [ForeignKey("Prod")]
        public int ProdId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
