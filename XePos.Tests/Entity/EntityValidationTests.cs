using System.Collections.Generic;
using XePOS.Application.Entities;
using XePOS.Application.Extensions;
using Xunit;

namespace XePos.Tests.Entity;

public class EntityValidationTests
{
    [Fact]
    public void WhenProductDataIsInvalid_Expect_IsPricingDataValidAsFalse()
    {
        // Arrange
        var invalidPricingList = new List<Product> { new() { Code = "", Price = -0.001m } };

        // Act & Assert
        Assert.False(invalidPricingList.IsPricingDataValid());
    }
    
    [Fact]
    public void WhenProductDataIsValid_Expect_IsPricingDataValidAsTrue()
    {
        // Arrange
        var validPricingList = new List<Product> { new() { Code = "A", Price = 0.001m } };

        // Act & Assert
        Assert.True(validPricingList.IsPricingDataValid());
    }

    [Fact]
    public void WhenPromotionDataIsInvalid_Expect_IsPricingDataValidAsFalse()
    {
        // Arrange
        var validPricingList = new List<Product> { new() { Code = "A", Price = 1.0m, Promotion = new Promotion(0, -2.0m) } };

        // Act & Assert
        Assert.False(validPricingList.IsPricingDataValid());
    }

    [Fact]
    public void WhenPromotionDataIsValid_Expect_IsPricingDataValidAsTrue()
    {
        // Arrange
        var validPricingList = new List<Product> { new() { Code = "A", Price = 1.0m , Promotion = new Promotion(3, 2.0m) } };

        // Act & Assert
        Assert.True(validPricingList.IsPricingDataValid());
    }
}