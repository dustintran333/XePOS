using XePOS.Application;
using XePOS.Application.Data;
using XePOS.Application.Extensions;
using Xunit;

namespace XePos.Tests;

public class CalculateTotalTests
{
    [Fact]
    public void Terminal_CalculateTotal_EmptyCart_ShouldReturn0()
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();
        // Act & Assert
        Assert.Equal(expected: 0, actual: terminal.CalculateTotal());
    }
    
    [Theory]
    [InlineData("ABCDABA", 13.25)]
    [InlineData("CCCCCCC", 6.00)]
    [InlineData("ABCD", 7.25)]
    public void Terminal_CalculateTotal_ShouldReturnTotalPrice(string scanOrder, decimal expected)
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(PricingData.GetData());

        // Act & Assert
        terminal.ScanProductRange(scanOrder);
        Assert.Equal(expected, actual: terminal.CalculateTotal());
    }
}