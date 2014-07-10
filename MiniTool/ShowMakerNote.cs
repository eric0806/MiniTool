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
    public partial class ShowMakerNote : Form
    {
        public ShowMakerNote(string MakerNote) {
            InitializeComponent();
            this.textBox1.Text = MakerNote;
        }

        public ShowMakerNote(Dictionary<string, string> MakerNote) {
            InitializeComponent();
            foreach (string key in MakerNote.Keys) {
                this.textBox1.Text += key + "\t\t" + MakerNote[key] + Environment.NewLine;
            }
        }

        private void ShowMakerNote_Load(object sender, EventArgs e) {

        }
    }
}
