using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class Stock
    {
        [Key]
        [Required]
        public int StockId { get; set; }

        public virtual Product Prod { get; set; }

        [ForeignKey("Prod")]
        public int ProdId { get; set; }

        public int Quantity { get; set; }

        public DateTime StockedDate { get; set; }
    }
}
