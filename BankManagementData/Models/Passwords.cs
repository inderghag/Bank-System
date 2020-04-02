using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BankManagementData.Models
{
    public class Passwords
    {
        [StringLength(100)]
        [Required]
        public string password { get; set; }
        public int client_id { get; set; }
    }
}
