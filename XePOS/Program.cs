using XePOS.Application.Data;
using XePOS.Application.Extensions;
using XePOS.Application.Services;

var terminal = new PointOfSaleTerminal();

// Initialize list
var list = PricingData.GetData();
terminal.SetPricing(list);


// Sample input
var scanOrder = "ABCDABA";
foreach (var code in scanOrder)
    terminal.ScanProduct(code.ToString());
var result = terminal.CalculateTotal();

Console.WriteLine("Scanned products: " + scanOrder);
Console.WriteLine("> Total price: " + result);

terminal.ClearCart();


// Using terminal extensions
terminal.ScanProductRange("CCCCCCC");
terminal.PrintCheckout();
terminal.ScanProductRange("ABCD");
terminal.PrintCheckout();

// Taking user input
while (true)
{

    Console.Write("Input a case-sensitive scan sequence (A, B, C, D): ");
    try
    {
        terminal.ScanProductRange(Console.ReadLine());
        terminal.PrintCheckout();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex switch
        {
            ArgumentException => "No product to scan\n",
            InvalidOperationException => "Wrong product code\n",
            _ => ex.ToString()
        });
    }
}
