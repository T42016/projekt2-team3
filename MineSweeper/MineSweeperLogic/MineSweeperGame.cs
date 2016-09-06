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
            NumberOfMines = nrOfMines;
            Positions = new PositionInfo[SizeY,SizeX];
        }

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }
        public PositionInfo[,] Positions { get; private set; }

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
            // Creates epmty positions
            for (int y = 0; y < Positions.GetLength(0); y++)
            {
                for (int x = 0; x < Positions.GetLength(1); x++)
                {
                    Positions[y, x].Y = y;
                    Positions[y, x].X = x;
                    Positions[y, x].HasMine = false;
                    Positions[y, x].IsOpen = false;
                    Positions[y, x].IsFlagged = false;
                }
            }

            // Add mines in random positions
            int currentMines = 0;
            ServiceBus sBus = new ServiceBus();

            while (currentMines < NumberOfMines)
            {
                int randX = sBus.Next(SizeX);
                int randY = sBus.Next(SizeY);

                if (Positions[randY, randX].HasMine == false)
                {
                    Positions[randY, randX].HasMine = true;
                    currentMines++;
                }
            }
        } 
        
        public void DrawBoard()
        {
        }

        #region MoveCursor Methods

        public void MoveCursorUp()
        {
        }

        public void MoveCursorDown()
        {
        }

        public void MoveCursorLeft()
        {
        }

        public void MoveCursorRight()
        {
        }

        #endregion

    }
}
