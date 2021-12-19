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
    public class CarrinhoItemConfiguration : IEntityTypeConfiguration<CarrinhoItem>
    {
        public void Configure(EntityTypeBuilder<CarrinhoItem> builder)
        {
            builder.Property(ci => ci.Quantidade)
                .IsRequired();
        }
    }
}
