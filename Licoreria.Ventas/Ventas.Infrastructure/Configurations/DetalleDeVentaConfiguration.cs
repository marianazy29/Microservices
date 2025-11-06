using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ventas.Domain.Aggregates;

namespace Ventas.Infrastructure.Configurations
{
    public class DetalleDeVentaConfiguration : IEntityTypeConfiguration<DetalleDeVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleDeVenta> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(p => p.Estado)
               .IsRequired()
               .HasMaxLength(8)
               .IsUnicode(false);
        }
    }
}
