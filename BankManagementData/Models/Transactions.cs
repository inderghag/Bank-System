using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BankManagementData.Models
{
    public class Transactions
    {
        public int from_account_id { get; set; }
        public string transaction_name { get; set; }
        public float amount_changed { get; set; }
        public DateTime date { get; set; }
        public int transaction_id { get; set; }
        public float to_account_id { get; set; }
    }
}
