using Courserio.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Infrastructure.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Id)
              .HasMaxLength(100)
              .IsRequired();

            builder.Property(c => c.Username)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(c => c.FirstName)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(c => c.LastName)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(c => c.ProfilePicture)
               .HasMaxLength(500)
               .IsRequired();

        }
    }
}
