using FluentAssertions;
using Microsoft.Xna.Framework.Input;
using TypingGame.Inputs;
using Xunit;

namespace TypingGame.Tests
{
    public class TestKeysLettersTranslator
    {
        [Theory]
        [InlineData(Keys.A, "a")]
        [InlineData(Keys.B, "b")]
        [InlineData(Keys.C, "c")]
        [InlineData(Keys.D, "d")]
        [InlineData(Keys.E, "e")]
        [InlineData(Keys.F, "f")]
        [InlineData(Keys.G, "g")]
        [InlineData(Keys.H, "h")]
        [InlineData(Keys.I, "i")]
        [InlineData(Keys.J, "j")]
        [InlineData(Keys.K, "k")]
        [InlineData(Keys.L, "l")]
        [InlineData(Keys.M, "m")]
        [InlineData(Keys.N, "n")]
        [InlineData(Keys.O, "o")]
        [InlineData(Keys.P, "p")]
        [InlineData(Keys.Q, "q")]
        [InlineData(Keys.R, "r")]
        [InlineData(Keys.S, "s")]
        [InlineData(Keys.T, "t")]
        [InlineData(Keys.U, "u")]
        [InlineData(Keys.V, "v")]
        [InlineData(Keys.W, "w")]
        [InlineData(Keys.X, "x")]
        [InlineData(Keys.Y, "y")]
        [InlineData(Keys.Z, "z")]
        public void TranslateKeyToString_ValidKey_ReturnsCorrectLetter(Keys inputKey, string expectedLetter)
        {
            // Arrange
            var dut = new KeysLettersTranslator();

            // Act
            var result = dut.TranslateKeyToString(inputKey);

            // Assert
            result.Should().Be(expectedLetter);
        }

        [Theory]
        [InlineData(Keys.Back)]
        [InlineData(Keys.Enter)]
        [InlineData(Keys.Space)]
        public void TranslateKeyToString_InvalidKey_ReturnsNull(Keys inputKey)
        {
            // Arrange
            var dut = new KeysLettersTranslator();

            // Act
            var result = dut.TranslateKeyToString(inputKey);

            // Assert
            result.Should().Be(null);
        }

    }
}
