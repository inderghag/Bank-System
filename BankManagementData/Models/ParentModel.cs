using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models
{
    public class ParentModel
    {
        public Passwords Passwords { get; set; }
        public User User { get; set; }
        public Account Account { get; set; }
        public Transactions Transactions { get; set; }
    }
}
