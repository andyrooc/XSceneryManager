using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XSceneryManager.Properties;

namespace XSceneryManager
{
    public partial class DSFBrowser : Form
    {

        CustomSceneryAirports.CustomDSFDataTable tbDSFs = new CustomSceneryAirports.CustomDSFDataTable();

        public DSFBrowser()
        {
            InitializeComponent();
        }

        private void DSFBrowser_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DBAccess db = new DBAccess();
            tbDSFs = db.GetDSFs();
            //id NUM, path TEXT, dsffolder TEXT, dsffile TEXT, enabled INT
            dgvDSF.AutoGenerateColumns = false;
            dgvDSF.DataSource = tbDSFs;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DSFBrowser_Resize(object sender, EventArgs e)
        {
            DoFormReposition();
        }

        private void DoFormReposition()
        {
            dgvDSF.Width = this.Width - 25;
            dgvDSF.Height = this.Height - 90;

            btnClose.Top = this.Height - 75;
            btnClose.Left = this.Width - 145;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            KMLGen kml = new KMLGen(tbDSFs);
            string f = kml.WriteDSFKMLFile(Settings.Default.XPlaneFolder);

            if (f != "")
            {
                try
                {
                    ProcessStartInfo sInfo = new ProcessStartInfo(f);
                    Process.Start(f);
                }
                catch (Exception) {  }
            }
        }
    }
}
  

        