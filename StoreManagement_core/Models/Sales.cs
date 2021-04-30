using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class Sales
    {
        [Key]
        [Required]
        public int SalesId { get; set; }

        public virtual Customer Cust { get; set; }

        [ForeignKey("Cust")]
        public int CustId { get; set; }

        [Required]
        public String BillNo { get; set; }

        public DateTime SalesDate { get; set; }

        public String Remarks { get; set; }
    }
}
