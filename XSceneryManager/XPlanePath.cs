using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using XSceneryManager.Properties;

namespace XSceneryManager
{
    public partial class XPlanePath : Form
    {
        public XPlanePath()
        {
            InitializeComponent();

            tbXPPath.Text = Settings.Default.XPlaneFolder;
        }

        private void btnFolderBrowse_Click(object sender, EventArgs e)
        {
            fbdXPlane.SelectedPath = tbXPPath.Text;
            if (fbdXPlane.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbXPPath.Text = fbdXPlane.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.XPlaneFolder = tbXPPath.Text;
            Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
