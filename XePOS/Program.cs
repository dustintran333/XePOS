﻿using XePOS.Application.Data;
using XePOS.Application.Extensions;
using XePOS.Application.Services;

var terminal = new PointOfSaleTerminal();

// initialize list
var list = PricingData.GetData();
terminal.SetPricing(list);

var scanOrder = "ABCDABA";

foreach (var code in scanOrder) terminal.ScanProduct(code.ToString());

var result = terminal.CalculateTotal();

Console.WriteLine("Scan order: " + scanOrder);
Console.WriteLine("Total price: " + result);

terminal.ClearCart();


//using terminal extensions

terminal.ScanProductRange("CCCCCCC");
terminal.PrintCheckout();

terminal.ScanProductRange("ABCD");
terminal.PrintCheckout();

terminal.ScanProductRange("");
terminal.PrintCheckout();

terminal.ScanProductRange(null);
terminal.PrintCheckout();

Console.ReadLine();