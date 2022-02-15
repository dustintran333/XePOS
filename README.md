## Introduction

Point Of Sale Terminal (XePos) is implemented in C# running on .NET 6 and is best to be run and tested in Visual Studio 2022. I structured my project as follow:
- **XePOS.Application**
  - Contains the main application and Entities, Extensions, Services..
- **XePOS.Tests**
  - Contains unit tests

For the sake of simplicity I store the data in a static class, so that it can be reused throughout the solution.

The main program covers the 3 minimal inputs. Additional input can be manually typed in for convenience purpose.
## Get started

Clone the repo with either Visual Studio or command line

```bash
git clone https://github.com/dustintran333/XePOS.git
```

## Usage
```bash
cd xepos
dotnet run
```

## Examples
```csharp
var terminal = new PointOfSaleTerminal();
var list = PricingData.GetData();
terminal.SetPricing(list);
var scanOrder = "ABCDABA";
foreach (var code in scanOrder)
    terminal.ScanProduct(code.ToString());
var result = terminal.CalculateTotal();

terminal.ScanProductRange("CCCCCCC"); // convenience method, single character code only
terminal.PrintCheckout(); // print the total and clear cart
```
