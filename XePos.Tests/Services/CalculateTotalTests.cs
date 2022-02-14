using XePOS.Application.Extensions;
using Xunit;

namespace XePos.Tests.Services;

public class CalculateTotalTests : TestBase
{
    [Fact]
    public void Terminal_CalculateTotal_EmptyCart_Return0()
    {
        // Arrange
        // Act & Assert
        Assert.Equal(expected: 0, actual: Terminal.CalculateTotal());
    }

    [Theory]
    [InlineData("ABCDABA", 13.25)]
    [InlineData("CCCCCCC", 6.00)]
    [InlineData("ABCD", 7.25)]
    public void Terminal_CalculateTotal_ReturnTotalPrice(string scanOrder, decimal expected)
    {
        // Arrange
        TestSetup();

        // Act & Assert
        Terminal.ScanProductRange(scanOrder);
        Assert.Equal(expected, actual: Terminal.CalculateTotal());
    }
}