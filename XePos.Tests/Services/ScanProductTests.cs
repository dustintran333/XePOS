using System;
using System.Linq;
using Xunit;

namespace XePos.Tests.Services
{
    public class ScanProductTests : TestBase
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Terminal_ScanProduct_WithNullOrEmptyCode_ThrowsArgumentException(string code)
        {
            // Arrange
            TestSetup();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Terminal.ScanProduct(code));
        }

        [Theory]
        [InlineData("F")]
        public void Terminal_ScanProduct_WithWrongCode_ThrowsInvalidOperationException(string code)
        {
            // Arrange
            TestSetup();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => Terminal.ScanProduct(code));
        }

        [Fact]
        public void Terminal_Scan1Product_Returns1()
        {
            // Arrange
            TestSetup();

            // Act & Assert
            Assert.Equal(expected: 1, Terminal.ScanProduct("A"));
        }

        [Theory]
        [InlineData(new[] { "A", "A", "A" }, 3)]
        public void Terminal_ScanNProduct_ReturnsN(string[] codes, int expected)
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