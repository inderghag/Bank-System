using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models.ModelConfigurations
{
    class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(prop => prop.account_id);

            builder.Property(prop => prop.client_id)
                .IsRequired();

            builder.Property(prop => prop.account_type)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(prop => prop.amount)
                .HasMaxLength(11);
        }
    }
}
