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
        private static Dictionary<Command, string> _command;
        public static Dictionary<Command, string> Commands { get { if (_command == null) { Init(); } return _command; } }
        private static void Init()
        {
            _command = new Dictionary<Command, string>();
            foreach (var c in (Command[])Enum.GetValues(typeof(Command)))
            {
                if ((int)c < startMemCommand)
                {
                    _command.Add(c, c.ToString().ToLower());
                }
                else
                {
                    _command.Add(c, $".{c.ToString().ToLower()}");
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