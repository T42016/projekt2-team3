using System;
using System.Text;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;

namespace MineSweeperTests
{
    [TestClass]
    public class ResetBoardTest
    {
        MineSweeperGame _underTest;

        [TestInitialize]
        public void Setup()
        {
            IServiceBus bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).Returns(1);
            _underTest = new MineSweeperGame(3, 3, 1, bus);
        }

        [TestMethod]
        public void ResetBoardShouldCreatePositionInfoForAllBoardPositions()
        {
            //Arrange via Setup

            //Act
            _underTest.ResetBoard();

            //Assert
            Assert.IsNotNull(_underTest.GetCoordinate(0, 0));

            for (int y = 0; y < _underTest.SizeY; y++)
            {
                for (int x = 0; x < _underTest.SizeX; x++)
                {
                    Assert.IsInstanceOfType(_underTest.GetCoordinate(x, y), typeof(PositionInfo));
                }
            }
        }

        [TestMethod]
        public void ResetBoardShouldPlaceCorrectNumberOfMinesOnBoard()
        {
            //Arrange via Setup
            int nrOfMines = 0;

            //Act
            _underTest.ResetBoard();

            //
            for (int y = 0; y < _underTest.SizeY; y++)
            {
                for (int x = 0; x < _underTest.SizeX; x++)
                {
                    if (_underTest.GetCoordinate(x, y).HasMine)
                        nrOfMines++;
                }
            }

            //Assert
            Assert.IsNotNull(_underTest.GetCoordinate(0, 0));
            Assert.AreEqual(_underTest.NumberOfMines, nrOfMines);
        }

        [TestMethod]
        public void ResetBoardShouldInitializePositionInfoWithX()
        {
            //Arrange via Setup

            //Act
            _underTest.ResetBoard();

            //Assert
            for (int y = 0; y < _underTest.SizeY; y++)
            {
                for (int x = 0; x < _underTest.SizeX; x++)
                {
                    Assert.AreEqual(x, _underTest.GetCoordinate(x, y).X);
                }
            }
            Assert.IsNotNull(_underTest.GetCoordinate(0, 0));
        }

        [TestMethod]
        public void ResetBoardShouldInitializePositionInfoWithY()
        {
            //Arrange via Setup

            //Act
            _underTest.ResetBoard();

            //Assert
            for (int y = 0; y < _underTest.SizeY; y++)
            {
                for (int x = 0; x < _underTest.SizeX; x++)
                {
                    Assert.AreEqual(y, _underTest.GetCoordinate(x, y).Y);
                }
            }
            Assert.IsNotNull(_underTest.GetCoordinate(0, 0));
        }

        [TestMethod]
        public void ResetBoardShouldNotFlagAnyCoordinates()
        {
            //Arrange via Setup

            //Act
            _underTest.ResetBoard();

            //Assert
            Assert.IsNotNull(_underTest.GetCoordinate(0, 0));
            for (int y = 0; y < _underTest.SizeY; y++)
            {
                for (int x = 0; x < _underTest.SizeX; x++)
                {
                    Assert.AreEqual(false, _underTest.GetCoordinate(x, y).IsFlagged);
                }
            }
        }

        [TestMethod]
        public void ResetBoardShouldSetAllCoordinatesToNotOpen()
        {
            //Arrange via Setup

            //Act
            _underTest.ResetBoard();

            //Assert
            for (int y = 0; y < _underTest.SizeY; y++)
            {
                for (int x = 0; x < _underTest.SizeX; x++)
                {
                    Assert.AreEqual(false, _underTest.GetCoordinate(x, y).IsOpen);
                }
            }
            Assert.IsNotNull(_underTest.GetCoordinate(0, 0));
        }

        [TestMethod]
        public void ResetBoardShouldResetGameStateToPlaying()
        {
            //Arrange
            var game = new MineSweeperGame(3, 3, 0, new ServiceBus());

            //Act
            game.ClickCoordinate();
            game.ResetBoard();
            
            //Assert
            Assert.AreEqual(GameState.Playing, game.State);
        }

        [TestMethod]
        public void ResetBoardShouldCalaculateNeighbouringNrOfMinesTopLeft()
        {
            //Act
            var coord = _underTest.GetCoordinate(0, 0);

            //Assert
            Assert.AreEqual(1, coord.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalaculateNeighbouringNrOfMinesTopRight()
        {
            //Act
            var coord = _underTest.GetCoordinate(2, 0);

            //Assert
            Assert.AreEqual(coord.NrOfNeighbours, 1);
        }

        [TestMethod]
        public void ResetBoardShouldCalaculateNeighbouringNrOfMinesBottomRight()
        {
            //Act
            var coord = _underTest.GetCoordinate(2, 2);

            //Assert
            Assert.AreEqual(coord.NrOfNeighbours, 1);
        }

        [TestMethod]
        public void ResetBoardShouldCalaculateNeighbouringNrOfMinesBottomLeft()
        {
            //Arrange & assume
            //Act
            var coord = _underTest.GetCoordinate(0, 2);
            //Assert
            Assert.AreEqual(coord.NrOfNeighbours, 1);
        }

        [TestMethod]
        public void ResetBoardShouldCalaculateNeighbouringNrOfMinesLargerThan1()
        {
            //Arrange & assume
            IServiceBus bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(1, 1, 2, 2);
            var game = new MineSweeperGame(3, 3, 2, bus);

            //Act
            var coord1 = game.GetCoordinate(2, 1);
            var coord2 = game.GetCoordinate(1, 2);

            //Assert
            Assert.AreEqual(2, coord1.NrOfNeighbours);
            Assert.AreEqual(2, coord2.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate8Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1, 0, 2, 1, 2, 2, 2);

            //Act
            var game = new MineSweeperGame(3, 3, 8, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(8, coordinate.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate7Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1, 0, 2, 1, 2);

            //Act
            var game = new MineSweeperGame(3, 3, 7, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(7, coordinate.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate6Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1, 0, 2);

            //Act
            var game = new MineSweeperGame(3, 3, 6, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(6, coordinate.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate5Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1, 2, 1);

            //Act
            var game = new MineSweeperGame(3, 3, 5, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(5, coordinate.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate4Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0, 0, 1);

            //Act
            var game = new MineSweeperGame(3, 3, 4, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(4, coordinate.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate3Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0, 2, 0);

            //Act
            var game = new MineSweeperGame(3, 3, 3, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(3, coordinate.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate2Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0, 1, 0);

            //Act
            var game = new MineSweeperGame(3, 3, 2, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(2, coordinate.NrOfNeighbours);
        }

        [TestMethod]
        public void ResetBoardShouldCalculate1Neighbours()
        {
            //Arrange
            var bus = A.Fake<IServiceBus>();
            A.CallTo(() => bus.Next(A<int>.Ignored)).ReturnsNextFromSequence(0, 0);

            //Act
            var game = new MineSweeperGame(3, 3, 1, bus);
            var coordinate = game.GetCoordinate(1, 1);
            //Assert
            Assert.AreEqual(1, coordinate.NrOfNeighbours);
        }
    }
}
