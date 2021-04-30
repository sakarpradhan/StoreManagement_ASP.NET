using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class Purchase
    {
        [Key]
        [Required]
        public int PurId { get; set; }

        [Required]
        public String VendorName { get; set; }

        //public String Address { get; set; }

        //public String BillNo { get; set; }

        public DateTime PurDate { get; set; }
    }
}
