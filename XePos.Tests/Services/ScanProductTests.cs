using System;
using System.Linq;
using Xunit;

namespace XePos.Tests.Services
{
    public class ScanProductTests : TestBase
    {
        [Theory]
        [InlineData("F")]
        public void Terminal_ScanProduct_WithInvalidCode_ThrowInvalidOperationException(string code)
        {
            // Arrange
            TestSetup();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => Terminal.GetProductPricing(code));
        }

        [Fact]
        public void Terminal_Scan1Product_Return1()
        {
            // Arrange
            TestSetup();

            // Act & Assert
            Assert.Equal(expected: 1, Terminal.ScanProduct("A"));
        }

        [Theory]
        [InlineData(new[] { "A", "A", "A" }, 3)]
        public void Terminal_ScanNProduct_ReturnN(string[] codes, int expected)
        {
            // Arrange
            TestSetup();

            // Act
            var actual = codes.Select(c => Terminal.ScanProduct(c.ToString())).ToArray().LastOrDefault();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}