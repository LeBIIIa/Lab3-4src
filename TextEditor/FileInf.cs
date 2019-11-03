using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScintillaNET.Demo
{
    public class FileInf
    {
        public string filename { get; set; }
        public string fullPath { get; set; }
        public bool isNew { get; set; }
        public ScintillaTabbedDocument page { get; set; }
        public int ID { get; set; }
        public FileInf() { }
        public FileInf(string name, string path, bool isNew, ScintillaTabbedDocument page)
        {
            filename = name;
            fullPath = path;
            this.isNew = isNew;
            this.page = page;
        }
        public FileInf(string name, string path)
        {
            filename = name;
            fullPath = path;
            this.isNew = isNew;
        }
    }
}
