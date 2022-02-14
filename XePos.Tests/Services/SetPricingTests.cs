using System;
using System.Collections.Generic;
using XePOS.Application.Data;
using XePOS.Application.Entities;
using XePOS.Application.Services;
using Xunit;

namespace XePos.Tests.Services;

public class SetPricingTests
{
    [Fact]
    public void Terminal_SetPricing_ReturnPricing()
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();

        // Act
        terminal.SetPricing(new List<Product>
        {
            new() { Code = "A", Price = 1.25m, Promotion = new Promotion(3, 3m) },
            new() { Code = "B", Price = 4.25m },
            new() { Code = "C", Price = 1.00m, Promotion = new Promotion(6, 5m) },
            new() { Code = "D", Price = 0.75m },
        });

        // Assert
        Assert.Equal(expected: PricingData.GetData(), actual:terminal.GetProductPricing());
    }
    
    [Theory]
    [InlineData(-1.0)]
    public void Terminal_SetPricing_WithNegativePrice_ThrowArgumentException(decimal price)
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => terminal.SetPricing(new List<Product>
        {
            new() { Code = "Z", Price = price },
        }));
    }

    [Fact]
    public void Terminal_SetPricing_WithCodeNotUnique_ThrowArgumentException()
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => terminal.SetPricing(new List<Product>
        {
            // duplicate product code
            new() { Code = "A", Price = 1.25m },
            new() { Code = "A", Price = 4.25m }
        }));
    }

    [Fact]
    public void Terminal_NotSetPricing_ReturnEmptyList()
    {
        // Arrange
        var terminal = new PointOfSaleTerminal();


        // Act & Assert
        Assert.Empty(terminal.GetProductPricing());
    }
}