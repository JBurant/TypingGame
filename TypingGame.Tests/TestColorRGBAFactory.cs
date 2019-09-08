using FluentAssertions;
using TypingGame.Logic;
using Xunit;

namespace TypingGame.Tests
{
    public class TestColorRGBAFactory
    {
        [Fact]
        public void GetColor_ShouldReturnAColor()
        {
            // Arrange
            var dut = new ColorRGBAFactory();

            // Act
            var result = dut.GetColor();

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void GetColor_ShouldReturnNonBlackColor()
        {
            // Arrange
            var dut = new ColorRGBAFactory();

            // Act
            var result = dut.GetColor();

            // Assert
            (result.R + result.G + result.B).Should().BeGreaterThan(0);
        }
    }
}
