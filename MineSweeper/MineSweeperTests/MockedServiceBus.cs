using System;
using MineSweeperLogic;

namespace MineSweeperTests
{
    public class MockedServiceBus : IServiceBus
    {
        public int NrOfWriteCalls { get; set; }
        public int NrOfWriteLineCalls { get; set; }
        public int NrOfNextCalls { get; set; }
        public string WriteOutput { get; set; }

        private readonly int[] randOutput = {1, 1, 2, 1, 3, 1};
        private int randIndex = 0;

        public void TestMethod1()
        {
        }

        public void Write(string text)
        {
            NrOfWriteCalls++;
            WriteOutput += text;
        }

        public void Write(string text, ConsoleColor backgroundColor)
        {
            NrOfWriteCalls++;
            WriteOutput += text;
        }

        public void WriteLine()
        {
            NrOfWriteLineCalls++;
            WriteOutput += "\n";
        }

        public int Next(int maxValue)
        {
            NrOfNextCalls++;
            return randOutput[randIndex++];
        }
    }
}
