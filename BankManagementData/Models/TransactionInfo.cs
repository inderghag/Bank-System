using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models
{
    public class TransactionInfo
    {
        public long transaction_count { get; set; }
        public int account_id { get; set; }
        public int transaction_id { get; set; }
        public float amount_after { get; set; }
        public string transaction_name { get; set; }
        public float amount_changed { get; set; }
        public DateTime date { get; set; }
    }
}
