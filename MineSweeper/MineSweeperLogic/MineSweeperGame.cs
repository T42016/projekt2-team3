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
            Positions = new PositionInfo[SizeY,SizeX];
            ResetBoard();
        }
        private IServiceBus iSB;

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }
        private string symbol;
        public PositionInfo[,] Positions;

        public PositionInfo GetCoordinate(int x, int y)
        {
            return Positions[y, x];
            //return new PositionInfo();
        }

        public void FlagCoordinate()
        {
            if (!Positions[PosY, PosX].IsFlagged)
                Positions[PosY, PosX].IsFlagged = true;
            else
                Positions[PosY, PosX].IsFlagged = false;
        }

        public void ClickCoordinate()
        {
            if (!Positions[PosY, PosX].IsOpen)
            {
                if (Positions[PosY, PosX].HasMine)
                {
                    State = GameState.Lost;
                    
                }
                else
                {
                    Positions[PosY, PosX].IsOpen = true;
                    
                }
            }
        }

        public void ResetBoard()
        {
            // Creates epmty positions
            for (int y = 0; y < Positions.GetLength(0); y++)
            {
                for (int x = 0; x < Positions.GetLength(1); x++)
                {
                    Positions[y, x] = new PositionInfo();
                    Positions[y, x].Y = y;
                    Positions[y, x].X = x;
                    Positions[y, x].HasMine = false;
                    Positions[y, x].IsFlagged = false;
                    Positions[y, x].IsOpen = false;
                    Positions[y, x].NrOfNeighbours = 0;
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
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {

                    //color = ConsoleColor.DarkCyan;

                    //if (j == PosX && i == PosY)
                    //    color = ConsoleColor.DarkCyan;
                    //else
                    //    color = ConsoleColor.Black;

                    //if (GetCoordinate(j, i).IsFlagged)
                    //    symbol = "! ";
                    //else if (GetCoordinate(j, i).IsOpen)
                    //{
                    //    if (GetCoordinate(j, i).HasMine)
                    //        symbol = "X ";
                    //    else if (GetCoordinate(j, i).NrOfNeighbours == 0)
                    //        symbol = ".  ";
                    //    else
                    //        symbol = GetCoordinate(j, i).NrOfNeighbours + " ";
                    //}
                    //else
                    //    symbol = "? ";
                    //iSB.Write(symbol,color);


                    if (j == PosX && i == PosY)
                    {
                        if (GetCoordinate(j, i).IsFlagged)
                            symbol = "! ";
                        else if (GetCoordinate(j, i).IsOpen)
                        {
                            if (GetCoordinate(j, i).HasMine)
                                symbol = "X ";
                            else if (GetCoordinate(j, i).NrOfNeighbours == 0)
                                symbol = ". ";
                            else
                                symbol = GetCoordinate(j, i).NrOfNeighbours + " ";
                        }
                        else
                            symbol = "? ";
                        iSB.Write(symbol, ConsoleColor.DarkCyan);
                    }

                    else
                    {
                        if (GetCoordinate(j, i).IsFlagged)
                            symbol = "! ";
                        else if (GetCoordinate(j, i).IsOpen)
                        {
                            if (GetCoordinate(j, i).HasMine)
                                symbol = "X ";
                            else if (GetCoordinate(j, i).NrOfNeighbours == 0)
                                symbol = ". ";
                            else
                                symbol = GetCoordinate(j, i).NrOfNeighbours + " ";
                        }
                        else
                            symbol = "? ";
                        iSB.Write(symbol);
                    }




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

    }
}
