using XePOS.Application;
using XePOS.Application.Entities;
using XePOS.Application.Extensions;

var terminal = new PointOfSaleTerminal();


// Todo: cases
// validation on product add
// Empty pricing list
// Duplicate Code - handled

// initialize list
var list = new List<Product>
{
    new() { Code = "A", Price = 1.25m, Promotion = new Promotion(3, 3m) },
    new() { Code = "B", Price = 4.25m },
    new() { Code = "C", Price = 1.00m, Promotion = new Promotion(6, 5m)},
    new() { Code = "D", Price = 0.75m },
    new() { Code = "F", Price = 0.75m, Promotion = new Promotion(10000000,2m)}
};
terminal.SetPricing(list);

// Todo: cases
// invalid product
// empty cart
terminal.ScanProduct("A");
terminal.ScanProduct("B");
terminal.ScanProduct("C");
terminal.ScanProduct("D");
terminal.ScanProduct("A");
terminal.ScanProduct("B");
terminal.ScanProduct("A");
var result = terminal.CalculateTotal();
Console.WriteLine(result);
terminal.ClearCart();

terminal.ScanProductRange("ABCDABA");
Console.WriteLine(terminal.CalculateTotal());
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