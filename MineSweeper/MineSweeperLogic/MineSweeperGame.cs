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
            iSB = bus;
            Positions = new PositionInfo[SizeX, SizeY];
            ResetBoard();
        }

        #region variables
        
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }

        private IServiceBus iSB;
        private PositionInfo[,] Positions;
        private string symbol;
        private int nrOfOpen;
        private int nrOfFlag;

        #endregion

        public PositionInfo GetCoordinate(int x, int y)
        {
            return Positions[x, y];
        }

        public void FlagCoordinate()
        {
            if (!Positions[PosX, PosY].IsOpen)
            {
                if (!Positions[PosX, PosY].IsFlagged)
                    Positions[PosX, PosY].IsFlagged = true;
                else
                    Positions[PosX, PosY].IsFlagged = false;
            }
        }

        public void ClickCoordinate()
        {
            if (!Positions[PosX, PosY].IsOpen && !Positions[PosX, PosY].IsFlagged)
            {
                // Checks for loss
                if (Positions[PosX, PosY].HasMine)
                {
                    // Reveals rest of mines
                    for (int y = 0; y < Positions.GetLength(1); y++)
                    {
                        for (int x = 0; x < Positions.GetLength(0); x++)
                        {
                            if (Positions[x, y].HasMine)
                                Positions[x, y].IsOpen = true;
                        }
                    }
                    State = GameState.Lost;
                }

                // Should not trigger floodfill if position has neighbours
                else if (Positions[PosX, PosY].NrOfNeighbours != 0)
                {
                    Positions[PosX, PosY].IsOpen = true;

                    // Checks for win
                    nrOfOpen = 0;
                    for (int y = 0; y < Positions.GetLength(1); y++)
                    {
                        for (int x = 0; x < Positions.GetLength(0); x++)
                        {
                            if (Positions[x, y].IsOpen && !Positions[x, y].HasMine)
                            {
                                nrOfOpen++;
                            }
                        }
                    }
                    if (nrOfOpen == ((SizeX * SizeY) - NumberOfMines))
                        State = GameState.Won;
                }

                // Triggers floodfill if position has no neighbours
                else
                {
                FloodFill(PosX, PosY);

                // Checks for win
                nrOfOpen = 0;
                for (int y = 0; y < Positions.GetLength(1); y++)
                {
                    for (int x = 0; x < Positions.GetLength(0); x++)
                    {
                        if (Positions[x,y].IsOpen && !Positions[x,y].HasMine)
                        {
                            nrOfOpen++;
                        }
                    }
                }
                if (nrOfOpen == ((SizeX*SizeY) - NumberOfMines))
                    State = GameState.Won;
                }
            }
        }

        public void ResetBoard()
        {
            // Creates epmty positions
            for (int y = 0; y < Positions.GetLength(1); y++)
            {
                for (int x = 0; x < Positions.GetLength(0); x++)
                {
                    Positions[x, y] = new PositionInfo();
                    Positions[x, y].X = x;
                    Positions[x, y].Y = y;
                    Positions[x, y].HasMine = false;
                    Positions[x, y].IsFlagged = false;
                    Positions[x, y].IsOpen = false;
                    Positions[x, y].NrOfNeighbours = 0;
                }
            }

            // Add mines in random positions
            int currentMines = 0;
            while (currentMines < NumberOfMines && currentMines < SizeX*SizeY)
            {
                int randX = iSB.Next(SizeX);
                int randY = iSB.Next(SizeY);
                if (Positions[randX, randY].HasMine == false)
                {
                    Positions[randX, randY].HasMine = true;
                    currentMines++;
                }
            }
            
            // Calculates neighbours
            for (int y = 0; y < Positions.GetLength(1); y++)
            {
                for (int x = 0; x < Positions.GetLength(0); x++)
                {
                    // Checks if position is in x = 0, y = 0 corner
                    if (Positions[x, y].Y == 0 && Positions[x, y].X == 0)
                    {
                        if (Positions[x + 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // Checks if position is in x = SizeX-1, y = 0 corner
                    else if (Positions[x, y].Y == 0 && Positions[x, y].X == (SizeX - 1))
                    {
                        if (Positions[x - 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // Checks if position is in x = 0, y = SizeY-1 corner
                    else if (Positions[x, y].Y == (SizeY - 1) && Positions[x, y].X == 0)
                    {
                        if (Positions[x, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // Checks if position is in x = SizeX-1, y = SizeY-1 corner
                    else if (Positions[x, y].Y == (SizeY - 1) && Positions[x, y].X == (SizeX - 1))
                    {
                        if (Positions[x - 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // Checks if position is in y = 0
                    else if (Positions[x, y].Y == 0)
                    {
                        if (Positions[x - 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // Checks if position is in y = SizeY-1
                    else if (Positions[x, y].Y == (SizeY - 1))
                    {
                        if (Positions[x - 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // Checks if position is in x = 0
                    else if (Positions[x, y].X == 0)
                    {
                        if (Positions[x, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // Checks if position is in x = SizeX-1
                    else if (Positions[x, y].X == (SizeX - 1))
                    {
                        if (Positions[x - 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }

                    // If position is not in a corner or outer row or colum
                    else
                    {
                        if (Positions[x - 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y - 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x - 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                        if (Positions[x + 1, y + 1].HasMine)
                            Positions[x, y].NrOfNeighbours++;
                    }
                }
            }
            State = GameState.Playing;
            nrOfOpen = 0;
            nrOfFlag = 0;
        }

        public void DrawBoard()
        {
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    if (x == PosX && y == PosY)
                    {
                        if (GetCoordinate(x, y).IsFlagged)
                            symbol = "! ";
                        else if (GetCoordinate(x, y).IsOpen)
                        {
                            if (GetCoordinate(x, y).HasMine)
                                symbol = "X ";
                            else if (GetCoordinate(x, y).NrOfNeighbours == 0)
                                symbol = ". ";
                            else
                                symbol = GetCoordinate(x, y).NrOfNeighbours + " ";
                        }
                        else
                            symbol = "? ";
                        iSB.Write(symbol, ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        if (GetCoordinate(x, y).IsFlagged)
                            symbol = "! ";
                        else if (GetCoordinate(x, y).IsOpen)
                        {
                            if (GetCoordinate(x, y).HasMine)
                                symbol = "X ";
                            else if (GetCoordinate(x, y).NrOfNeighbours == 0)
                                symbol = ". ";
                            else
                                symbol = GetCoordinate(x, y).NrOfNeighbours + " ";
                        }
                        else
                            symbol = "? ";
                        iSB.Write(symbol);
                    }
                }
                if (y == 0)
                {
                    nrOfFlag = 0;
                    for (int i = 0; i < SizeY; i++)
                    {
                        for (int j = 0; j < SizeX; j++)
                        {
                            if (Positions[j,i].IsFlagged)
                            {
                                nrOfFlag++;
                            }
                        }
                    }
                    iSB.Write("   Flagged:   " + nrOfFlag);
                }
                if (y == 1)
                {
                    iSB.Write("   Mines:     " + NumberOfMines);
                }
                if (y == 2)
                {
                     iSB.Write("   Opened:    " + nrOfOpen + "/" + (SizeX*SizeY - NumberOfMines));
                }
                iSB.WriteLine();
            }
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
            if (PosX < SizeX - 1)
                PosX++;
        }

        #endregion

        private void FloodFill(int x, int y)
        {
            //perform bounds checking X
            if ((x > Positions.GetLength(0) -1)|| (x < 0))
                return; //outside of bounds

            //perform bounds checking Y
            if ((y > Positions.GetLength(1) -1) || (y < 0))
                return; //ouside of bounds

            //check to see if the node is the target color
            if (Positions[x,y].IsOpen || Positions[x, y].HasMine || Positions[x, y].IsFlagged)
                return; //return and do nothing
            else if (Positions[x, y].NrOfNeighbours == 0)
            {
                Positions[x, y].IsOpen = true;

                //recurse
                //try to fill one step to the right
                FloodFill(x + 1, y);
                //try to fill one step to the left
                FloodFill(x - 1, y);
                //try to fill one step to the north
                FloodFill(x , y - 1);
                //try to fill one step to the south
                FloodFill(x, y + 1);

                //exit method
                return;
            }
            else
            {
                Positions[x, y].IsOpen = true;
                return;
            }
        }
    }
}
