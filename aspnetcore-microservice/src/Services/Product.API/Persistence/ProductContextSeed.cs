using Product.API.Entities;
using Ilogger = Serilog.ILogger;

namespace Product.API.Persistence;

public class ProductContextSeed
{
    public static async Task SeedProductAsync(ProductContext productContext, Ilogger logger)
    {
        if (!productContext.Products.Any())
        {
            productContext.AddRange(GetCatalogProduct());
            await productContext.SaveChangesAsync();
            logger.Information("Seeding product database with context {DbContextName}", nameof(ProductContext));
        }
    }

    private static IEnumerable<CatalogProduct> GetCatalogProduct()
    {
        return new List<CatalogProduct>
        {
            new()
            {
                No = "Lotus",
                Name = "Esprit",
                Summary = "Summary of Lotus",
                Description = "Description of Lotus",
                Price = (decimal)177722.49
            },
            new()
            {
                No = "Nokia",
                Name = "Xperia",
                Summary = "Summary of Nokia",
                Description = "Description of Nokia",
                Price = (decimal)127722.49
            }
        };
    }
}