using System.Collections.Concurrent;
using XePOS.Application.Entities;

namespace XePOS.Application;

public class PointOfSaleTerminal
{
    /// <summary>
    /// Product pricing information
    /// </summary>
    private IEnumerable<Product> _productList;

    /// <summary>
    /// Store (Product, Quantity) as (Key, Value) pair
    /// </summary>
    private IDictionary<Product, int> _cart;

    public PointOfSaleTerminal()
    {
        _productList = new List<Product>();
        _cart = new ConcurrentDictionary<Product, int>();
    }

    public void SetPricing(IEnumerable<Product> productList)
    {
        // product code must be unique
        if (productList.DistinctBy(p => p.Code).Count() != productList.Count())
            throw new ArgumentException("Product code must be unique");

        // set pricing
        _productList = productList;
    }

    /// <summary>
    /// Scan a product. Return product quantity
    /// </summary>
    public int? ScanProduct(string code)
    {
        var p = _productList.FirstOrDefault(p => p.Code == code);
        
        // check product existence

        // add to/update cart
        _cart.TryGetValue(p, out var val);
        _cart[p] = ++val;

        return _cart[p];
    }

    public int? DropProduct(string code)
    {
        var p = _productList.FirstOrDefault(p => p.Code == code);

        _cart.TryGetValue(p, out var val);

        if (val == 0) throw new Exception("No product to remove");

        _cart[p] = --val;

        return _cart[p];
    }

    /// <summary>
    /// Scan a product. Return product quantity
    /// </summary>
    public void ClearCart()
    {
        _cart = new ConcurrentDictionary<Product, int>();
    }

    public decimal CalculateTotal() => _cart.Sum(p => GetProductTotal(p.Key,p.Value));

    private static decimal GetProductTotal(Product p, int quantity) 
        => p.Promotion.HasValue 
            ? p.Promotion.Value.BundlePrice * (quantity / p.Promotion.Value.BundleQuantity) +
              p.Price * (quantity % p.Promotion.Value.BundleQuantity)
            : p.Price * quantity;
}