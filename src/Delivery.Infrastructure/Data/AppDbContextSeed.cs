using Delivery.Core.Entities.ProdutoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext dbContext,
        ILoggerFactory loggerFactory, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (dbContext.Database.IsSqlServer())
                {
                    dbContext.Database.Migrate();
                }

                if (!await dbContext.Produtos.AnyAsync())
                {
                    await dbContext.Produtos.AddRangeAsync(
                        GetProdutosSeedData());

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;
                var log = loggerFactory.CreateLogger<AppDbContextSeed>();
                log.LogError(ex.Message);
                await SeedAsync(dbContext, loggerFactory, retryForAvailability);
                throw;
            }
        }

        static IEnumerable<Produto> GetProdutosSeedData()
        {
            return new List<Produto>
            {
                new("Shampoo 400ml", "Anti-caspa", 21.99m, 10),
                new("Sabonete", "", 12.33m, 13),
                new("Coxão-mole", "Valor por kg", 53.33m, 33),
                new("Vassoura", "", 21.33m, 53),
                new("Mussarela", "Valor por kg", 53.66m, 63),
                new("Coca-cola 600ml", "Garrafa PET", 5.77m, 8),
                new("Cerveja Skol Lata", "", 3.33m, 34),
                new("Pasta de dente Sorriso", "", 3.55m, 7),
                new("Vinho Tinto Gato Preto", "", 33.5m, 24),
                new("Bacon 300g", "", 26.7m, 64),
                new("Postas de Tambaqui", "Congelado", 44m, 34),
                new("Vodka Skylime 600ml", "", 59m, 64),
                new("Guaraná 2L", "", 9m, 89),
                new("Vinho do Porto", "", 45.2m, 45),
                new("Condicionador Monange", "", 23.5m, 53),
                new("Xarope de Guaraná", "", 23.6m, 234),
            };
        }
    }
}
