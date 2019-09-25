using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assembler
{
    class SSol
    {
        public void Run( List<string> argv)
        {
            string line;
            State state;
            StreamReader file = null;

            if (argv.Count != 2)
            {
                throw new MessageException($"error: usage: {argv[0]} <machine-code file>\n");
            }

            /* initialize memories and registers */
            state.mem = Enumerable.Repeat(0, Common.NUMMEMORY).ToList();
            state.reg = Enumerable.Repeat(0, Common.NUMREGS).ToList();
            state.pc = 0;

            /* read machine-code file into instruction/data memory (starting at
            address 0) */
            try
            {
                file = new StreamReader(argv[1]);
                for (state.numMemory = 0; !file.EndOfStream;state.numMemory++)
                {
                    line = file.ReadLine();
                    if (state.numMemory >= Common.NUMMEMORY)
                    {
                        throw new MessageException("Exceeded memory size!");
                    }
                    if ( state.mem.Count <= state.numMemory)
                    {
                        throw new MessageException($"error in reading address {state.numMemory}");
                    }
                    Console.WriteLine("memory[{0}]={1}\n", state.numMemory, state.mem[state.numMemory]);
                }
                /* run never returns */
                Start(state);
            }
            finally
            {
                file?.Close();
            }
        }

        private void Start( State state )
        {
            var isHalt = false;
            int arg0, arg1, arg2, addressField;
            var instructions = 0;
            Command opcode;
            var maxMem = -1;    /* highest memory address touched during run */

            for (; !isHalt; instructions++)
            {
                /* infinite loop, exits when it executes halt */
                PrintState(state);

                if (state.pc < 0 || state.pc >= Common.NUMMEMORY)
                {
                    throw new MessageException($"PC went out of the memory range {state.pc}");
                }

                maxMem = ( state.pc > maxMem ) ? state.pc : maxMem;

                /* this is to make the following code easier to read */
                opcode = (Command)(state.mem[state.pc] >> 22);
                arg0 = ( state.mem[state.pc] >> 19 ) & 0x7;
                arg1 = ( state.mem[state.pc] >> 16 ) & 0x7;
                arg2 = state.mem[state.pc] & 0x7; /* only for add, nand */

                addressField = ConvertNum(state.mem[state.pc] & 0xFFFF); /* for beq,
								    lw, sw */
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
                        if (state.reg[arg0] + addressField < 0 || state.reg[arg0] + addressField >= Common.NUMMEMORY)
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
                        if (state.reg[arg0] + addressField < 0 || state.reg[arg0] + addressField >= Common.NUMMEMORY)
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
                    case Command.HALT:
                        Console.WriteLine("machine halted\n");
                        Console.WriteLine($"total of {instructions + 1} instructions executed\n");
                        Console.WriteLine("final state of machine:\n");
                        PrintState(state);
                        isHalt = true;
                        break;
                    default:
                        Console.WriteLine($"error: illegal opcode 0x{opcode:X}");
                        break;
                }
                state.reg[0] = 0;
            }
        }

        private static void PrintState( State state)
        {
            Console.WriteLine("\n@@@\nstate:");
            Console.WriteLine($"\tpc {state.pc}");
            Console.WriteLine("\tmemory:");
            for (var i = 0; i < state.numMemory; i++)
            {
                Console.WriteLine($"\t\tmem[ {i} ] {state.mem[i]}");
            }
            Console.WriteLine("\tregisters:");
            for (var i = 0; i < Common.NUMREGS; i++)
            {
                Console.WriteLine("\t\treg[ i ] state.reg[i]");
            }
            Console.WriteLine("end state");
        }

        private static int ConvertNum( int num )
        {
            /* convert a 16-bit number into a 32-bit Sun integer */
            var test = num & ( 1 << 15 );
            if ( test > 0 )
            {
                num -= 1 << 16;
            }
            return num;
        }

    }
}
