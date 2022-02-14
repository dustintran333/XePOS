using System;
using XePOS.Application.Extensions;
using Xunit;

namespace XePos.Tests.Services;

public class DropProductTests : TestBase
{
    [Theory]
    [InlineData("AA","A", 1)]
    [InlineData("AB","B", 0)]
    public void Terminal_DropProduct_WithValidCode_ReturnQuantity(string scanOrder, string itemToDrop, decimal expected)
    {
        // Arrange
        TestSetup();

        // Act & Assert
        Terminal.ScanProductRange(scanOrder);
        Assert.Equal(expected, actual: Terminal.DropProduct(itemToDrop));
    }

    [Theory]
    [InlineData(null, "A")]
    [InlineData("A","B")]
    public void Terminal_DropProduct_WithNoProduct_ThrowInvalidOperationException(string scanOrder, string dropCode)
    {
        // Arrange
        TestSetup();

        // Act & Assert
        Terminal.ScanProductRange(scanOrder);
        Assert.Throws<InvalidOperationException>(()=> Terminal.DropProduct(dropCode));
    }
}