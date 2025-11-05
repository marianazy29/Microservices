using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Domain.Aggregates;

namespace Ventas.Infrastructure.Configurations
{
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.UsuarioId).IsRequired();

            builder.Property(v => v.ClienteId).IsRequired();

            builder.Property(p => p.Fecha)
                .HasColumnType("date");

            builder.Property(v => v.Descuento).HasPrecision(18, 2);

            builder.Property(v => v.MontoTotal).HasPrecision(18, 2);

            builder.Property(p => p.Comentarios)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode(false);

            builder.Property(p => p.Estado)
               .IsRequired()
               .HasMaxLength(8)
               .IsUnicode(false);

            builder.HasMany(v => v.Detalles)
                   .WithOne()
                   .HasForeignKey(d => d.VentaId);

        }
    }
}
