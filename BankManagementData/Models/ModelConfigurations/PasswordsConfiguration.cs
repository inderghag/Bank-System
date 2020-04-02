using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models.ModelConfigurations
{
    class PasswordsConfiguration : IEntityTypeConfiguration<Passwords>
    {
        public void Configure(EntityTypeBuilder<Passwords> builder)
        {
            builder.HasKey(prop => prop.client_id);

            builder.Property(prop => prop.password)
                .HasMaxLength(100)
                .IsRequired();

        }   

    }
}
