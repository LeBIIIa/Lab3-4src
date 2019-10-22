using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Text;

namespace Assembler
{
    public class ASol : Sol
    {
        public void Run(List<string> argv)
        {
            string inFileString, outFileString;
            int address;
            string label, opcode, arg0, arg1, arg2;
            int numLabels = 0;
            int num;
            int addressField;
            var labelArray = new List<Label>(CommonUtil.MAXNUMLABELS);

            inFileString = argv[0];
            outFileString = argv[1];

            StreamReader inFile = null;
            StreamWriter outFile = null;

            try
            {
                inFile = new StreamReader(inFileString);
                outFile = new StreamWriter(outFileString);
                /* map symbols to addresses */

                /* assume address start at 0 */
                for (address = 0; ReadAndParse(inFile, out label, out opcode, out arg0, out arg1, out arg2); address++)
                {

                    /* check for illegal opcode */
                    if ( !CommonUtil.Commands.Any(c => opcode == c.Value))
                    {
                        throw new MessageException($"Unrecognized opcode {opcode} at address {address}");
                    }

                    /* check register fields */
                    if (opcode == (CommonUtil.Commands[Command.ADD]) || opcode == (CommonUtil.Commands[Command.NAND]) ||
                        opcode == (CommonUtil.Commands[Command.LW]) || opcode == (CommonUtil.Commands[Command.SW]) ||
                        opcode == (CommonUtil.Commands[Command.BEQ]) || opcode == (CommonUtil.Commands[Command.JALR]) ||
                        opcode == (CommonUtil.Commands[Command.MUL]))
                    {
                        TestRegArg(arg0);
                        TestRegArg(arg1);
                    }
                    if (opcode == (CommonUtil.Commands[Command.ADD]) || opcode == (CommonUtil.Commands[Command.NAND]) ||
                        opcode == (CommonUtil.Commands[Command.MUL]))
                    {
                        TestRegArg(arg2);
                    }

                    /* check addressField */
                    if (opcode == (CommonUtil.Commands[Command.LW]) || opcode == (CommonUtil.Commands[Command.SW]) ||
                        opcode == (CommonUtil.Commands[Command.BEQ]))
                    {
                        TestAddrArg(arg2);
                    }
                    if (opcode == (CommonUtil.Commands[Command.FILL]))
                    {
                        TestAddrArg(arg0);
                    }

                    /* check for enough arguments */
                    if (( opcode != (CommonUtil.Commands[Command.HALT]) && opcode != (CommonUtil.Commands[Command.FILL]) &&
                          opcode != (CommonUtil.Commands[Command.JALR]) && string.IsNullOrEmpty(arg2) ) ||
                         ( opcode == (CommonUtil.Commands[Command.JALR]) && string.IsNullOrEmpty(arg1) ) ||
                         ( opcode == (CommonUtil.Commands[Command.FILL]) && string.IsNullOrEmpty(arg0) ))
                    {
                        throw new MessageException($"Error at address {address}: not enough arguments");
                    }

                    if ( !string.IsNullOrEmpty(label) )
                    {
                        /* check for labels that are too long */
                        if (label.Length >= CommonUtil.MAXLABELLENGTH)
                        {
                            throw new MessageException($"Label {label} is too long!(max length: {CommonUtil.MAXLABELLENGTH})");
                        }

                        /* make sure label starts with letter */
                        if (!Regex.IsMatch(label, "[a-zA-Z]"))
                        {
                            throw new MessageException($"Label {label} doesn't start with letter!");
                        }

                        /* make sure label consists of only letters and numbers */
                        if (!Regex.IsMatch(label, "[a-zA-Z0-9]"))
                        {
                            throw new MessageException($"Label {label} has character other that letters and numbers!");
                        }

                        /* look for duplicate label */
                        if (labelArray.Exists(l => l.label == (label)))
                        {
                            var index = labelArray.First(l => l.label == (label)).Address;
                            throw new MessageException($"Error: duplicate label {label} at address {index}");
                        }
                    }
                    /* see if there are too many labels */
                    if (numLabels >= CommonUtil.MAXNUMLABELS)
                    {
                        throw new MessageException($"Error: too many labels (label = {label})");
                    }
                    labelArray.Add(new Label { label = label, Address = address });
                }

                /* now do second pass (print machine code, with symbols filled in as
                addresses) */
                inFile.DiscardBufferedData();
                inFile.BaseStream.Seek(0, SeekOrigin.Begin);
                for (address = 0; ReadAndParse(inFile, out label, out opcode, out arg0, out arg1, out arg2); address++)
                {
                    if (opcode == CommonUtil.Commands[Command.ADD])
                    {
                        num = ((int)Command.ADD << 22) | (int.Parse(arg0) << 19) | (int.Parse(arg1) << 16)
                            | int.Parse(arg2);
                    }
                    else if (opcode == (CommonUtil.Commands[Command.NAND]))
                    {
                        num = ((int)Command.NAND << 22) | (int.Parse(arg0) << 19) | (int.Parse(arg1) << 16)
                            | int.Parse(arg2);
                    }
                    else if (opcode == (CommonUtil.Commands[Command.JALR]))
                    {
                        num = ((int)Command.JALR << 22) | (int.Parse(arg0) << 19) | (int.Parse(arg1) << 16);
                    }
                    else if (opcode == (CommonUtil.Commands[Command.HALT]))
                    {
                        num = ((int)Command.HALT << 22);
                    }
                    else if (opcode == (CommonUtil.Commands[Command.MUL]))
                    {
                        num = ((int)Command.MUL << 22) | (int.Parse(arg0) << 19) | (int.Parse(arg1) << 16)
                            | int.Parse(arg2);
                    }
                    else if (opcode == (CommonUtil.Commands[Command.LW]) || 
                        opcode == (CommonUtil.Commands[Command.SW]) || 
                        opcode == (CommonUtil.Commands[Command.BEQ]))
                    {
                        /* if arg2 is symbolic, then translate into an address */
                        if (!IsNumber(arg2))
                        {
                            addressField = TranslateSymbol(labelArray, arg2);
                            if (opcode == (CommonUtil.Commands[Command.BEQ]))
                            {
                                addressField = addressField - address - 1;
                            }
                        }
                        else
                        {
                            addressField = int.Parse(arg2);
                        }
                        if (addressField < -32768 || addressField > 32767)
                        {
                            throw new MessageException($"Error: offset {addressField} out of range");
                        }

                        if (opcode == (CommonUtil.Commands[Command.BEQ]))
                        {
                            num = ((int)Command.BEQ << 22) | (int.Parse(arg0) << 19) | (int.Parse(arg1) << 16)
                            | addressField;
                        }
                        else
                        {
                            /* lw or sw */
                            if (opcode == (CommonUtil.Commands[Command.LW]))
                            {
                                num = ((int)Command.LW << 22) | (int.Parse(arg0) << 19) |
                                                        (int.Parse(arg1) << 16) | addressField;
                            }
                            else
                            {
                                num = ((int)Command.SW << 22) | (int.Parse(arg0) << 19) |
                                                        (int.Parse(arg1) << 16) | addressField;
                            }
                        }
                    }
                    else if (opcode == (CommonUtil.Commands[Command.FILL]))
                    {
                        if (int.TryParse(arg0, out num))
                        {
                            num = int.Parse(arg0);
                        }
                        else
                        {
                            num = TranslateSymbol(labelArray, arg0);
                        }
                    }
                }
            }
            finally
            {
                inFile?.Close();
                outFile?.Close();
            }
        }

