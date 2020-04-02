using BankManagementData.Models.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new PasswordsConfiguration());
            builder.ApplyConfiguration(new TransactionsConfiguration());
            builder.ApplyConfiguration(new TransactionInfoConfiguration());
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Passwords> Passwords { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TransactionInfo> TransactionInfo { get; set; }

    }
}
