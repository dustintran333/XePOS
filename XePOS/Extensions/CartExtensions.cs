namespace XePOS.Application.Extensions;

public static class CartExtensions
{
    public static void ScanProductRange(this PointOfSaleTerminal pos,string range)
    {
        Console.WriteLine($"Scanned products: {range}");
        _ = range.Select(c => pos.ScanProduct(c.ToString())).ToArray();
    }
}