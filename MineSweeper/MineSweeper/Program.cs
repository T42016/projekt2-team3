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
        static MineSweeperGame game = new MineSweeperGame(2, 2, 1, new ServiceBus());
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                game.DrawBoard();

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
