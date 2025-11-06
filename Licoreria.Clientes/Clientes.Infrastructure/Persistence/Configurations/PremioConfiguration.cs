using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clientes.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clientes.Infrastructure.Persistence.Configurations
{
    public class PremioConfiguration : IEntityTypeConfiguration<Premio>
    {
        public void Configure(EntityTypeBuilder<Premio> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(p => p.Descripcion)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(false);

            builder.Property(p => p.Estado)
              .IsRequired()
              .HasMaxLength(8)
              .IsUnicode(false);
        }
    }
}
