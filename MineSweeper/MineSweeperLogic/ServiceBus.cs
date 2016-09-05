using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperLogic
{
    public class ServiceBus : IServiceBus
    {
        readonly Random rnd = new Random();

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void Write(string text, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public int Next(int maxValue)
        {
            return rnd.Next(maxValue);
        }
    }
}
