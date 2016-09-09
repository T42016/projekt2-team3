using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeperLogic;

namespace MineSweeper
{
    class Program
    {
        static MineSweeperGame game = new MineSweeperGame(10, 10, 10, new ServiceBus());
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                game.DrawBoard();

                // Writes out number of flagged, mines & opened positions
                Console.SetCursorPosition(game.SizeX * 2 + 5, 0);
                Console.Write("Flagged:");
                Console.SetCursorPosition(game.SizeX * 2 + 15, 0);
                Console.Write(game.nrOfFlagged);
                Console.SetCursorPosition(game.SizeX * 2 + 5, 1);
                Console.Write("Mines:");
                Console.SetCursorPosition(game.SizeX * 2 + 15, 1);
                Console.Write(game.NumberOfMines);
                Console.SetCursorPosition(game.SizeX * 2 + 5, 2);
                Console.Write("Opened:");
                Console.SetCursorPosition(game.SizeX * 2 + 15, 2);
                Console.Write(game.nrOfOpened + "/" + ((game.SizeX * game.SizeY) - game.NumberOfMines));

                // Sets cursor position back to normal
                Console.SetCursorPosition(0,game.SizeY);

                if (game.State == GameState.Won)
                {
                    Console.WriteLine("You won!");
                    Console.ReadLine();
                    game.ResetBoard();
                    continue;
                }
                else if (game.State == GameState.Lost)
                {
                    Console.WriteLine("You lost!");
                    Console.ReadLine();
                    game.ResetBoard();
                    continue;
                }
                
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.LeftArrow)
                    game.MoveCursorLeft();
                if (key.Key == ConsoleKey.RightArrow)
                    game.MoveCursorRight();
                if (key.Key == ConsoleKey.UpArrow)
                    game.MoveCursorUp();
                if (key.Key == ConsoleKey.DownArrow)
                    game.MoveCursorDown();
                if(key.Key == ConsoleKey.Spacebar)
                    game.ClickCoordinate();
                if (key.Key == ConsoleKey.Enter)
                    game.FlagCoordinate();
            }
        }
    }
}
