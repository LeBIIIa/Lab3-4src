using System.Collections.Generic;
using System.Diagnostics;

namespace Assembler
{
    public class ASSol : ISol
    {
        private string ResultMessage { get; set; }
        public void Run(List<string> argv)
        {
            List<string> sub = new List<string>() { argv[0], argv[1] };
            List<string> sub2 = new List<string>() { argv[1], argv[2] };
            ISol sol = new ASol();
            sol.Run(sub);
            ResultMessage = sol.Result();
            sol = new SSol();
            sol.Run(sub2);
            ResultMessage += "\n" + sol.Result();
        }

        public string Result()
        {
            return ResultMessage;
        }
    }
}
