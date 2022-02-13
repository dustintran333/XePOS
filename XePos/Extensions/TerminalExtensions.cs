using System.Diagnostics.CodeAnalysis;

namespace XePOS.Application.Extensions;

[ExcludeFromCodeCoverage]
public static class TerminalExtensions
{
    /// <summary>
    /// Scan multiple products
    /// </summary>
    public static void ScanProductRange(this PointOfSaleTerminal terminal, string? products)
    {
        Console.WriteLine(string.IsNullOrEmpty(products) ? "Cart is empty" : $"Scanned products: {products}");
        _ = products?.Select(c => terminal.ScanProduct(c.ToString())).ToArray();
    }

    /// <summary>
    /// Print total price and clear the cart for the next scan
    /// </summary>
    public static void PrintCheckout(this PointOfSaleTerminal terminal)
    {
        Console.WriteLine($"Total price: {terminal.CalculateTotal()}");
        terminal.ClearCart();
    }
}