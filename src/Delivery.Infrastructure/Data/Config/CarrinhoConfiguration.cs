using Delivery.Core.Entities.CarrinhoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Data.Config
{
    public class CarrinhoConfiguration : IEntityTypeConfiguration<Carrinho>
    {
        public void Configure(EntityTypeBuilder<Carrinho> builder)
        {
            builder.Metadata
                .FindNavigation(nameof(Carrinho.Itens))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(c => c.IdentityUserId)
                .IsRequired();
        }
    }
}
