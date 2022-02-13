using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using XePOS.Application.Entities;

namespace XePOS.Application;

public class PointOfSaleTerminal
{
    /// <summary>
    /// Product pricing information
    /// </summary>
    private IList<Product> _productPricing;

    /// <summary>
    /// Store (Product, Quantity) as (Key, Value) pair
    /// </summary>
    private IDictionary<Product, int> _cart;

    public PointOfSaleTerminal()
    {
        _productPricing = new List<Product>();
        _cart = new ConcurrentDictionary<Product, int>();
    }

    public IList<Product> SetPricing(IList<Product> productPricing)
    {
        // product code must be unique
        if (productPricing.DistinctBy(p => p.Code).Count() != productPricing.Count())
            throw new ArgumentException("Product code must be unique");

        // ToDo: if price < 0 throw exception
        // ToDo: use validation
        //var _ = new ValidationContext();

        // set pricing
        _productPricing = productPricing;
        return _productPricing;
    }

    /// <summary> Scan a product code</summary>
    /// <returns> Return product quantity, or exception on non-existent product code </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int ScanProduct(string code)
    {
        var p = GetProductPricing(code);

        // add to/update cart
        _cart.TryGetValue(p, out var val);
        _cart[p] = ++val;

        return _cart[p];
    }

    /// <summary>
    /// Remove 1 product in the cart
    /// </summary>
    public int DropProduct(string code)
    {
        var p = GetProductPricing(code);

        _cart.TryGetValue(p, out var val);

        if (val == 0) throw new InvalidOperationException("No product to remove");

        _cart[p] = --val;

        return _cart[p];
    }

    public IList<Product> GetProductPricing() => _productPricing;

    public Product GetProductPricing(string code) => _productPricing.First(p => p.Code == code);

    /// <summary>
    /// Calculate the cart's total price
    /// </summary>
    public decimal CalculateTotal() => _cart.Sum(p => GetProductTotal(p.Key,p.Value));

    /// <summary>
    /// Given a product pricing and quantity, calculate the total price
    /// </summary>
    [ExcludeFromCodeCoverage]
    private static decimal GetProductTotal(Product p, int quantity) 
        => p.Promotion.HasValue 
            ? p.Promotion.Value.BundlePrice * (quantity / p.Promotion.Value.BundleQuantity) +
              p.Price * (quantity % p.Promotion.Value.BundleQuantity)
            : p.Price * quantity;

    /// <summary> Clear the cart </summary>
    [ExcludeFromCodeCoverage]
    public void ClearCart()
    {
        _cart = new ConcurrentDictionary<Product, int>();
    }
}