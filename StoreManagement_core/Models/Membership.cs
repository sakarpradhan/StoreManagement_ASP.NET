using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Models
{
    public class Membership
    {
        [Key]
        [Required]
        public int MemId { get; set; }

        [Required]
        public String MemName { get; set; }
    }
}
