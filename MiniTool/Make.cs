using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniTool
{
    public partial class Make : Form
    {
        public Make() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            string[] Line = SourceText.Text.Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Line.Length; i++) {
                string[] val = Line[i].Split('=');
                TargetText.Text += "case " + val[0].Trim() + ": ret = \"" + val[1].Trim() + "\"; break;" + Environment.NewLine;
            }
        }
    }
}
