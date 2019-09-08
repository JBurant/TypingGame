using FluentAssertions;
using Moq;
using TypingGame.Logic;
using Xunit;

namespace TypingGame.Tests
{
    public class TestStringFileReader
    {
        private readonly Mock<IFileStreamReader> fileStreamReaderMock;
        private string dummyLine = "DummyLine";

        public TestStringFileReader()
        {
            fileStreamReaderMock = new Mock<IFileStreamReader>();
            fileStreamReaderMock.Setup(x => x.ReadLine()).Returns(dummyLine);
        }

        [Fact]
        public void GetBatchOfStrings_EndOfStream_BreaksTheLoop()
        {
            // Arrange
            fileStreamReaderMock.Setup(x => x.EndOfStream).Returns(true);
            var dut = new StringFileReader(fileStreamReaderMock.Object);

            // Act
            var result = dut.GetBatchOfStrings(2);

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public void GetBatchOfStrings_EnoughLinesForElements_ReturnsCorrectList()
        {
            // Arrange
            fileStreamReaderMock.Setup(x => x.EndOfStream).Returns(false);
            var dut = new StringFileReader(fileStreamReaderMock.Object);

            // Act
            var result = dut.GetBatchOfStrings(2);

            // Assert
            result.Count.Should().Be(100);
        }
    }
}
