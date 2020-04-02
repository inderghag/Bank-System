using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models.ModelConfigurations
{
    class TransactionsConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.HasKey(prop => prop.transaction_id);
            builder.Property(prop => prop.to_account_id)
                .IsRequired();
            builder.Property(prop => prop.transaction_name);
            builder.Property(prop => prop.amount_changed)
                .IsRequired();
            builder.Property(prop => prop.date)
                .IsRequired()
                .HasDefaultValue();
            builder.Property(prop => prop.from_account_id);
        }
    }
}
