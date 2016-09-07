﻿using System;
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
            Positions = new PositionInfo[SizeY, SizeX];
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
        private PositionInfo[,] Positions;

        public PositionInfo GetCoordinate(int x, int y)
        {
            return Positions[y, x];
        }

        public void FlagCoordinate()
        {
            if (!Positions[PosY, PosX].IsOpen)
            {
                if (!Positions[PosY, PosX].IsFlagged)
                    Positions[PosY, PosX].IsFlagged = true;
                else
                    Positions[PosY, PosX].IsFlagged = false;
            }
        }

        public void ClickCoordinate()
        {
            if (!Positions[PosY, PosX].IsOpen && !Positions[PosY, PosX].IsFlagged)
            {
                if (Positions[PosY, PosX].HasMine)
                {
                    for (int y = 0; y < Positions.GetLength(0); y++)
                    {
                        for (int x = 0; x < Positions.GetLength(1); x++)
                        {
                            if (Positions[y, x].HasMine)
                                Positions[y, x].IsOpen = true;
                        }
                    }
                    State = GameState.Lost;
                }
                else
                {

                }
                Positions[PosY, PosX].IsOpen = true;
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

            while (currentMines < NumberOfMines)
            {
                int randX = iSB.Next(SizeX);
                int randY = iSB.Next(SizeY);
                if (Positions[randY, randX].HasMine == false)
                {
                    Positions[randY, randX].HasMine = true;
                    currentMines++;
                }
            }

            // Calculates neighbours
            for (int y = 0; y < Positions.GetLength(0); y++)
            {
                for (int x = 0; x < Positions.GetLength(1); x++)
                {
                    // Checks if position is in 0,0 corner
                    if (Positions[y, x].Y == 0 && Positions[y, x].X == 0)
                    {
                        if (Positions[0, 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[1, 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[1, 0].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // Checks if position is in 0,(SizeX - 1) corner
                    else if (Positions[y, x].Y == 0 && Positions[y, x].X == (SizeX - 1))
                    {
                        if (Positions[0, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // Checks if position is in (SizeY - 1),0 corner
                    else if (Positions[y, x].Y == (SizeY - 1) && Positions[y, x].X == 0)
                    {
                        if (Positions[y, 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, 0].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // Checks if position is in (SizeY - 1),(SizeX - 1) corner
                    else if (Positions[y, x].Y == (SizeY - 1) && Positions[y, x].X == (SizeX - 1))
                    {
                        if (Positions[y, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // Checks if position is in row 0
                    else if (Positions[y, x].Y == 0)
                    {
                        if (Positions[0, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[1, x + 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[0, x + 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // Checks if position is in row (SizeY - 1)
                    else if (Positions[y, x].Y == (SizeY - 1))
                    {
                        if (Positions[y, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x + 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y, x + 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // Checks if position is in colum 0
                    else if (Positions[y, x].X == 0)
                    {
                        if (Positions[y - 1, 0].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y, 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y + 1, 0].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y + 1, 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // Checks if position is in colum (SizeX - 1)
                    else if (Positions[y, x].X == (SizeX - 1))
                    {
                        if (Positions[y - 1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y + 1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y + 1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }

                    // If position is not in a corner, outer row or colum
                    else
                    {
                        if (Positions[y - 1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y - 1, x + 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y, x + 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y + 1, x - 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y + 1, x].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                        if (Positions[y + 1, x + 1].HasMine)
                            Positions[y, x].NrOfNeighbours++;
                    }
                }
            }
            State = GameState.Playing;
        }

        public void DrawBoard()
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
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
