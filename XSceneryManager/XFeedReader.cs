using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FeedDotNet;
using FeedDotNet.Common;
using System.Diagnostics;
using XSceneryManager.Properties;
using log4net;

namespace XSceneryManager
{
    public partial class XFeedReader : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(XFeedReader));

        ArrayList RSSItems = new ArrayList();
        private DBAccess db;


        public XFeedReader()
        {
            InitializeComponent();

        }

        private void FeedReader_Load(object sender, EventArgs e)
        {   
            db = new DBAccess();

            LoadDBList();

            pbxThrobber.Visible = true;
            bgwRSS.RunWorkerAsync();

        }

        private void LoadDBList()
        {
            RSSDataSet.RSSDataDataTable rs = db.GetRSSData();
            foreach (RSSDataSet.RSSDataRow rw in rs.Rows)
            {
                ListViewItem it = new ListViewItem();
                it.Checked = true;
                it.Text = rw.name;
                it.SubItems.Add(rw.name);
                it.SubItems.Add(rw.link);
                lvRSS.Items.Add(it);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RSSDataSet.RSSDataDataTable rssDT = new RSSDataSet.RSSDataDataTable();

            foreach (ListViewItem lvi in lvRSS.Items)
            {
                if (lvi.Checked)
                    rssDT.AddRSSDataRow(lvi.SubItems[1].Text, lvi.SubItems[2].Text, 0);
            }
            
            db.InsertRSSData(rssDT);
            db.Close();

            this.Close();
        }

        private void bgwRSS_DoWork(object sender, DoWorkEventArgs e)
        {
            string rssUrl = Settings.Default.RSS_1;

            try {

                Feed f = FeedReader.Read(rssUrl);

                RSSItems.Clear();
                foreach (FeedItem item in f.Items)
                {
                    RSSData rs = new RSSData(item.Title,item.WebUris[0].ToString(),0);
                    RSSItems.Add(rs);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        private void bgwRSS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (object item in RSSItems)
            {
                RSSData d = (RSSData) item;
                //
                if (lvRSS.FindItemWithText(d.Name) == null)
                {
                    
                    ListViewItem it = new ListViewItem();
                    it.BackColor = Color.LightBlue;
                    it.Checked = RSSAnalyse(d.Name);
                    it.Text = d.Name;
                    it.SubItems.Add(d.Name);
                    it.SubItems.Add(d.Link);
                    lvRSS.Items.Insert(0,it);
                }
            }

            pbxThrobber.Visible = false;
        }


        //try and determine airport scenery
        private bool RSSAnalyse(string txt)
        {
            bool result = false;
            char[] sep = {' ', '-', '_', '(', ')',',','.'};
            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8'};

            string[] tokens = txt.Split(sep);
            string[] comp = Settings.Default.AptMatchWords.Split(sep);

            foreach (string word in tokens)
            {
                if (word.Equals(word.ToUpper()) && word.Length == 4 && word.IndexOfAny(nums) < 0) //Potential ICAO detected
                {
                    result = true;
                }
                else
                {
                    foreach (string cword in comp)
                    {
                        if (word.Equals(cword, StringComparison.OrdinalIgnoreCase))
                        {
                            result = true;
                        }
                    }
                }
                if (result)
                    break;
            }

            return result;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            db.Close();
            this.Close();
        }

        private void lvRSS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRSS.SelectedItems.Count == 1)
            {
                try
                {
                    ProcessStartInfo sInfo = new ProcessStartInfo(lvRSS.SelectedItems[0].SubItems[2].Text);
                    Process.Start(sInfo);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
                
            }
        }


    }

    public class RSSData
    {
        public RSSData(string nm, string lk, int status)
        {
            Name = nm;
            Link = lk;
            Status = status;
        }

        public string Name { get; set; }
        public string Link { get; set; }
        public int Status { get; set; }
    }
}
