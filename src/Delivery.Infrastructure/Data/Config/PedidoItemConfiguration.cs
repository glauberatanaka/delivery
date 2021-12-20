using Delivery.Core.Entities.PedidoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Data.Config
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.Property(c => c.PedidoId)
                .IsRequired();

            builder.Property(c => c.Nome)
                .IsRequired();

            builder.Property(c => c.Preco)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Quantidade)
                .IsRequired();
        }
    }
}