        /*
         * Read and parse a line of the assembly-language file.  Fields are returned
         * in label, opcode, arg0, arg1, arg2 (these strings must have memory already
         * allocated to them).
         *
         * Return values:
         *     0 if reached end of file
         *     1 if all went well
         *
         * exit(1) if line is too long.
         */
        private static bool ReadAndParse( StreamReader inFile, out string label, out string opcode, out string arg0, out string arg1, out string arg2 )
        {
            label = opcode = arg0 = arg1 = arg2 = string.Empty;
            /* delete prior values */

            /* read the line from the assembly-language file */
            if (inFile.EndOfStream)
            {
                /* reached end of file */
                return false;
            }
            var line = inFile.ReadLine();
            /* check for line too long */
            if (line != null && line.Length == CommonUtil.MAXLINELENGTH - 1)
            {
                throw new MessageException("Error: line too long");
            }

            var index = 0;
            if (line == null) return false;
            var split = line.Split(new[] {'\t', '\n', '\r', ' '}, StringSplitOptions.RemoveEmptyEntries);
            if (!line.StartsWith(" "))
            {
                label = split[index++];
            }
            opcode = split.ElementAtOrDefault(index++) ?? "";
            arg0 = split.ElementAtOrDefault(index++) ?? "";
            arg1 = split.ElementAtOrDefault(index++) ?? "";
            arg2 = split.ElementAtOrDefault(index) ?? "";
            return true;
        }

        private static int TranslateSymbol( IEnumerable<Label> labelArray, string symbol )
        {
            /* search through address label table */
            var label = labelArray.FirstOrDefault(l => l.label == (symbol));

            if (label.Equals(default(Label)))
            {
                throw new MessageException($"Error: missing label {symbol}");
            }

            return label.Address;
        }

        private bool IsNumber( string str)
        {
            /* return 1 if string is a number */
            return int.TryParse(str, out _);
        }
        /*
         * Test register argument; make sure it's in range and has no bad characters.
         */
        private void TestRegArg( string arg )
        {
            if (!IsNumber(arg)) return;
            if (!Regex.IsMatch(arg, "[0-9]"))
            {
                throw new MessageException("Bad character in register argument!");
            }

            var num = int.Parse(arg);

            if (num < 0 || num > CommonUtil.COUNTREGISTERS)
            {
                throw new MessageException($"Error: register out of range ({num})");
            }
        }
        /*
         * Test addressField argument.
         */
        private void TestAddrArg( string arg )
        {
            /* test numeric addressField */
            if (IsNumber(arg) && !Regex.IsMatch(arg, "[0-9]"))
            {
                throw new MessageException("Bad character in addressField!");
            }
        }

    }
}
