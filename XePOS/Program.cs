using System.Linq;
using XePOS;
using XePOS.Application;
using XePOS.Application.Entities;

var terminal = new PointOfSaleTerminal();


/// Todo: cases
/// validation on product
/// Empty pricing list
/// Duplicate Code
terminal.SetPricing(new List<Product>()
{
    new Product { Code = "A", Price = 1.25m, Promotion = new Promotion(3, 3m)},
    new Product { Code = "B", Price = 4.25m },
    new Product { Code = "C", Price = 1.00m, Promotion = new Promotion(6, 5m)},
    new Product { Code = "D", Price = 0.75m },
    new Product { Code = "F", Price = 0.75m, Promotion = new(10000000,2m)},
});

/// Todo: cases
/// invalid product
/// empty cart
terminal.ScanProduct("A");
terminal.ScanProduct("B");
terminal.ScanProduct("C");
terminal.ScanProduct("D");
terminal.ScanProduct("A");
terminal.ScanProduct("B");
terminal.ScanProduct("A");
terminal.ClearCart();

terminal.ScanProductRange("ABCDABA");
var result = terminal.CalculateTotal();
Console.WriteLine(result);
terminal.ClearCart();

terminal.ScanProductRange("CCCCCCC");
Console.WriteLine(terminal.CalculateTotal());
terminal.ClearCart();

terminal.ScanProductRange("ABCD");
Console.WriteLine(terminal.CalculateTotal());
terminal.ClearCart();

terminal.ScanProductRange("FF");
Console.WriteLine(terminal.CalculateTotal());
terminal.ClearCart();

//terminal.ScanProduct("E"); //ToDo: handle invalid product


Console.Read();