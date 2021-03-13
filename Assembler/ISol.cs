using System.Collections.Generic;

namespace Assembler
{
    public interface ISol
    {
        void Run(List<string> argv);
        string Result();
    }
}
