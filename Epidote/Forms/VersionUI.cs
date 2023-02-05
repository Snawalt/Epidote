using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Epidote.Forms
{
    public partial class VersionUI : Form
    {
        public VersionUI()
        {
            InitializeComponent();
        }

        private void VersionUI_Load(object sender, EventArgs e)
        {
            guna2GroupBox1.Text = "Current: "+Epidote.Utils.VersionChecker.CurrentVersion;
            guna2GroupBox2.Text = "Available: " + Epidote.Utils.VersionChecker.LatestVersionUrl;
        }

        private void VersionUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
