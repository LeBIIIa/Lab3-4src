using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    public class ASSol : Sol
    {
        public void Run(List<string> argv)
        {
            var sub = new List<string>() { argv[0], argv[1] };
            var sub2 = new List<string>() { argv[0], argv[2] };
            Sol sol = new ASol();
            sol.Run(sub);
            sol = new SSol();
            sol.Run(sub2);
        }
    }
}
