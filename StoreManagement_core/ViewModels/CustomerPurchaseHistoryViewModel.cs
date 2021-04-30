using StoreManagement_core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.ViewModels
{
    public class CustomerPurchaseHistoryViewModel
    {
        // Customer
        public virtual Customer Cust { get; set; }

        [ForeignKey("Cust")]
        public int CustId { get; set; }

        public String CustName { get; set; }

        // Sales
        public virtual Sales Sale { get; set; }

        [ForeignKey("Sale")]
        public int SalesId { get; set; }

        public String BillNo { get; set; }

        public DateTime SalesDate { get; set; }

        // Sales Detail
        public virtual SalesDetail SalDet { get; set; }

        [ForeignKey("SalDet")]
        public int SalesDetailId { get; set; }

        // Product
        public virtual Product Prod { get; set; }

        [ForeignKey("Prod")]
        public int ProdId { get; set; }

        public String ProdName { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

    }
}
