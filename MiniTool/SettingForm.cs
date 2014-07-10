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
    public partial class SettingForm : Form
    {

        public SettingForm() {
            InitializeComponent();
            linkLabel1.Links.Add(0, linkLabel1.Text.Length, "http://www.microsoft.com/en-us/download/details.aspx?id=26829");
        }

        private void SettingForm_Load(object sender, EventArgs e) {
            if (Config.GetConfig("ReadRAW") == "1") {
                chkLoadRAW.Checked = true;
            }
            else {
                chkLoadRAW.Checked = false;
            }

            int Zoom = int.Parse(Config.GetConfig("Zoom") == string.Empty ? "15" : Config.GetConfig("Zoom"));
            if (Zoom > 21 || Zoom < 0) Zoom = 15;
            MapZoomBar.Value = Zoom;
            //lblBarValue.Text = MapZoomBar.Value.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (chkLoadRAW.Checked) {
                Config.SetConfig("ReadRAW", "1");
            }
            else {
                Config.SetConfig("ReadRAW", "0");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (e.Link.LinkData != null) {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
        }

        private void MapZoomBar_ValueChanged(object sender, EventArgs e) {
            lblBarValue.Text = MapZoomBar.Value.ToString();
            Config.SetConfig("Zoom", MapZoomBar.Value.ToString());
        }
    }
}
