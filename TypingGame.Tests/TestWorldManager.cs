using Moq;
using System.Collections.Generic;
using TypingGame.Logic;
using Xunit;
using FizzWare.NBuilder;
using FluentAssertions;

namespace TypingGame.Tests
{
    public class TestWorldManager
    {
        private readonly Mock<IElementFactory> elementFactoryMock;
        private readonly decimal dummyDeltaTime = 0.1M;
        private readonly decimal dummySpeed = 1;

        public TestWorldManager()
        {
            elementFactoryMock = new Mock<IElementFactory>();
        }

        [Fact]
        public void AddNewElements_CallsElementFactory()
        {
            // Arrange
            var dut = new WorldManager(elementFactoryMock.Object);
            elementFactoryMock.Setup(x => x.GetElements(It.IsAny<int>())).Returns(new List<Element> { new Element("", new ColorRGBA(), dummySpeed, 0, 0)});

            // Act
            dut.AddNewElements(1);

            // Assert
            elementFactoryMock.Verify(x => x.GetElements(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Move_ExistingElements_ShouldMoveThem()
        {
            // Arrange
            var dut = new WorldManager(elementFactoryMock.Object);
            dut.Elements = Builder<Element>.CreateListOfSize(2)
                .All()
                    .With(x => x.Speed, 1)
                    .And(x => x.X, 0)
                .Build() as List<Element>;

            // Act
            dut.Move(dummyDeltaTime);

            // Assert
            dut.Elements.TrueForAll(x => x.X == dummyDeltaTime).Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 1, GameState.RUNNING)]
        [InlineData(0, -1, GameState.FAILED)]
        [InlineData(100, 1, GameState.WON)]
        [InlineData(100, -1, GameState.FAILED)]
        public void CheckEndConditions_LivesAndLevel_ReturnsCorrectGameState(int level, int lives, GameState gameState)
        {
            // Arrange
            var dut = new WorldManager(elementFactoryMock.Object);
            dut.Level = level;
            dut.Lives = lives;

            // Act
            var result = dut.CheckEndConditions();

            // Assert
            result.Should().Be(gameState);
        }

        [Fact]
        public void RemoveOldElements_HasElementWithNoText_RemovesIt()
        {
            // Arrange
            var dut = new WorldManager(elementFactoryMock.Object);
            dut.Elements = Builder<Element>.CreateListOfSize(2)
               .All()
                   .With(x => x.X, 0)
               .TheFirst(1)
                   .With(x => x.Text, "Text")
               .TheNext(1)
                   .With(x => x.Text, "")
               .Build() as List<Element>;

            // Act
            dut.RemoveOldElements();

            // Assert
            dut.Elements.Count.Should().Be(1);
        }

        [Fact]
        public void RemoveOldElements_HasElementWithXPositionTooBig_RemovesIt()
        {
            // Arrange
            var dut = new WorldManager(elementFactoryMock.Object);
            dut.Elements = Builder<Element>.CreateListOfSize(2)
                .All()
                    .With(x => x.Text, "TestText")
                .TheFirst(1)
                    .With(x => x.X, 0)
                .TheNext(1)
                    .With(x => x.X, 10 )
                .Build() as List<Element>;

            // Act
            dut.RemoveOldElements();

            // Assert
            dut.Elements.Count.Should().Be(1);
        }
    }
}
