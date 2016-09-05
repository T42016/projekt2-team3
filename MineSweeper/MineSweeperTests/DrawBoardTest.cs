using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;

namespace MineSweeperTests
{
    [TestClass]
    public class DrawBoardTest
    {
        private MineSweeperGame _underTest;
        private MockedServiceBus _mockedServiceBus;

        [TestInitialize]
        public void Setup()
        {
            _mockedServiceBus = new MockedServiceBus();
            _underTest = new MineSweeperGame(2, 2, 1, _mockedServiceBus);    
        }

        [TestMethod]
        public void DrawBoardShouldCallWriteLine()
        {
            //Act
            _underTest.DrawBoard();
            //Assert
            Assert.AreEqual(2, _mockedServiceBus.NrOfWriteLineCalls);
        }

        [TestMethod]
        public void DrawBoardShouldCallWriteProportionalToBoardSize()
        {
            //Act
            _underTest.DrawBoard();
            //Assert
            Assert.AreEqual(4, _mockedServiceBus.NrOfWriteCalls);
        }

        [TestMethod]
        public void DrawBoardShouldDrawCorrectSymbolForUnOpenedCoordinates()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            var game = new MineSweeperGame(2, 2, 0, bus);

            //Act
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("? ")).MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [TestMethod]
        public void DrawBoardShouldDrawCorrectSymbolForMarkedUnOpenedCoordinates()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            var game = new MineSweeperGame(2, 2, 0, bus);

            //Act
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("? ", ConsoleColor.DarkCyan)).MustHaveHappened(Repeated.Exactly.Times(1));
        }

        [TestMethod]
        public void DrawBoardShouldDrawCorrectSymbolForOpenedCoordinates()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            var game = new MineSweeperGame(2, 2, 0, bus);

            //Act
            game.ClickCoordinate();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write(". ")).MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [TestMethod]
        public void DrawBoardShouldDrawCorrectSymbolForMarkedOpenedCoordinates()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            var game = new MineSweeperGame(2, 2, 0, bus);

            //Act
            game.ClickCoordinate();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write(". ", ConsoleColor.DarkCyan)).MustHaveHappened(Repeated.Exactly.Times(1));
        }


        [TestMethod]
        public void DrawBoardShouldDrawMarkedPositionBasedOnPosXAndPosY()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0);
            var game = new MineSweeperGame(3, 3, 2, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("2 ", ConsoleColor.DarkCyan)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor1Neighbour()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(1, 1);
            var game = new MineSweeperGame(2, 2, 1, bus);

            //Act
            game.ClickCoordinate();
            game.MoveCursorRight();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("1 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor2Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0);
            var game = new MineSweeperGame(3, 3, 2, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.MoveCursorUp();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("2 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor3Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0);
            var game = new MineSweeperGame(3, 3, 3, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.MoveCursorUp();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("3 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor4Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1);
            var game = new MineSweeperGame(3, 3, 4, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.MoveCursorUp();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("4 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor5Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1);
            var game = new MineSweeperGame(3, 3, 5, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.MoveCursorUp();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("5 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor6Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1, 0, 2);
            var game = new MineSweeperGame(3, 3, 6, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.MoveCursorUp();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("6 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor7Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1, 0, 2, 1, 2);
            var game = new MineSweeperGame(3, 3, 7, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.MoveCursorUp();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("7 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolFor8Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1, 0, 2, 1, 2, 2, 2);
            var game = new MineSweeperGame(3, 3, 8, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            game.MoveCursorUp();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("8 ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolForMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0);
            var game = new MineSweeperGame(2, 2, 1, bus);

            //Act
            game.ClickCoordinate();
            game.MoveCursorRight();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("X ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawMarkedSymbolForMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0);
            var game = new MineSweeperGame(2, 2, 1, bus);

            //Act
            game.ClickCoordinate();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("X ", ConsoleColor.DarkCyan)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawSymbolForFlag()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            var game = new MineSweeperGame(2, 2, 0, bus);

            //Act
            game.FlagCoordinate();
            game.MoveCursorRight();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("! ")).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void DrawBoardShouldDrawMarkedSymbolForFlag()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            var game = new MineSweeperGame(2, 2, 0, bus);

            //Act
            game.FlagCoordinate();
            game.DrawBoard();
            //Assert
            A.CallTo(() => bus.Write("! ", ConsoleColor.DarkCyan)).MustHaveHappened(Repeated.Exactly.Once);
        }

    }
}
