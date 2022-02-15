using System;
using XePOS.Application.Extensions;
using Xunit;

namespace XePos.Tests.Services;

public class CalculateTotalTests : TestBase
{
    [Theory]
    [InlineData("ABCDABA", 13.25)]
    [InlineData("CCCCCCC", 6.00)]
    [InlineData("ABCD", 7.25)]
    public void Terminal_CalculateTotal_ReturnsTotalPrice(string scanOrder, decimal expected)
    {
        // Arrange
        TestSetup();

        // Act & Assert
        Terminal.ScanProductRange(scanOrder);
        Assert.Equal(expected, actual: Terminal.CalculateTotal());
    }

    [Fact]
    public void Terminal_CalculateTotal_WithEmptyCart_ThrowsInvalidOperationException()
    {
        // Arrange
        TestSetup();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Terminal.CalculateTotal());
    }
}