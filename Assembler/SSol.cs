using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Assembler
{
    public class SSol : ISol
    {
        private string FileName { get; set; }
        private StreamWriter Writer { get; set; }
        private Stopwatch Watch { get; set; } = new Stopwatch();

        public void Run( List<string> argv)
        {
            string line;
            State state = new State();
            if (argv.Count != 2)
                throw new MessageException($"error: usage: <machine-code file> <result-file>\n");

            /* initialize memories and registers */
            state.mem = Enumerable.Repeat(0, CommonUtil.NUMMEMORY).ToList();
            state.reg = Enumerable.Repeat(0, CommonUtil.NUMREGS).ToList();
            state.pc = 0;
            FileName = argv[1];

            Watch.Start();

            /* read machine-code file into instruction/data memory (starting at address 0) */
            using (StreamReader file = new StreamReader(argv[0]))
            {
                using (Writer = File.CreateText(argv[1]))
                {
                    for (state.numMemory = 0; !file.EndOfStream; state.numMemory++)
                    {
                        line = file.ReadLine();
                        if (state.numMemory >= CommonUtil.NUMMEMORY)
                        {
                            throw new MessageException("Exceeded memory size!");
                        }
                        if (!int.TryParse(line, out int val))
                            throw new MessageException($"error in reading address {state.numMemory}");
                        state.mem[state.numMemory] = val;
                        Writer.WriteLine("memory[{0}]={1}", state.numMemory, state.mem[state.numMemory]);
                    }
                    /* run never returns */
                    Start(state);
                }
            }
            Watch.Stop();
        }

        public string Result()
        {
            return "Done! Compiled: " + Watch.Elapsed + ".\n Output file: " + FileName;
        }


        private void Start(State state)
        {
            bool isHalt = false;
            int arg0, arg1, arg2, addressField;
            int instructions = 0;
            Command opcode;
            int maxMem = -1;    /* highest memory address touched during run */

            for (; !isHalt; instructions++)
            {
                /* infinite loop, exits when it executes halt */
                PrintState(Writer, state);

                if (state.pc < 0 || state.pc >= CommonUtil.NUMMEMORY)
                    throw new MessageException($"PC went out of the memory range {state.pc}");

                maxMem = ( state.pc > maxMem ) ? state.pc : maxMem;

                /* this is to make the following code easier to read */
                opcode = (Command)(state.mem[state.pc] >> 22);
                arg0 = ( state.mem[state.pc] >> 19 ) & 0x7;
                arg1 = ( state.mem[state.pc] >> 16 ) & 0x7;
                arg2 = state.mem[state.pc] & 0x7; /* only for add, nand */

                addressField = ConvertNum(state.mem[state.pc] & 0xFFFF); /* for beq, lw, sw */
                state.pc++;
                switch (opcode)
                {
                    case Command.ADD:
                        state.reg[arg2] = state.reg[arg0] + state.reg[arg1];
                        break;
                    case Command.NAND:
                        state.reg[arg2] = ~( state.reg[arg0] & state.reg[arg1] );
                        break;
                    case Command.LW:
                        if (state.reg[arg0] + addressField < 0 || state.reg[arg0] + addressField >= CommonUtil.NUMMEMORY)
                        {
                            throw new MessageException($"Address out of bounds({state.reg[arg0] + addressField})");
                        }
                        state.reg[arg1] = state.mem[state.reg[arg0] + addressField];
                        if (state.reg[arg0] + addressField > maxMem)
                        {
                            maxMem = state.reg[arg0] + addressField;
                        }
                        break;
                    case Command.SW:
                        if (state.reg[arg0] + addressField < 0 || state.reg[arg0] + addressField >= CommonUtil.NUMMEMORY)
                        {
                            throw new MessageException($"Address out of bounds({state.reg[arg0] + addressField})");
                        }
                        state.mem[state.reg[arg0] + addressField] = state.reg[arg1];
                        if (state.reg[arg0] + addressField > maxMem)
                        {
                            maxMem = state.reg[arg0] + addressField;
                        }
                        break;
                    case Command.BEQ:
                        if (state.reg[arg0] == state.reg[arg1])
                        {
                            state.pc += addressField;
                        }
                        break;
                    case Command.JALR:
                        state.reg[arg1] = state.pc;
                        state.pc = arg0 != 0 ? state.reg[arg0] : 0;
                        break;
                    case Command.MUL:
                        state.reg[arg2] = state.reg[arg0] * state.reg[arg1];
                        break;
                    case Command.NOOP:
                        break;
                    case Command.HALT:
                        Writer.WriteLine("machine halted\n");
                        Writer.WriteLine($"total of {instructions + 1} instructions executed\n");
                        Writer.WriteLine("final state of machine:\n");
                        PrintState(Writer, state);
                        isHalt = true;
                        break;
                    default:
                        Writer.WriteLine($"error: illegal opcode 0x{opcode:X}");
                        break;
                }
                state.reg[0] = 0;
            }
        }
        private static void PrintState(StreamWriter writer, State state)
        {
            writer.WriteLine("\n@@@\nstate:");
            writer.WriteLine($"\tpc {state.pc}");
            writer.WriteLine("\tmemory:");
            for (int i = 0; i < state.numMemory; i++)
            {
                writer.WriteLine($"\t\tmem[ {i} ] {state.mem[i]}");
            }
            writer.WriteLine("\tregisters:");
            for (int i = 0; i < CommonUtil.NUMREGS; i++)
            {
                writer.WriteLine($"\t\treg[ {i} ] {state.reg[i]}");
            }
            writer.WriteLine("end state");
        }
        private static int ConvertNum(int num)
        {
            /* convert a 16-bit number into a 32-bit Sun integer */
            int test = num & ( 1 << 15 );
            if ( test > 0 )
            {
                num -= 1 << 16;
            }
            return num;
        }
    }
}
