using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;

namespace MineSweeperTests
{
    [TestClass]
    public class FlagCoordinateTest
    {
        private MineSweeperGame _underTest;

        [TestInitialize]
        public void Setup()
        {
            _underTest = new MineSweeperGame(5, 5, 0, new ServiceBus());            
        }

        [TestMethod]
        public void FlagCoordinateShouldFlagNonOpenCoordinate()
        {
            //Arrange via setup
            //Act
            _underTest.FlagCoordinate();
            var coord = _underTest.GetCoordinate(0, 0);

            //Assert
            Assert.AreEqual(coord.IsFlagged, true);
        }

        [TestMethod]
        public void FlagCoordinateShouldUsePosXAndPosY()
        {
            //Arrange via setup
            //Act
            _underTest.MoveCursorRight();
            _underTest.MoveCursorDown();
            _underTest.FlagCoordinate();
            var coord = _underTest.GetCoordinate(1, 1);

            //Assert
            Assert.AreEqual(coord.IsFlagged, true);
        }

        [TestMethod]
        public void FlagCoordinateShouldNotFlagOpenCoordinate()
        {
            //Arrange via setup
            //Act
            _underTest.ClickCoordinate();
            _underTest.FlagCoordinate();
            var coord = _underTest.GetCoordinate(0, 0);

            //Assert
            Assert.AreEqual(coord.IsFlagged, false);
        }

        [TestMethod]
        public void FlagCoordinateShouldToogleFlaggedCoordinate()
        {
            //Arrange via setup
            //Act
            _underTest.FlagCoordinate();
            _underTest.FlagCoordinate();
            var coord = _underTest.GetCoordinate(0, 0);

            //Assert
            Assert.AreEqual(coord.IsFlagged, false);
        }
    }
}
