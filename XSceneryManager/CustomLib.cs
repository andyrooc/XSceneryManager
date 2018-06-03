using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XSceneryManager
{
    public partial class CustomLib : Form
    {
        public CustomLib()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DBAccess db = new DBAccess();
            CustomSceneryAirports.CustomLibsDataTable tbLibraries = db.GetLibraries();

            dgvCustomLibs.AutoGenerateColumns = false;
            dgvCustomLibs.DataSource = tbLibraries;            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
