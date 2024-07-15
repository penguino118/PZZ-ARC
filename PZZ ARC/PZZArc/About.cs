using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PZZ_ARC.PZZArc
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void pzzcomplink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = pzzcomplink.Text,
                UseShellExecute = true
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
