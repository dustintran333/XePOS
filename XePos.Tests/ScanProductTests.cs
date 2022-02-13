using System;
using System.Linq;
using XePOS.Application;
using XePOS.Application.Data;
using Xunit;

namespace XePos.Tests
{
    public class ScanProductTests
    {
        [Theory]
        [InlineData("F")]
        public void Terminal_ScanProduct_WithInvalidCode_ShouldReturnInvalidOperationException(string code)
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            terminal.SetPricing(PricingData.GetData());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => terminal.GetProductPricing(code));
        }

        [Fact]
        public void Terminal_Scan1Product_ShouldReturn1()
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            terminal.SetPricing(PricingData.GetData());

            // Act & Assert
            Assert.Equal(expected: 1, terminal.ScanProduct("A"));
        }

        [Theory]
        [InlineData(new[] { "A", "A", "A" }, 3)]
        public void Terminal_ScanNProduct_ShouldReturnN(string[] codes, int expected)
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            terminal.SetPricing(PricingData.GetData());

            // Act
            var actual = codes.Select(c => terminal.ScanProduct(c.ToString())).ToArray().LastOrDefault();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}