using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models.ModelConfigurations
{
    class TransactionInfoConfiguration : IEntityTypeConfiguration<TransactionInfo>
    {
        public void Configure(EntityTypeBuilder<TransactionInfo> builder)
        {
            builder.Property(prop => prop.transaction_id)
                .IsRequired();
            builder.Property(prop => prop.account_id)
                .IsRequired();
            builder.Property(prop => prop.amount_after)
                .IsRequired();
        }

    }
}
