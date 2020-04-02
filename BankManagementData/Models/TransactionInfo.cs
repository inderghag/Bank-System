using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models
{
    public class TransactionInfo
    {
        public int account_id { get; set; }
        public int transaction_id { get; set; }
        public float amount_after { get; set; }
    }
}
