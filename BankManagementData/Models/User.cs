using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BankManagementData.Models
{
    public class User
    {
        [StringLength(50)]
        [Required]
        public string first_name { get; set; }
        [StringLength(20)]
        [Required]
        public string last_name { get; set; }
        [StringLength(50)]
        public string email { get; set; }
        [StringLength(50)]
        public string street_address { get; set; }
        [StringLength(20)]
        public string city { get; set; }
        [StringLength(30)]
        public string province { get; set;  }
        [StringLength(20)]
        public string country { get; set; }
        [StringLength(50)]
        public string phone_number { get; set; }
        public int client_id { get; set; }
        public int user_role { get; set; }
                     
    }
}
