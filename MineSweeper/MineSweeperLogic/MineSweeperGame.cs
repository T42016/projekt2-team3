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
        }
        private IServiceBus iSB;

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int SizeX { get; }
        public int SizeY { get; }
        public int NumberOfMines { get; }
        public GameState State { get; private set; }
        private string symbol;
        PositionInfo[,] positions;

        public PositionInfo GetCoordinate(int x, int y)
        {
            return positions[x, y];
            //return new PositionInfo();
        }

        public void FlagCoordinate()
        {
            if (!positions[PosX, PosY].IsFlagged)
                positions[PosX, PosY].IsFlagged = true;
            else
                positions[PosX, PosY].IsFlagged = false;
        }

        public void ClickCoordinate()
        {
        }

        public void ResetBoard()
        {
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
                                symbol = ".  ";
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
                                symbol = ".  ";
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
