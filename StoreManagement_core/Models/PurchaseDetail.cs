using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class PurchaseDetail
    {
        [Key]
        [Required]
        public int PurDetId { get; set; }

        public virtual Purchase Pur { get; set; }

        [ForeignKey("Pur")]
        public int PurId { get; set; }

        public virtual Product Prod { get; set; }

        [ForeignKey("Prod")]
        public int ProdId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

    }
}
