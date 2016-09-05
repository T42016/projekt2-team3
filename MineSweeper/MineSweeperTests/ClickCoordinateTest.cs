using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;
using FakeItEasy;

namespace MineSweeperTests
{
    [TestClass]
    public class ClickCoordinateTest
    {
        private MineSweeperGame _underTest;

        [TestInitialize]
        public void Setup()
        {
            _underTest = new MineSweeperGame(3, 3, 0, new ServiceBus());    
        }

        [TestMethod]
        public void ClickCoordinateShouldOpenBoardCoordinate()
        {
            //Arrange via Setup
            //Act
            _underTest.ClickCoordinate();
            //Assert
            Assert.AreEqual(true, _underTest.GetCoordinate(0, 0).IsOpen);
        }

        [TestMethod]
        public void ClickCoordinateShouldUsePosXAndPosY()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0);
            var game = new MineSweeperGame(2, 2, 1, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            Assert.AreEqual(true, game.GetCoordinate(1, 1).IsOpen);
        }

        [TestMethod]
        public void ClickCoordinateShouldNotOpenFlaggedBoardCoordinate()
        {
            //Arrange via Setup
            //Act
            _underTest.FlagCoordinate();
            _underTest.ClickCoordinate();
            //Assert
            Assert.AreEqual(false, _underTest.GetCoordinate(0, 0).IsOpen);
        }

        [TestMethod]
        public void ClickCoordinateShouldTriggerFloodFill()
        {
            //Arrange via Setup
            //Act
            _underTest.ClickCoordinate();
            int openCount = 0;

            for (int y = 0; y < _underTest.SizeY; y++)
            {
                for (int x = 0; x < _underTest.SizeX; x++)
                {
                    if (_underTest.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            //Assert
            Assert.IsNotNull(_underTest.GetCoordinate(0, 0));
            Assert.AreEqual(_underTest.SizeY * _underTest.SizeX, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldDetectGameWin()
        {
            //Arrange via Setup
            //Act
            _underTest.ClickCoordinate();
            //Assert
            Assert.AreEqual(GameState.Won, _underTest.State);
        }

        [TestMethod]
        public void ClickCoordinateShouldDetectGameLoss()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(1, 1);
            var game = new MineSweeperGame(2, 2, 1, bus);

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            
            Assert.AreEqual(GameState.Lost, game.State);
        }

        [TestMethod]
        public void ClickCoordinateShouldOpenAllMineCoordinatesIfMineHit()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(1, 1, 0, 0);
            var game = new MineSweeperGame(2, 2, 2, bus);

            //Act
            game.ClickCoordinate();
            var coord = game.GetCoordinate(1, 1);

            //Assert
            Assert.AreEqual(true, coord.IsOpen);
        }

        [TestMethod]
        public void ClickCoordinateShouldDetectGameWinWhenAllSafePositionsAreOpened()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);

            //Act
            game.ClickCoordinate();
            //Assert
            Assert.AreEqual(GameState.Won, game.State);
        }

        #region ClickCoordinateShouldOnlyOpenOneCoordinate Near Mine

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateSouthOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateSouthEastOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateSouthWestOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateWestOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateNorthWestOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateNorthOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateNorthEastOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        [TestMethod]
        public void ClickCoordinateShouldOnlyOpenOneCoordinateEastOfMine()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(2, 2);
            var game = new MineSweeperGame(5, 5, 1, bus);
            int openCount = 0;

            //Act
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorRight();
            game.MoveCursorDown();
            game.MoveCursorDown();
            game.ClickCoordinate();
            //Assert
            for (int y = 0; y < game.SizeY; y++)
            {
                for (int x = 0; x < game.SizeX; x++)
                {
                    if (game.GetCoordinate(x, y).IsOpen)
                        openCount++;
                }
            }
            Assert.AreEqual(1, openCount);
        }

        #endregion
    }
}
