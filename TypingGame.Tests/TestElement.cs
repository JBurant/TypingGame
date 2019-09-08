using FluentAssertions;
using TypingGame.Logic;
using Xunit;

namespace TypingGame.Tests
{
    public class TestElement
    {
        [Theory]
        [InlineData(0.1, 1.5)]
        [InlineData(0, 0.5)]
        public void Move_ShouldCorrectlyAdjustPositionX(decimal deltaTime, decimal expectedPositionX)
        {
            // Arrange
            var dut = new Element("TestString", new ColorRGBA(), 10, 0.5M, 0);

            // Act
            dut.Move(deltaTime);

            // Assert
            dut.X.Should().Be(expectedPositionX);
        }

        [Theory]
        [InlineData("TestData", "estData", "T")]
        [InlineData("a", "", "a")]
        public void TryHit_ShouldRemoveLetter(string originalText, string expectedModifiedText, string letterToRemove)
        {
            // Arrange
            var dut = new Element(originalText, new ColorRGBA(), 0, 0, 0);

            // Act
            dut.TryHit(letterToRemove);

            // Assert
            dut.Text.Should().Be(expectedModifiedText);
        }

        [Theory]
        [InlineData("TestData", true, "T")]
        [InlineData("a", false, "a")]
        public void Hit_ShouldSetIsHitCorrectly(string originalText, bool expectedIsHit, string letterToRemove)
        {
            // Arrange
            var dut = new Element(originalText, new ColorRGBA(), 0, 0, 0);

            // Act
            dut.TryHit(letterToRemove);

            // Assert
            dut.IsHit.Should().Be(expectedIsHit);
        }
    }
}
