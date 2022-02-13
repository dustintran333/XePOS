using XePOS.Application;
using XePOS.Application.Data;
using Xunit;

namespace XePos.Tests;

public class GetProductPricingTests
{
    [Theory]
    [InlineData("A",1.25)]
    [InlineData("B",4.25)]
    [InlineData("C",1.00)]
    [InlineData("D",0.75)]
    public void Terminal_GetProductPricing_Price_WithValidCode_ShouldReturnProductPrice(string code, decimal expected)
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(PricingData.GetData());

        // Act
        var actual = terminal.GetProductPricing(code).Price;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("A", "A")]
    [InlineData("B", "B")]
    [InlineData("C", "C")]
    [InlineData("D", "D")]
    public void Terminal_GetProductPricing_Code_WithValidCode_ShouldReturnProductCode(string code, string expected)
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();
        terminal.SetPricing(PricingData.GetData());

        // Act
        var actual = terminal.GetProductPricing(code).Code;

        // Assert
        Assert.Equal(expected, actual);
    }
}