using System;
using XePOS.Application;
using XePOS.Application.Data;
using XePOS.Application.Extensions;
using Xunit;

namespace XePos.Tests;

public class DropProductTests
{
    [Theory]
    [InlineData("AA","A", 1)]
    [InlineData("AB","B", 0)]
    public void Terminal_DropProduct_WithValidCode_ShouldReturnQuantity(string scanOrder, string itemToDrop, decimal expected)
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(PricingData.GetData());

        // Act & Assert
        terminal.ScanProductRange(scanOrder);
        Assert.Equal(expected, actual: terminal.DropProduct(itemToDrop));
    }

    [Theory]
    [InlineData(null, "A")]
    [InlineData("A","B")]
    public void Terminal_DropProduct_WithNoProduct_ShouldReturnInvalidOperationException(string scanOrder, string dropCode)
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(PricingData.GetData());

        // Act & Assert
        terminal.ScanProductRange(scanOrder);
        Assert.Throws<InvalidOperationException>(()=> terminal.DropProduct(dropCode));
    }
}