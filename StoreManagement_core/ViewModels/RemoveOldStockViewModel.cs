using StoreManagement_core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.ViewModels
{
    public class RemoveOldStockViewModel
    {
        public virtual Product Prod { get; set; }

        [Key]
        [Required]
        public int ProdId { get; set; }

        public String ProdName { get; set; }

        public int Quantity { get; set; }

        public DateTime StockedDate { get; set; }

    }
}
