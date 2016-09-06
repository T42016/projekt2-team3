using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperLogic
{
    public class MineSweeperGame
    {

        public MineSweeperGame(int sizeX, int sizeY, int nrOfMines, IServiceBus bus)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            NumberOfMines = NumberOfMines;
        }

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }

        public PositionInfo GetCoordinate(int x, int y)
        {
            return null;
        }

        public void FlagCoordinate()
        {
        }

        public void ClickCoordinate()
        {
        }

        public void ResetBoard()
        {
        }

        public void DrawBoard()
        {
        }

        #region MoveCursor Methods

        public void MoveCursorUp()
        {
            if (PosY > 0)
                PosY--;
        }


        public void MoveCursorDown()
        {
            if (PosY < SizeY - 1)
                PosY++;
        }

       

        public void MoveCursorLeft()
        {
            if (PosX > 0)
                PosX--;

        }

  
        public void MoveCursorRight()
        {
            if (PosX  < SizeX-1)
                PosX++;

        }






        #endregion

    }
}
