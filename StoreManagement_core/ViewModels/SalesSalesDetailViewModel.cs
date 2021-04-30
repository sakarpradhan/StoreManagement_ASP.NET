using StoreManagement_core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.ViewModels
{
    public class SalesSalesDetailViewModel
    {
        // Fields for Sales
        public virtual Customer Cust { get; set; }

        [ForeignKey("Cust")]
        public int CustId { get; set; }

        [Required]
        public String BillNo { get; set; }

        public DateTime SalesDate { get; set; }

        public String Remarks { get; set; }

        // Fields for Sales Details
        public virtual Product Prod { get; set; }

        [ForeignKey("Prod")]
        public int ProdId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
