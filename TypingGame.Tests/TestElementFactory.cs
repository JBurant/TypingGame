using FluentAssertions;
using Moq;
using System.Collections.Generic;
using TypingGame.Logic;
using Xunit;

namespace TypingGame.Tests
{
    public class TestElementFactory
    {
        private readonly Mock<IColorRGBAFactory> colorRGBAFactoryMock;
        private readonly Mock<IStringFileReader> stringFileReaderMock;
        private readonly ColorRGBA dummyColor;
        private readonly List<string> dummyBatchOfStrings;
        private readonly List<string> secondDummyBatchOfStrings;

        public TestElementFactory()
        {
            dummyColor = new ColorRGBA() { R = 256 };
            dummyBatchOfStrings = new List<string> { "TestString1", "TestString2", "TestString3" };
            secondDummyBatchOfStrings = new List<string> { "AnotherTestString", "AnotherTestString2" };
            colorRGBAFactoryMock = new Mock<IColorRGBAFactory>();
            stringFileReaderMock = new Mock<IStringFileReader>();

            colorRGBAFactoryMock.Setup(x => x.GetColor()).Returns(dummyColor);
            stringFileReaderMock.Setup(x => x.GetBatchOfStrings(3)).Returns(dummyBatchOfStrings);
            stringFileReaderMock.Setup(x => x.GetBatchOfStrings(1)).Returns(secondDummyBatchOfStrings);
        }

        [Fact]
        public void GetElements_NoStringsReady_CallsGetBatchOfStrings()
        {
            // Arrange
            var dut = new ElementFactory(stringFileReaderMock.Object, colorRGBAFactoryMock.Object);

            // Act
            dut.GetElements(3);

            // Assert
            stringFileReaderMock.Verify(x => x.GetBatchOfStrings(3), Times.Once);
        }

        [Fact]
        public void GetElements_ProperRequest_ReturnsListOfElements()
        {
            // Arrange
            var dut = new ElementFactory(stringFileReaderMock.Object, colorRGBAFactoryMock.Object);

            // Act
            var result = dut.GetElements(3);

            // Assert
            result.Count.Should().Be(3);
            result[0].Text.Should().Be(dummyBatchOfStrings[0]);
            result[1].Text.Should().Be(dummyBatchOfStrings[1]);
            result[2].Text.Should().Be(dummyBatchOfStrings[2]);
        }

        [Fact]
        public void GetElements_CalledTwice_ReturnsCorrectElements()
        {
            // Arrange
            var dut = new ElementFactory(stringFileReaderMock.Object, colorRGBAFactoryMock.Object);

            // Act
            var result1 = dut.GetElements(3);
            var result2 = dut.GetElements(1);

            // Assert
            result2.Count.Should().Be(1);
            result2[0].Text.Should().Be(secondDummyBatchOfStrings[0]);
        }
    }
}
