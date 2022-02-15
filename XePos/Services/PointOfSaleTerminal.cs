using System.Collections.Concurrent;
using XePOS.Application.Entities;
using XePOS.Application.Extensions;
using XePOS.Application.Interfaces;

namespace XePOS.Application.Services;

public class PointOfSaleTerminal : IPointOfSaleTerminal
{
    private IList<Product> _productPricingList;
    private IDictionary<Product, int> _cart;

    public PointOfSaleTerminal()
    {
        _productPricingList = new List<Product>();
        _cart = new ConcurrentDictionary<Product, int>();
    }

    public IList<Product> SetPricing(IList<Product> productPricingList)
    {
        // Test validations
        if (!productPricingList.IsPricingDataValid())
            throw new ArgumentException("Pricing data is invalid");

        // Test uniqueness
        if (productPricingList.DistinctBy(p => p.Code).Count() != productPricingList.Count())
            throw new ArgumentException("Product code must be unique");

        // Set pricing
        _productPricingList = productPricingList;
        return _productPricingList;
    }

    public int ScanProduct(string code)
    {
        var p = GetProductPricing(code);

        // Add to/update cart
        _cart.TryGetValue(p, out var val);
        _cart[p] = ++val;

        return _cart[p];
    }

    public int DropProduct(string code)
    {
        var p = GetProductPricing(code);

        _cart.TryGetValue(p, out var val);
        if (val == 0) throw new InvalidOperationException("No product to remove");
        _cart[p] = --val;

        return _cart[p];
    }

    public IList<Product> GetProductPricing() => _productPricingList;

    public Product GetProductPricing(string code) =>
        string.IsNullOrEmpty(code)
            ? throw new ArgumentException("Code cannot be null or empty")
            : _productPricingList.First(p => p.Code == code);

    public decimal CalculateTotal() =>
        _cart.Count == 0
            ? throw new InvalidOperationException("Cart is empty")
            : _cart.Sum(p => GetProductTotal(p.Key, p.Value));

    public void ClearCart() => _cart = new ConcurrentDictionary<Product, int>();

    /// <param name="p"> The product pricing</param>
    /// <param name="quantity"> The product quantity in the cart </param>
    /// <returns> The total price of a product in the cart</returns>
    private static decimal GetProductTotal(Product p, int quantity) =>
        p.Promotion.HasValue
            ? p.Promotion.Value.BundlePrice * (quantity / p.Promotion.Value.BundleQuantity) +
              p.Price * (quantity % p.Promotion.Value.BundleQuantity)
            : p.Price * quantity;
}