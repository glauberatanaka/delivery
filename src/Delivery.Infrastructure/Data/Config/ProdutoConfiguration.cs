using Delivery.Core.Entities;
using Delivery.Core.Entities.ProdutoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Data.Config
{
    public sealed class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.Property(p => p.Nome)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(p => p.Preco)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
        }
    }
}
