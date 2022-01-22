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
    public class RoleApplicationConfiguration : IEntityTypeConfiguration<RoleApplication>
    {
        public void Configure(EntityTypeBuilder<RoleApplication> builder)
        {

        }
    }
}
