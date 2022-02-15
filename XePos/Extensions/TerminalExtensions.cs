using System.Diagnostics.CodeAnalysis;
using XePOS.Application.Services;

namespace XePOS.Application.Extensions;

[ExcludeFromCodeCoverage]
public static class TerminalExtensions
{
    /// <summary> Scan multiple products </summary>
    /// <exception cref="ArgumentException"> Thrown when the code is null or empty </exception>
    public static void ScanProductRange(this PointOfSaleTerminal terminal, string? products)
    {
        if (string.IsNullOrEmpty(products)) throw new ArgumentException("Codes cannot be null or empty");
        
        _ = products?.Select(c => terminal.ScanProduct(c.ToString())).ToArray();
        
        Console.WriteLine($"Scanned product{(products.Length > 1 ? "s" : "")}: {products}");
    }

    /// <summary> Print total price and clear the cart for the next scan </summary>
    public static void PrintCheckout(this PointOfSaleTerminal terminal)
    {
        Console.WriteLine($"> Total price: {terminal.CalculateTotal()}\n");
        terminal.ClearCart();
    }
}