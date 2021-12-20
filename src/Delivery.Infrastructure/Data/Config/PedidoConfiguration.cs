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
    class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.Metadata
                .FindNavigation(nameof(Pedido.Itens))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(c => c.IdentityUserId)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.DataCadastro)
                .IsRequired();

            builder.Property(c => c.ValorTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.ValorFrete)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.OwnsOne(user => user.Endereco,
                navigationBuilder =>
                {
                    navigationBuilder.Property(endereco => endereco.Cep).HasColumnName("Cep");
                    navigationBuilder.Property(endereco => endereco.Uf).HasColumnName("Uf");
                    navigationBuilder.Property(endereco => endereco.Localidade).HasColumnName("Localidade");
                    navigationBuilder.Property(endereco => endereco.Logradouro).HasColumnName("Logradouro");
                    navigationBuilder.Property(endereco => endereco.Numero).HasColumnName("Numero");
                    navigationBuilder.Property(endereco => endereco.Complemento).HasColumnName("Complemento");
                });
        }
    }
}
