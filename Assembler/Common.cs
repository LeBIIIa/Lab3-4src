using System;
using System.Collections.Generic;

namespace Assembler
{
    internal struct Label
    {
        public int Address;
        public string label;
    }

    internal struct State
    {
        public int pc;
        public List<int> mem;
        public List<int> reg;
        public int numMemory;
    }

    internal static class Common
    {
        public const int MAXLINELENGTH = 1000;
        public const int MAXNUMLABELS = 65536;
        public const int MAXLABELLENGTH = 7;
        public const int COUNTREGISTERS = 8;
        public const int NUMMEMORY = 65536;
        public const int NUMREGS = 8;
        internal const int startMemCommand = 256;
        public static readonly Dictionary<Command, string> Commands;
        static Common()
        {
            foreach(var c in (Command[])Enum.GetValues(typeof(Command)))
            {
                if ((int)c < startMemCommand)
                {
                    Commands.Add(c, c.ToString().ToLower());
                }
                else
                {
                    Commands.Add(c, $".{c.ToString().ToLower()}");
                }
            }
        }
    }

    internal enum Command
    {
        ADD,
        NAND,
        LW,
        SW,
        BEQ,
        JALR,
        HALT,
        MUL,

        FILL = Common.startMemCommand
    }
}