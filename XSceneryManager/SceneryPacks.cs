using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace XSceneryManager
{
    internal class SceneryPacks
    {
        public const string ENABLED = "SCENERY_PACK";
        public const string DISABLED = "SCENERY_PACK_DISABLED";


        private int[] supportedVersion = new int[1]{1000};
        DataTable packs = new DataTable();
        private int version = 1000;
        private string fname;


        public SceneryPacks(string path)
        {
            DataColumn col1 = new DataColumn("state", typeof(string));
            DataColumn col2 = new DataColumn("path", typeof(string));
            DataColumn col3 = new DataColumn("enabled", typeof(bool));

            packs.Columns.Add(col1);
            packs.Columns.Add(col2);
            packs.Columns.Add(col3);

            version = ScanINI(path);
        }

        private int ScanINI(string path)
        {
            fname = String.Format("{0}\\Custom Scenery\\scenery_packs.ini", path);
            string line;
            char[] s = {' '};

            if (!File.Exists(fname))
                return -1;

            try
            {
                StreamReader sr = File.OpenText(fname);

                int cnt = 0;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (String.IsNullOrEmpty(line) || String.IsNullOrWhiteSpace(line))
                        continue;

                    cnt++;

                    switch (cnt)
                    {
                        case 1: // I
                            break;

                        case 2: //version
                            Int32.TryParse(line.Split(s, 2)[0].Trim(), out version);
                            break;

                        default:
                            if (line.Split(s,2).Count() == 2)
                            {
                                DataRow rw = packs.NewRow();
                                rw["state"] = line.Split(s, 2)[0];
                                rw["path"] = line.Split(s, 2)[1];
                                rw["enabled"] = rw["state"].ToString() == ENABLED ? true : false;
                                packs.Rows.Add(rw);
                            }
                            break;
                    }
                }

                sr.Close();
            }
            catch (IOException ex)
            {
            }

            return version;
        }

        public DataTable GetSceneryPacks()
        {
            return packs;
        }

        public bool SavePacks(DataTable newPacks)
        {
            bool success = false;
            
            try
            {
                FileStream sr = File.OpenWrite(fname);
                
                sr.Write(System.Text.Encoding.ASCII.GetBytes("I\r\n"),0,3);
                sr.Write(System.Text.Encoding.ASCII.GetBytes(String.Format("{0} version\r\n",version)), 0, 14);
                sr.Write(System.Text.Encoding.ASCII.GetBytes(String.Format("\r\n", version)), 0, 2);
                sr.Write(System.Text.Encoding.ASCII.GetBytes("SCENERY\r\n"), 0, 9);
;
                foreach (DataRow dr in packs.Rows)
                {
                    string line = String.Format("{0} {1}\r\n", dr["state"], dr["path"]);
                    sr.Write(System.Text.Encoding.ASCII.GetBytes(line),0,line.Length);
                }

                sr.Close();
                success = true;
            }
            catch (IOException ex)
            {
            }
            
            return success;
        }

    }
}
