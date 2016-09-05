using System;

namespace MineSweeperLogic
{
    public interface IServiceBus
    {
        void Write(string text);
        void Write(string text, ConsoleColor backgroundColor);
        void WriteLine();
        int Next(int maxValue);
    }
}