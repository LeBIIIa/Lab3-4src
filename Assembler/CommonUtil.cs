using System;
using System.Collections.Generic;

namespace Assembler
{
    internal class Label
    {
        public int Address;
        public string label;
    }

    internal class State
    {
        public int pc;
        public List<int> mem;
        public List<int> reg;
        public int numMemory;
    }

    public static class CommonUtil
    {
        #region public variables
        public const int MAXLINELENGTH = 1000;
        public const int MAXNUMLABELS = 65536;
        public const int MAXLABELLENGTH = 7;
        public const int COUNTREGISTERS = 8;
        public const int NUMMEMORY = 65536;
        public const int NUMREGS = 8;
        public const int startMemCommand = 256;
        public const string memSpecialCommandSymbol = ".";
        public static Dictionary<Command, string> Commands { get { if (_command == null) { Init(); } return _command; } }
        #endregion
        #region private variables
        private static Dictionary<Command, string> _command;
        #endregion
        private static void Init()
        {
            _command = new Dictionary<Command, string>();
            foreach (Command c in (Command[])Enum.GetValues(typeof(Command)))
            {
                if ((int)c < startMemCommand)
                {
                    _command.Add(c, c.ToString().ToLower());
                }
                else
                {
                    _command.Add(c, memSpecialCommandSymbol + c.ToString().ToLower());
                }
            }
        }
    }

    public enum Command
    {
        ADD,
        NAND,
        LW,
        SW,
        BEQ,
        JALR,
        HALT,
        MUL,
        NOOP,
        FILL = CommonUtil.startMemCommand
    }
}