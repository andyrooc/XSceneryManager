using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using XSceneryManager.Properties;
using Microsoft.Win32;
using System.IO;
using log4net;
using System.Globalization;
using System.Collections;


namespace XSceneryManager
{
    public partial class MainForm : Form    
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainForm));

        DBAccess db = null;
        CustomSceneryAirports.CustomAirportsDataTable tbAirports = new CustomSceneryAirports.CustomAirportsDataTable();
        CustomSceneryAirports.RunwaysDataTable tbRunways = new CustomSceneryAirports.RunwaysDataTable();
        CustomSceneryAirports.ATCDataTable tbATCs = new CustomSceneryAirports.ATCDataTable();


        public MainForm()
        {
            InitializeComponent();
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

            if (!File.Exists(String.Format("{0}\\Custom Scenery\\scenery_packs.ini", Settings.Default.XPlaneFolder)))
                manageLoadOrderToolStripMenuItem.Enabled = false;

            db = new DBAccess();
            tbAirports = db.GetAirports();

            //verify and clean out redundant packages.
            ArrayList badairports = ScanScenery.VerifySceneryPacks(tbAirports);
            for (int id=0; id < badairports.Count; id++)
            {
                db.DeleteAirportRecord((int)badairports[id]);
                CustomSceneryAirports.CustomAirportsRow[] rws = (CustomSceneryAirports.CustomAirportsRow[])tbAirports.Select("id=" + badairports[id].ToString());
                for (int i=0; i < rws.Length; i++)
                    tbAirports.RemoveCustomAirportsRow(rws[i]);
            }

            dgvAirports.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            
            LoadGrid(tbAirports);
            tbRunways = db.GetRunways();
            tbATCs = db.GetATCs();

            SetProgress(0, String.Format("{0} Custom Airports", tbAirports.Count), false, 0);
            SetProgress(1, "", false, 0);

            DoFormReposition();
        }


        #region Private Methods
        private void LoadGrid(CustomSceneryAirports.CustomAirportsDataTable tb)
        {
            dgvAirports.AutoGenerateColumns = false;
            dgvAirports.DataSource = tb;
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }


        public void UpdateProgress(int type, string msg, int progess)
        {
            switch (type)
            {
                case 0:
                    if (msg.Length > 0)
                        lblCustom.Text = msg;
                    pbCustom.Value = progess;
                    break;
            }
            lblCustom.Refresh();
                
        }

        public void SetProgress(int type, string msg, bool pbVisible, int max)
        {
            switch (type)
            {
                case 0:
                    if (pbVisible)
                        pbCustom.Visible = true;
                    else
                    {
                        pbCustom.Visible = false;
                        btnCancelScan.Visible = false;
                    }
                    if (msg.Length > 0)
                        lblCustom.Text = msg;
                    pbCustom.Maximum = max;
                    break;
            }
            lblCustom.Refresh();
        }

        /// <summary>
        /// Filter out all except duplicate entries
        /// </summary>
        private string ShowDuplicates()
        {
            StringBuilder dups2 = new StringBuilder();

            CustomSceneryAirports.CustomAirportsDataTable tmp = new CustomSceneryAirports.CustomAirportsDataTable();
            SortedDictionary<string, int> icao = new SortedDictionary<string, int>();
            SortedDictionary<string, int> dups = new SortedDictionary<string, int>();

            try
            {
                foreach (CustomSceneryAirports.CustomAirportsRow rw in tbAirports)
                {
                    if (rw.enabled == 1)
                    {
                        if (icao.ContainsKey(rw.icao))
                        {
                            if (!dups.ContainsKey(rw.icao))
                            {
                                dups.Add(rw.icao.ToString(), rw.id);
                                if (dups2.Length > 0)
                                    dups2.Append(',');
                                dups2.AppendFormat("'{0}'", rw.icao.ToString());
                            }
                        }
                        else
                            icao.Add(rw.icao, rw.id);
                    }
                }
                icao.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }

            return dups2.ToString();

        }

        private void ShowMap(string url)
        {

            string icao = (string)dgvAirports.SelectedRows[0].Cells["icao"].Value;
            string nme = (string)dgvAirports.SelectedRows[0].Cells["name"].Value;

            try 
            {
                ProcessStartInfo sInfo = new ProcessStartInfo(String.Format(url, nme, icao));
                Process.Start(sInfo);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

        private void ShowICAOSite(string url)
        {
            string icao = (string)dgvAirports.SelectedRows[0].Cells["icao"].Value;

            try 
            {
                ProcessStartInfo sInfo = new ProcessStartInfo(String.Format(url, icao));
                Process.Start(sInfo);
            }
            catch (Exception ex) { log.Error(ex.Message); }

        }

        private void ShowLatLongSite(string url)
        {
            double lat = (double)dgvAirports.SelectedRows[0].Cells["latitude"].Value;
            double lng = (double)dgvAirports.SelectedRows[0].Cells["longitude"].Value;

            try
            {
                ProcessStartInfo sInfo = new ProcessStartInfo(String.Format(url, Convert.ToString(lat,CultureInfo.InvariantCulture), Convert.ToString(lng,CultureInfo.InvariantCulture)));
                Process.Start(sInfo);
            }
            catch (Exception ex) { log.Error(ex.Message); }

        }


        private string GetFileType(string ext)
        {
            string openCmd = "";

            RegistryKey regky = Registry.ClassesRoot;
            RegistryKey progID = regky.OpenSubKey(ext);
            string strID = progID.GetValue("").ToString();

            progID.Close();
            RegistryKey cmd = regky.OpenSubKey(strID + @"\shell\open\command");
            openCmd = cmd.GetValue("").ToString();
            cmd.Close();

            regky.Close();


            if (openCmd.Contains("%1"))
            {
                int idx = openCmd.IndexOf("%1");
                openCmd = openCmd.Substring(0, idx - 1).Trim();
            }

            if (!File.Exists(openCmd) && openCmd.Contains("Local"))
            {
                int idx = openCmd.IndexOf("Local") + 5;
                openCmd = String.Format("{0}{1}", Environment.GetEnvironmentVariable("PROGRAMFILES(X86)"), openCmd.Substring(idx));
            }
           // if (!File.Exists(openCmd))
           //     openCmd = "";

            return openCmd;
        }

        #endregion


        #region Events
        private void MainForm_Resize(object sender, EventArgs e)
        {
            DoFormReposition();
        }

        private void DoFormReposition()
        {
            int gridWidthOffset = 0;

            if (rtbProperties.Visible)
                gridWidthOffset = rtbProperties.Width;

            dgvAirports.Width = this.Width - (25 + gridWidthOffset);
            dgvAirports.Height = this.Height - 108;

            lblCustom.Top = this.Height - 70;
            pbCustom.Top = this.Height - 74;
            btnCancelScan.Top = this.Height - 74;

            rtbProperties.Left = this.Width - (20 + gridWidthOffset);

            btnExpand.Left = this.Width - 47;

            tbFilter.Left = btnExpand.Left - (tbFilter.Width + 5);
            lblFilter.Left = tbFilter.Left - (lblFilter.Width + 5);
        }


        private void DoSceneryScan()
        {
            bgwCustom.WorkerReportsProgress = true;
            bgwCustom.WorkerSupportsCancellation = true;
            bgwCustom.RunWorkerCompleted += bgwCustom_RunWorkerCompleted;
            bgwCustom.RunWorkerAsync(false);
            btnCancelScan.Visible = true;
            pbCustom.Visible = true;
        }

        /// <summary>
        /// Scan custom scenery & load into db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoSceneryScan();
        }

        /// <summary>
        /// Cancel the scan (if possible)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelScan_Click(object sender, EventArgs e)
        {
            if (bgwCustom.WorkerSupportsCancellation)
                bgwCustom.CancelAsync();
        }

        private void dgvAirports_MouseDown(object sender, MouseEventArgs e)
        {
            dgvAirports.ClearSelection();
            if (dgvAirports.HitTest(e.Location.X, e.Location.Y).RowIndex >= 0)
            {
                dgvAirports.Rows[dgvAirports.HitTest(e.Location.X, e.Location.Y).RowIndex].Selected = true;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (manageLoadOrderToolStripMenuItem.Enabled)
                    {
                        disableToolStripMenuItem.Text = "Manage Load Order";
                    }
                    else
                    {
                        //Set context menu item
                        if (((int) dgvAirports.SelectedRows[0].Cells["enabled"].Value) == 1)
                            disableToolStripMenuItem.Text = "Disable";
                        else
                            disableToolStripMenuItem.Text = "Enable";
                    }
                }                
            }
        }

        private void cmsRightClick_Opening(object sender, CancelEventArgs e)
        {
            if (dgvAirports.SelectedRows.Count < 1)
                e.Cancel = true;
            else
            {
                double lat = (double)dgvAirports.SelectedRows[0].Cells["latitude"].Value;
                double lng = (double)dgvAirports.SelectedRows[0].Cells["longitude"].Value;
                if (lat == 0 || lng == 0)
                    cmsRightClick.Items["skyVectorChartsToolStripMenuItem1"].Enabled = false;
                else
                    cmsRightClick.Items["skyVectorChartsToolStripMenuItem1"].Enabled = true;
            }
        }

        private void dgvAirports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if enabled, show green image
            if (e.ColumnIndex == 0)
            {
                if (((int)dgvAirports.Rows[e.RowIndex].Cells["enabled"].Value) == 1)
                    e.Value = Resources.enabled;
                else
                {
                    e.Value = Resources.disabled;
                }
            }
        }

        private void dgvAirports_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dgvAirports_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in  
                // case the user has selected multiple files. 
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    // Assign the first image to the picture variable. 
                    AddScenery frm = new AddScenery();
                    frm.AddSceneryPath(files[0]);
                    frm.ShowDialog();
                    // Set the picture location equal to the drop point. 
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    MessageBox.Show(ex.Message);
                    return;
                }
            }


        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (btnExpand.Text == "+")
            {
                btnExpand.Text = "-";
                //Expand Form
                rtbProperties.Visible = true;
                ShowProperties();
                DoFormReposition();
            }
            else
            {
                btnExpand.Text = "+";
                //Shrink Form
                rtbProperties.Visible = false;
                DoFormReposition();
            }
        }

        private void dgvAirports_SelectionChanged(object sender, EventArgs e)
        {
            ShowProperties();
        }

        #endregion


        #region Async Thread

        /// <summary>
        /// Async thread for scanning custom scenery path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwCustom_DoWork(object sender, DoWorkEventArgs e)
        {
            bool disdir = (bool)e.Argument;

            //first get enabled scenery
            bgwCustom.WorkerSupportsCancellation = true;
            ScanScenery scanner = new ScanScenery(Settings.Default.XPlaneFolder, false, bgwCustom);

            CustomSceneryAirports apts = scanner.GetReport();

            bgwCustom.WorkerSupportsCancellation = false;
            db.ClearTables();
            db.InsertScanData(apts, true);



            //now get disabled scenery
            bgwCustom.WorkerSupportsCancellation = true;
            scanner = new ScanScenery(Settings.Default.XPlaneFolder, true, bgwCustom);
            scanner.SetIDSeed(apts.CustomAirports.Rows.Count);
            apts = scanner.GetReport();
            
            bgwCustom.WorkerSupportsCancellation = false;
            db.InsertScanData(apts, false);

        }

        /// <summary>
        /// Scan completed operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwCustom_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                tbAirports = db.GetAirports();                

                LoadGrid(tbAirports);

                tbRunways = db.GetRunways();
                tbATCs = db.GetATCs();

                SetProgress(0, String.Format("{0} Custom Airports", tbAirports.Count), false, 0);
            }
        }

        /// <summary>
        /// Update the progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwCustom_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
                        if (((string)e.UserState) == "Start")
                pbCustom.Maximum = e.ProgressPercentage;
            else
            {
                pbCustom.Value = (int)e.ProgressPercentage;
                if (e.UserState != null)
                {
                    this.lblCustom.Text = (string)e.UserState;
                    this.lblCustom.Refresh();
                }
            }
        }
        #endregion


        #region Menu Events
        /// <summary>
        /// Exit Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Set the X-Plane path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setXPlaneBasePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XPlanePath xp = new XPlanePath();
            xp.ShowDialog();

            if (!File.Exists(String.Format("{0}\\Custom Scenery\\scenery_packs.ini", Settings.Default.XPlaneFolder)))
                manageLoadOrderToolStripMenuItem.Enabled = false;
        }

        private void showOnMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double lat = (double)dgvAirports.SelectedRows[0].Cells["latitude"].Value;
            double lng = (double)dgvAirports.SelectedRows[0].Cells["longitude"].Value;

            if (lat == 0 && lng == 0)
                ShowMap(Settings.Default.GoogleMapPLACE);
            else
                ShowLatLongSite(Settings.Default.GoogleMapLATLONG);
        }

        private void showOnBingMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double lat = (double)dgvAirports.SelectedRows[0].Cells["latitude"].Value;
            double lng = (double)dgvAirports.SelectedRows[0].Cells["longitude"].Value;

            if (lat == 0 && lng == 0)
                ShowMap(Settings.Default.BingMapPLACE);
            else
                ShowLatLongSite(Settings.Default.BingMapLATLONG);
        }


        private void metarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowICAOSite(Settings.Default.Metar);
        }

        private void flightStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowICAOSite(Settings.Default.FlightStats);
        }

        private void worldAeroDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowICAOSite(Settings.Default.WorldAeroData);
        }

        private void skyVectorChartsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowLatLongSite(Settings.Default.SkyVector);
        }

        private void rSSFeedsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XFeedReader frm = new XFeedReader();
            frm.ShowDialog();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox abt = new AboutBox();
            abt.ShowDialog();
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow rw = dgvAirports.SelectedRows[0];

            string disFolder = String.Format(@"{0}\{1}", Settings.Default.XPlaneFolder,
                                             Settings.Default.DisabledSceneryFolder);

            string enFolder = String.Format(@"{0}\{1}", Settings.Default.XPlaneFolder, 
                                            ScanScenery.CUSTOM_PATH);

            string targetPath = rw.Cells["path"].Value.ToString();
            targetPath = targetPath.Substring(0, targetPath.IndexOf(@"\Earth nav data"));

            string pkgDir = targetPath.Substring(targetPath.LastIndexOf(@"\") + 1);

            if (!Directory.Exists(pkgDir))
            {
                MessageBox.Show("This package no longer exists on the disk and will be removed from this view.", "Remove Airport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //package probably removed manually
                db.DeleteAirportRecord((int)rw.Cells["id"].Value);
                dgvAirports.Rows.Remove(rw);
            }
            else
            {
                if (disableToolStripMenuItem.Text == "Disable")
                {
                    string destPath = String.Format(@"{0}\{1}", disFolder, pkgDir);
                    Directory.Move(targetPath, disFolder);
                    db.DisableRow(rw.Cells["id"].Value, String.Format("{0}\\Earth nav data\\apt.dat", destPath));
                    foreach (DataRow rws in tbAirports.Select(String.Format("id={0}", rw.Cells["id"].Value)))
                    {
                        rws["enabled"] = 0;
                        rws["path"] = String.Format("{0}\\Earth nav data\\apt.dat", destPath); ;
                    }

                }
                else if (disableToolStripMenuItem.Text == "Enable")
                {
                    string destPath = String.Format(@"{0}\{1}", enFolder, pkgDir);
                    Directory.Move(targetPath, destPath);
                    db.EnableRow(rw.Cells["id"].Value, String.Format("{0}\\Earth nav data\\apt.dat", destPath));
                    foreach (DataRow rws in tbAirports.Select(String.Format("id={0}", rw.Cells["id"].Value)))
                    {
                        rws["enabled"] = 1;
                        rws["path"] = String.Format("{0}\\Earth nav data\\apt.dat", destPath);
                    }
                }
                else
                {
                    LoadOrder loForm = new LoadOrder();
                    loForm.ShowDialog();
                }
            }

        }

        private void installNewSceneryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddScenery frm = new AddScenery();
            frm.ShowDialog();
        }

        private void exportKMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KMLGen kml = new KMLGen(tbAirports);
            string f = kml.WriteKMLFile(Settings.Default.XPlaneFolder);

            string cmd = GetFileType(".kml");
            if (cmd != "")
            {
                try
                {
                    ProcessStartInfo sInfo = new ProcessStartInfo(cmd, String.Format("\"{0}\"",f));
                    Process.Start(f);
                }
                catch (Exception ex) { log.Error(ex.Message); }
            }
        }

        private void manageLoadOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOrder loForm = new LoadOrder();
            loForm.ShowDialog();
        }

        private void showDuplicatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetViewFilter("");
        }

        private void showDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetViewFilter("");
        }


        #endregion


        private void ShowProperties()
        {
            try
            {
                if (dgvAirports.SelectedRows.Count > 0 && rtbProperties.Visible)
                {
                    string[] surfaces = { "", "Asphalt", "Concrete", "Grass", "Dirt", "Gravel", "6", "7", "8", "9", "10", "11", "Dry Lake", "Water", "Snow", "Custom" };
                    rtbProperties.Clear();

                    rtbProperties.Text = String.Format(Resources.AIPMask,
                        dgvAirports.SelectedRows[0].Cells["icao"].Value,
                        dgvAirports.SelectedRows[0].Cells["elevation"].Value);

                    rtbProperties.AppendText(Environment.NewLine);

                    int start = 0;
                    int idx = 0;
                    int idx2 = 0;

                    while ((idx = rtbProperties.Find(":".ToCharArray(), start)) > 0)
                    {
                        idx++;
                        idx2 = rtbProperties.Find(Environment.NewLine.ToCharArray(), idx);
                        rtbProperties.Select(idx, idx2 - idx);
                        rtbProperties.SelectionColor = Color.LawnGreen;
                        start = idx2;
                    }
                    //Add runways

                    foreach (CustomSceneryAirports.RunwaysRow rw in tbRunways.Select(String.Format("id='{0}'", dgvAirports.SelectedRows[0].Cells["id"].Value)))
                    {
                        AppendText(rtbProperties, String.Format("{0}/{1}", rw.runway1, rw.runway2), Color.LawnGreen);
                        AppendText(rtbProperties, String.Format("Surface: {0}", surfaces[rw.surface]), Color.LawnGreen);
                        AppendText(rtbProperties, String.Format("Length: {0}m", rw.length), Color.LawnGreen);
                        AppendText(rtbProperties, String.Format("Heading: {0}°", rw.heading), Color.LawnGreen);
                        AppendText(rtbProperties, "_______________", Color.Turquoise);

                    }

                    foreach (CustomSceneryAirports.ATCRow rw in tbATCs.Select(String.Format("id='{0}'", dgvAirports.SelectedRows[0].Cells["id"].Value)))
                    {
                        string atc = "";
                        switch (rw.atctype)
                        {
                            case 50: atc = "ATIS"; break;
                            case 51: atc = "UNIC"; break;
                            case 52: atc = "CLC"; break;
                            case 53: atc = "GND"; break;
                            case 54: atc = "TWR"; break;
                            case 55: atc = "APP"; break;
                            case 56: atc = "DEP"; break;
                        }
                        AppendText(rtbProperties, String.Format("{2}:{0}\t{1}", rw.freq, rw.atcdesc, atc), Color.LawnGreen);  
                    }
                    AppendText(rtbProperties, "______End______", Color.Turquoise);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }


        public static void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.AppendText(Environment.NewLine);
        }

        private static void DetectCSChange()
        {
            string cspath = Settings.Default.XPlaneFolder;
            DirectoryInfo di = new DirectoryInfo(cspath);
            di.GetHashCode();
        }

        private void openThisFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo di = new FileInfo(dgvAirports.SelectedRows[0].Cells["path"].Value.ToString());

            Process.Start(di.DirectoryName + @"\..\..");
        }

        private void showGlobalAirportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetViewFilter("");

        }

        private void SetViewFilter(string newFilter)
        {
            DataView dv = new DataView(tbAirports);
            string filter = "";

            if (!showGlobalAirportsToolStripMenuItem.Checked)
            {
                filter = "(path not like '%Global Airports%') and ";
            }

            if (!showDisabledToolStripMenuItem.Checked && !showDuplicatesToolStripMenuItem.Checked)
            {
                filter += "(enabled=1) and ";
            }

            if (showDuplicatesToolStripMenuItem.Checked)
            {
                string dups = ShowDuplicates();
                if (dups.Length > 0)
                {
                    filter += String.Format("(enabled=1) AND (icao in ({0})) and ", dups);
                    dv.Sort = "icao";
                    showDisabledToolStripMenuItem.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No duplicates detected!", "Show Duplicates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showDuplicatesToolStripMenuItem.Checked = false;
                }
            }
            else
            {
                showDisabledToolStripMenuItem.Enabled = true;
            }

            if (newFilter.Length > 0)
            {
                filter += String.Format("(icao like '%{0}%' or name like '%{0}%')", newFilter);
            }

            if (filter.EndsWith(" and "))
                filter = filter.Substring(0, filter.Length - 5);

            dv.RowFilter = filter;
            dgvAirports.DataSource = dv;
            
            if (showDuplicatesToolStripMenuItem.Checked)
                lblCustom.Text = String.Format("{0} Duplicates detected", dv.Count / 2);
            else
                lblCustom.Text = String.Format("{0} Airports detected", dgvAirports.Rows.Count);

        }

        private void showOnOpenStreetMapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string icao = (string)dgvAirports.SelectedRows[0].Cells["icao"].Value;
            string nme = (string)dgvAirports.SelectedRows[0].Cells["name"].Value;
            double lat = (double)dgvAirports.SelectedRows[0].Cells["latitude"].Value;
            double lng = (double)dgvAirports.SelectedRows[0].Cells["longitude"].Value;

            string url = "http://www.openstreetmap.org/search?query={0}#map=15/{1}/{2}";

            try
            {
                ProcessStartInfo sInfo = new ProcessStartInfo(String.Format(url, nme, lat, lng));
                Process.Start(sInfo);
            }
            catch (Exception ex) { log.Error(ex.Message); }
        }

        private void librariesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomLib libfrm = new CustomLib();
            libfrm.ShowDialog();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

            if (tbAirports.Count == 0)
            {
                if (
                    MessageBox.Show(
                        "This looks like a new installation. Would you like to scan your custom scenery now? You can do it later via a menu option if you wish.",
                        "Initialise", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    XPlanePath xp = new XPlanePath();
                    xp.ShowDialog();

                    DoSceneryScan();

                    tbRunways = db.GetRunways();
                    LoadGrid(tbAirports);

                }
            }
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            //Update view filter
            if (tbFilter.Text.Length > 3)
            {
                SetViewFilter(tbFilter.Text);
            }
            else if (tbFilter.Text.Length == 0)
                SetViewFilter("");
        }

        private void browseCustomDSFsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSFBrowser frm = new DSFBrowser();
            frm.ShowDialog();
        }

        private void dgvAirports_KeyDown(object sender, KeyEventArgs e)
        {
            char kv = (char)e.KeyValue;
            string srch = "ICAO";

            if (dgvAirports.SortOrder != SortOrder.None && dgvAirports.SortedColumn.Name == "name")
                srch = "name";

            int startidx = dgvAirports.SelectedRows[0].Index + 1;

            DataGridViewRow dgvr;
            for (int i=startidx; i < dgvAirports.Rows.Count; i++)                
            {
                dgvr = dgvAirports.Rows[i];
                if (dgvr.Cells[srch].Value.ToString().StartsWith(kv.ToString()))
                {
                    dgvAirports.FirstDisplayedScrollingRowIndex = dgvr.Index;
                    dgvAirports.Rows[i].Selected = true;
                    break;
                }
            }
            
        }

        private void createGNS430FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KMLGen kml = new KMLGen(tbAirports);
            string pth = string.Format("{0}{1}Custom Data{1}GNS430{1}navdata{1}xsm-airports.txt", Settings.Default.XPlaneFolder, Path.DirectorySeparatorChar);
            try
            {
                string f = kml.GNS430Gen(pth, tbRunways);

                MessageBox.Show(String.Format("Wrote GNS430 data to {0}", pth));
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                MessageBox.Show(String.Format("Error writing GNS430 file. {0}", ex.Message));
            }

        }


    }
}
