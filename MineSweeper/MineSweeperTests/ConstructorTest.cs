using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;

namespace MineSweeperTests
{
    [TestClass]
    public class ConstructorTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void ConstructorShouldSetSize()
        {
            //Arrange & Act
            var game = new MineSweeperGame(10, 10, 10, new ServiceBus());

            //Assert
            Assert.AreEqual(game.SizeX, 10);
            Assert.AreEqual(game.SizeY, 10);
        }

        [TestMethod]
        public void ConstructorShouldSetCorrectSizeX()
        {
            //Arrange & Act
            var game = new MineSweeperGame(5, 6, 10, new ServiceBus());

            //Assert
            Assert.AreEqual(game.SizeX, 5);
        }

        [TestMethod]
        public void ConstructorShouldSetCorrectSizeY()
        {
            //Arrange & Act
            var game = new MineSweeperGame(5, 6, 10, new ServiceBus());

            //Assert
            Assert.AreEqual(game.SizeY, 6);
        }

        [TestMethod]
        public void ConstructorShouldSetCorrectMineCount()
        {
            //Arrange & Act
            var game = new MineSweeperGame(5, 6, 10, new ServiceBus());

            //Assert
            Assert.AreEqual(game.NumberOfMines, 10);
        }

        [TestMethod]
        public void ConstructorShouldStartInStatePlaying()
        {
            //Arrange & Act
            var game = new MineSweeperGame(5, 6, 10, new ServiceBus());

            //Assert
            Assert.AreEqual(game.State, GameState.Playing);
        }

        [TestMethod]
        public void ConstructorShouldCallResetBoard()
        {
            //Arrange
            var game = new MineSweeperGame(5, 6, 10, new ServiceBus());

            //Act
            var coord = game.GetCoordinate(3, 3);

            //Assert
            Assert.IsNotNull(coord);
        }
    }
}
