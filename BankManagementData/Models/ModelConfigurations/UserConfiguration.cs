using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementData.Models.ModelConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.client_id);

            builder.Property(prop => prop.first_name)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(prop => prop.last_name)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(prop => prop.email)
                .HasMaxLength(50);
            builder.Property(prop => prop.street_address)
                .HasMaxLength(50);
            builder.Property(prop => prop.city)
                .HasMaxLength(20);
            builder.Property(prop => prop.province)
                .HasMaxLength(30);
            builder.Property(prop => prop.country)
                .HasMaxLength(20);
            builder.Property(prop => prop.phone_number)
                .HasMaxLength(50);
            builder.Property(prop => prop.user_role)
                .HasDefaultValue(1)
                .IsRequired();

        }
    }
}
