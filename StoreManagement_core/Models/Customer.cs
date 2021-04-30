using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int CustId { get; set; }

        [Required]
        public String CustName { get; set; }

        [Required]
        public String CustMemNo { get; set; }

        public virtual Membership Mem { get; set; }

        [ForeignKey("Mem")]
        public int MemId { get; set; }

        public String CustAdd { get; set; }

        public int CustPhone { get; set; }

        public String CustEmail { get; set; }

    }
}
