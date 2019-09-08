using FluentAssertions;
using Microsoft.Xna.Framework.Input;
using Moq;
using TypingGame.Inputs;
using Xunit;

namespace TypingGame.Tests
{
    public class TestKeyboardLetterTostringMapper
    {
        [Fact]
        public void MapKeysToStrings_NoOldKeys_AddsAllTheKeys()
        {
            // Arrange
            var mockTranslator = new Mock<IKeysLettersTranslator>();
            mockTranslator.Setup(x => x.TranslateKeyToString(It.IsAny<Keys>())).Returns("A");
            Keys[] testArray = { Keys.A, Keys.M, Keys.Z };

            var dut = new KeyboardLetterToStringMapper(mockTranslator.Object);

            // Act
            var result = dut.MapKeysToStrings(testArray);

            // Assert
            result.Count.Should().Be(3);
        }

        [Fact]
        public void MapKeysToStrings_OldKeys_FiltersOnlyNewKeys()
        {
            // Arrange
            var mockTranslator = new Mock<IKeysLettersTranslator>();
            mockTranslator.Setup(x => x.TranslateKeyToString(It.IsAny<Keys>())).Returns("A");
            Keys[] OldTestArray = { Keys.B, Keys.M };
            Keys[] testArray = { Keys.A, Keys.M, Keys.Z };

            var dut = new KeyboardLetterToStringMapper(mockTranslator.Object);
            dut.MapKeysToStrings(OldTestArray);

            // Act
            var result = dut.MapKeysToStrings(testArray);

            // Assert
            result.Count.Should().Be(2);
        }
    }
}
