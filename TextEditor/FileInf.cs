using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScintillaNET.Demo
{
    public class FileInf
    {
        public string filename { get; set; }
        public string fullPath { get; set; }
        public bool isNew { get; set; }
        public TabPage page { get; set; }
        public FileInf(string name, string path, bool isNew, TabPage page)
        {
            filename = name;
            fullPath = path;
            this.isNew = isNew;
            this.page = page;
        }
        public FileInf(string name, string path, bool isNew)
        {
            filename = name;
            fullPath = path;
            this.isNew = isNew;
        }
    }
}
