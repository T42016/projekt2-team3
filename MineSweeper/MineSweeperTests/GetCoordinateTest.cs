using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;

namespace MineSweeperTests
{
    [TestClass]
    public class GetCoordinateTest
    {
        private MineSweeperGame _underTest;

        [TestInitialize]
        public void Setup()
        {
            _underTest = new MineSweeperGame(5, 5, 0, new ServiceBus());    
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetCoordinateShouldThrowExceptionForNegativeX()
        {
            //Arrange via setup
            //Act
            _underTest.GetCoordinate(-1, 0);
            //Assert via Exception & method attribute
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetCoordinateShouldThrowExceptionForNegativeY()
        {
            //Arrange via setup
            //Act
            _underTest.GetCoordinate(0, -1);
            //Assert via Exception & method attribute
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetCoordinateShouldThrowExceptionForHighX()
        {
            //Arrange via setup
            //Act
            _underTest.GetCoordinate(_underTest.SizeX, 0);
            //Assert via Exception & method attribute
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetCoordinateShouldThrowExceptionForHighY()
        {
            //Arrange via setup
            //Act
            _underTest.GetCoordinate(0, _underTest.SizeY);
            //Assert via Exception & method attribute
        }

        [TestMethod]
        public void GetCoordinateShouldReturnPositionInfoForValidCoordinate()
        {
            //Arrange via setup
            //Act
            var coord =_underTest.GetCoordinate(0, 0);
            //Assert 
            Assert.IsInstanceOfType(coord, typeof(PositionInfo));
        }

        [TestMethod]
        public void GetCoordinateShouldReturnPositionInfoWithCorrectXForValidCoordinate()
        {
            //Arrange via setup
            //Act
            var coord = _underTest.GetCoordinate(2, 0);
            //Assert 
            Assert.AreEqual(coord.X, 2);
        }

        [TestMethod]
        public void GetCoordinateShouldReturnPositionInfoWithCorrectYForValidCoordinate()
        {
            //Arrange via setup
            //Act
            var coord = _underTest.GetCoordinate(0, 3);
            //Assert 
            Assert.AreEqual(coord.Y, 3);
        }
    }
}
