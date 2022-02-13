using XePOS.Application.Entities;

namespace XePOS.Application.Data;

public static class PricingData
{
    private static readonly IList<Product> ProductPricing = new List<Product>
    {
        new() { Code = "A", Price = 1.25m, Promotion = new Promotion(3, 3.00m) },
        new() { Code = "B", Price = 4.25m },
        new() { Code = "C", Price = 1.00m, Promotion = new Promotion(6, 5.00m) },
        new() { Code = "D", Price = 0.75m },
    };

    public static IList<Product> GetData() => ProductPricing;
}