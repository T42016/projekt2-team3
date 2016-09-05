using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;

namespace MineSweeperTests
{
    [TestClass]
    public class MoveMethodsTest
    {
        private MineSweeperGame _underTest;

        [TestInitialize]
        public void Setup()
        {
            _underTest = new MineSweeperGame(3, 3, 0, new ServiceBus());    
        }

        [TestMethod]
        public void MoveCursorRightShouldMoveRight()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorRight();
            //Assert
            Assert.AreEqual(1, _underTest.PosX);
        }

        [TestMethod]
        public void MoveCursorDownShouldMoveDown()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorDown();
            //Assert
            Assert.AreEqual(1, _underTest.PosY);
        }

        [TestMethod]
        public void MoveCursorDownShouldNotMoveOutsideBoard()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorDown();
            _underTest.MoveCursorDown();
            _underTest.MoveCursorDown();
            //Assert
            Assert.AreEqual(2, _underTest.PosY);
        }

        [TestMethod]
        public void MoveCursorRightShouldNotMoveOutsideBoard()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorRight();
            _underTest.MoveCursorRight();
            _underTest.MoveCursorRight();
            //Assert
            Assert.AreEqual(2, _underTest.PosX);
        }

        [TestMethod]
        public void MoveCursorLeftShouldMoveLeft()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorRight();
            _underTest.MoveCursorRight();
            _underTest.MoveCursorLeft();
            //Assert
            Assert.AreEqual(1, _underTest.PosX);
        }

        [TestMethod]
        public void MoveCursorUpShouldMoveUp()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorDown();
            _underTest.MoveCursorDown();
            _underTest.MoveCursorUp();
            //Assert
            Assert.AreEqual(1, _underTest.PosY);
        }

        [TestMethod]
        public void MoveCursorUpShouldNotMoveOutsideBoard()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorUp();
            //Assert
            Assert.AreEqual(0, _underTest.PosY);
        }

        [TestMethod]
        public void MoveCursorLeftShouldNotMoveOutsideBoard()
        {
            //Arrange via Setup
            //Act
            _underTest.MoveCursorLeft();
            //Assert
            Assert.AreEqual(0, _underTest.PosX);
        }
    }
}
