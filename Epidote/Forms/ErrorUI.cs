using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Epidote.Forms
{
    public partial class ErrorUI : Form
    {

        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }


        public ErrorUI()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }

        private void ErrorUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ErrorUI_Load(object sender, EventArgs e)
        {
            guna2GroupBox1.AutoSize = true;
            guna2GroupBox1.Text = Epidote.Utils.FileVerification.errorMessage;
            if(Epidote.Utils.FileVerification.isProblemWithLunarJrePath())
            {
                // Delete the specific directory
                Directory.Delete(Epidote.Utils.FileVerification.LunarJrePath, true);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string inviteLink = "https://discord.gg/nT73fjuDQp";
            Process.Start(inviteLink);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string websiteLink = "https://epidote.lol";
            Process.Start(websiteLink);
        }
    }
}
