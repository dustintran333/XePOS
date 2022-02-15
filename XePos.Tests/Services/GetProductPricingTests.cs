using System;
using Xunit;

namespace XePos.Tests.Services;

public class GetProductPricingTests : TestBase
{
    [Theory]
    [InlineData("A", 1.25)]
    [InlineData("B", 4.25)]
    [InlineData("C", 1.00)]
    [InlineData("D", 0.75)]
    public void Terminal_GetProductPricing_Price_WithValidCode_ReturnsProductPrice(string code, decimal expected)
    {
        // Arrange
        TestSetup();

        // Act
        var actual = Terminal.GetProductPricing(code).Price;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("A", "A")]
    [InlineData("B", "B")]
    [InlineData("C", "C")]
    [InlineData("D", "D")]
    public void Terminal_GetProductPricing_Code_WithValidCode_ReturnsProductCode(string code, string expected)
    {
        // Arrange
        TestSetup();

        // Act
        var actual = Terminal.GetProductPricing(code).Code;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Terminal_GetProductPricing_Code_WithNullOrEmptyCode_ThrowsArgumentException(string code)
    {
        // Arrange
        TestSetup();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Terminal.GetProductPricing(code));
    }

    [Theory]
    [InlineData("S")]
    public void Terminal_GetProductPricing_Code_WithWrongCode_ThrowsInvalidOperationException(string code)
    {
        // Arrange
        TestSetup();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Terminal.GetProductPricing(code));
    }
}