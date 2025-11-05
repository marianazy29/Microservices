using Clientes.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(p => p.NombreCompleto)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(false);

            builder.Property(p => p.Estado)
               .IsRequired()
               .HasMaxLength(8)
               .IsUnicode(false);

            builder.OwnsOne(c => c.PuntosAcumulados, pa =>
            {
                pa.Property(p => p.Puntos)
                  .HasColumnName("PuntosAcumulados")
                  .IsRequired();
            });

            builder.HasMany(c => c.Premios)
                  .WithOne()
                  .HasForeignKey(p => p.ClienteId);
        }
    }
}
