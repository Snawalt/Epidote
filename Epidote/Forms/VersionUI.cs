using System;
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
            currentVersionBox.Text = "Current: "+Epidote.Utils.VersionChecker.CurrentVersion;
            latestVersionBox.Text = "Available: " + Epidote.Utils.VersionChecker.LatestVersion;
        }

        private void VersionUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
