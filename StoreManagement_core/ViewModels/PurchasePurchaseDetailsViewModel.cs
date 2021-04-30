using StoreManagement_core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.ViewModels
{
    public class PurchasePurchaseDetailsViewModel
    {
        // Properties for Purchase
        [Required]
        public String VendorName { get; set; }

        public DateTime PurDate { get; set; }

        // Properties for PurchaseDetails
        public virtual Product Prod { get; set; }

        [ForeignKey("Prod")]
        public int ProdId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
