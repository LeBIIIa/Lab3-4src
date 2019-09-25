using System.Collections.Generic;
using System.Windows.Forms;
using Assembler;

namespace Lab3_4src
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Start();
        }
        public void Start()
        {
            var a = new ASol();
            a.Compile(new List<string> {
                @"C:\Users\denys.skrypnyk\Documents\asol.txt",
                @"C:\Users\denys.skrypnyk\Documents\ssol.txt"
            });
        }
    }
}
