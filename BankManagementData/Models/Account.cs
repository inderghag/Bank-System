using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BankManagementData.Models
{
    public class Account
    {
        [Required]
        public int account_id { get; set; }
        [Required]
        public int client_id { get; set; }
        [StringLength(30)]
        [Required]
        public string account_type { get; set; }
        public float amount { get; set; }

    }
}
