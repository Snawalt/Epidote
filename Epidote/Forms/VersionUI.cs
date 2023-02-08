using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Epidote.Forms
{
    public partial class VersionUI : Form
    {
        public VersionUI()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        [DllImport("DwmApi", EntryPoint = "DwmSetWindowAttribute")]
        private static extern int SetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            // Calls the DwmSetWindowAttribute function to set the window attribute
            int result = SetWindowAttribute(Handle, 19, new[] { 1 }, 4);

            // If the result of the function call is not 0, call the function again with a different attribute value
            if (result != 0)
            {
                SetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            }
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
