using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Threading;
using System.IO;
using TextFileParsers;
using System.Data;
using log4net;

namespace XSceneryManager
{
    class ScanScenery
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ScanScenery));

        public const string APT_PATH = @"Earth nav data\apt.dat";
        public const string CUSTOM_PATH = @"Custom Scenery";
        public const string DISABLED_PATH = @"Custom Scenery (disabled)";

        private string basePath = "";
        private bool enabled = true;
        private int idseed = 0;

        private System.ComponentModel.BackgroundWorker thd = null;

        enum PortType
        {
            Airport = 1,
            Seaport = 16,
            Helipad = 17
        }

        public ScanScenery(string path, bool disabledPath, System.ComponentModel.BackgroundWorker thd)
        {
            this.thd = thd;
            this.enabled = !disabledPath;

            if (disabledPath)
            {
                basePath = String.Format(@"{0}\{1}", path, DISABLED_PATH);
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);
            } 
            else
                basePath = String.Format(@"{0}\{1}", path, CUSTOM_PATH);
        }

        public static ArrayList VerifySceneryPacks(CustomSceneryAirports.CustomAirportsDataTable recs) {
            ArrayList arr = new ArrayList();

            foreach (CustomSceneryAirports.CustomAirportsRow rw in recs)
            {
                if (!File.Exists(rw["path"].ToString()))
                    arr.Add((int)rw["id"]);
            }

            return arr;
        }

        public ScanScenery(string path, bool disabledPath)
        {
            
            if (disabledPath)
            {
                basePath = String.Format(@"{0}\{1}", path, DISABLED_PATH);
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);
            }
            else
                basePath = String.Format(@"{0}\{1}",path, CUSTOM_PATH);
        }

        public void SetIDSeed(int seed)
        {
            idseed = seed;
        }

        public CustomSceneryAirports GetReport()
        {
            return ScanFolders(basePath);
        }

        CustomSceneryAirports ScanFolders(string basePath)
        {
            CustomSceneryAirports.CustomAirportsDataTable aptData = new CustomSceneryAirports.CustomAirportsDataTable();
            CustomSceneryAirports.CustomDSFDataTable dsfData = new CustomSceneryAirports.CustomDSFDataTable();
            CustomSceneryAirports.CustomLibsDataTable libData = new CustomSceneryAirports.CustomLibsDataTable();
            CustomSceneryAirports.RunwaysDataTable rwysData = new CustomSceneryAirports.RunwaysDataTable();
            CustomSceneryAirports.ATCDataTable atcData = new CustomSceneryAirports.ATCDataTable();

            CustomSceneryAirports ds = new CustomSceneryAirports();

            if (!Directory.Exists(basePath))
                return ds;

            string[] dirs = Directory.GetDirectories(basePath);
            


            aptData.idColumn.AutoIncrementSeed = idseed;
            // 
            if (thd != null)
            {
                thd.ReportProgress(dirs.Length, "Start");
                thd.ReportProgress(0, String.Format("Scanning {0}", basePath));
            }
            else
                Program.mf.SetProgress(0, String.Format("Scanning {0}", basePath), true, dirs.Length);

            int cnt = 0;
            int dsfCnt = 0;
            int aptCnt = 0;

            int lastId = -1;
            try
            {

                foreach (string pth in dirs)
                {

                    if (thd != null)
                        thd.ReportProgress(cnt++);
                    else
                        Program.mf.UpdateProgress(0, "", cnt++);

                    string aptPath = String.Format(@"{0}\{1}", pth, APT_PATH);

                    if (File.Exists(aptPath))
                    {
                        List<AptData> aptRec = ParseApt(aptPath);

                        foreach (AptData recData in aptRec)
                        {
                            CustomSceneryAirports.CustomAirportsRow rw = aptData.NewCustomAirportsRow();
                            rw.icao = recData.icao;
                            rw.name = recData.name;
                            rw.elevation = recData.elevation;
                            rw.latitude = recData.lat;
                            rw.longitude = recData.lng;
                            rw.path = aptPath;
                            rw.enabled = enabled == true ? 1 : 0;
                            aptData.AddCustomAirportsRow(rw);
                            aptCnt++;

                            //Runways
                            foreach (RwyData rwyRec in recData.rwys)
                            {
                                CustomSceneryAirports.RunwaysRow rwyrw = rwysData.NewRunwaysRow();
                                rwyrw.icao = rwyRec.icao;
                                rwyrw.id = rw.id;
                                rwyrw.surface = rwyRec.surface;
                                rwyrw.heading = rwyRec.heading;
                                rwyrw.length = rwyRec.length;

                                rwyrw.runway1 = rwyRec.runway1;
                                rwyrw.latitude1 = rwyRec.lat1;
                                rwyrw.longitude1 = rwyRec.lng1;
                                rwyrw.ils1 = rwyRec.ils1;

                                rwyrw.runway2 = rwyRec.runway2;
                                rwyrw.latitude2 = rwyRec.lat2;
                                rwyrw.longitude2 = rwyRec.lng2;
                                rwyrw.ils2 = rwyRec.ils2;

                                rwysData.AddRunwaysRow(rwyrw);
                            }

                            foreach (AtcData atcRec in recData.atcs)
                            {
                                CustomSceneryAirports.ATCRow atcrw = atcData.NewATCRow();
                                atcrw.freq = atcRec.freq;
                                atcrw.id = rw.id;
                                atcrw.atcdesc = atcRec.atcdesc;
                                atcrw.atctype = atcRec.atctype;

                                atcData.AddATCRow(atcrw);
                            }

                            lastId = rw.id;
                        }

                        //Now lets scan for DSFs
                        FileInfo fi = new FileInfo(aptPath);
                        if (Directory.Exists(fi.DirectoryName) && enabled)
                        {
                            string[] dsfDirs = Directory.GetDirectories(fi.DirectoryName);
                            foreach (string dsfPath in dsfDirs)
                            {
                                string[] dsfs = Directory.GetFiles(dsfPath);
                                foreach (string dsfFile in dsfs)
                                {
                                    FileInfo dsfi = new FileInfo(dsfFile);
                                    if (dsfi.Extension == ".dsf")
                                    {
                                        CustomSceneryAirports.CustomDSFRow rw = dsfData.NewCustomDSFRow();
                                        rw.path = dsfPath;
                                        rw.dsfFolder = dsfi.Directory.Parent.Parent.Name;
                                        rw.dsfFile = dsfi.Name;
                                        rw.id = lastId;
                                        rw.enabled = enabled == true ? 1 : 0;
                                        dsfData.AddCustomDSFRow(rw);
                                        dsfCnt++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string libPath = String.Format(@"{0}\Library.txt", pth);

                        if (File.Exists(libPath))
                        {
                            CustomSceneryAirports.CustomLibsRow rw = libData.NewCustomLibsRow();
                            rw.path = pth;
                            libData.AddCustomLibsRow(rw);
                        }
                        else
                        {
                            //no apt, no lib, maybe just dsf
                            FileInfo fi = new FileInfo(aptPath);
                            if (Directory.Exists(fi.DirectoryName) && enabled)
                            {
                                string[] dsfDirs = Directory.GetDirectories(fi.DirectoryName);
                                foreach (string dsfPath in dsfDirs)
                                {
                                    string[] dsfs = Directory.GetFiles(dsfPath);
                                    foreach (string dsfFile in dsfs)
                                    {
                                        FileInfo dsfi = new FileInfo(dsfFile);
                                        if (dsfi.Extension == ".dsf")
                                        {
                                            CustomSceneryAirports.CustomDSFRow rw = dsfData.NewCustomDSFRow();
                                            rw.path = dsfPath;
                                            rw.dsfFolder = dsfi.Directory.Parent.Parent.Name;
                                            rw.dsfFile = dsfi.Name;
                                            rw.id = -1;
                                            rw.enabled = enabled == true ? 1 : 0;
                                            dsfData.AddCustomDSFRow(rw);
                                            dsfCnt++;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }

            
            ds.CustomAirports.Load(aptData.CreateDataReader());
            ds.CustomDSF.Load(dsfData.CreateDataReader());
            ds.Runways.Load(rwysData.CreateDataReader());
            ds.ATC.Load(atcData.CreateDataReader());
            ds.CustomLibs.Load(libData.CreateDataReader());


            if (thd != null)
                thd.ReportProgress(0, String.Format("Scanned {0} airports in {1} packages", aptCnt, cnt));
            else
                Program.mf.SetProgress(0, String.Format("Scanned {0} airports in {1} packages", aptCnt, cnt), false, 0);
            

            return ds;
        }

        List<AptData> ParseApt(string xppath) 
        {
            List<AptData> aptCollection = new List<AptData>();
            AptData data = new AptData();
            List<RwyData> rwys = new List<RwyData>();
            List<AtcData> atcs = new List<AtcData>();

            bool recordopen=false;
            bool locset = false;
            int cnt = 0;
            int recCnt = 0;
            PortType currentporttype = PortType.Airport;

            char[] delims = { ' ', '\t' };
            string[] cmts = { "#"};

            using (DelimitedFieldParser parser = new DelimitedFieldParser(xppath))
            {
                parser.SetDelimiters(delims);
                parser.SetCommentTokens(cmts);
	            parser.HasFieldsEnclosedInQuotes = false;
                parser.TrimWhiteSpace = true;
                parser.SqueezeDelimiters = true;                

	            while (!parser.EndOfFile)
	            {
                    try
                    {
                        // Reads the next record.
                        TextFields fields = parser.ReadFields();
                        if (cnt == 0 || fields.Count < 1 || fields[0] == "")
                        {
                            cnt++;
                            continue; //skip first record
                        }

                        if (fields.GetInt32(0) == 99)
                        {
                            //End of File
                            if (recordopen)
                            {
                                //won't get here unless the last airport had no runways (type 100 records) ..which would be strange
                                data.rwys = rwys;
                                data.atcs = atcs;
                                aptCollection.Add(data);
                                recordopen = false;
                            }
                            break;
                        }

                        if (fields.GetInt32(0) == 1 || //airport
                            fields.GetInt32(0) == 16 || //sealane
                            fields.GetInt32(0) == 17) // helipad
                        {
                            if (recordopen)
                            {
                                //won't get here unless the last airport had no runways (type 100 records) ..which would be strange
                                data.rwys = rwys;
                                data.atcs = atcs;
                                aptCollection.Add(data);
                                recordopen = false;
                            }
                            data = new AptData();
                            rwys = new List<RwyData>();
                            atcs = new List<AtcData>();
                            locset = false;

                            data.elevation = Int32.Parse(fields.GetString(1));
                            if (fields.Count < 5)
                            {
                                //problem
                                log.ErrorFormat("not enough fields for {0}", data.name);
                            }
                            data.icao = fields.GetString(4);
                            data.name = fields.GetString(5);
                            for (int i = 6; i < fields.Count; i++)
                            {
                                data.name = String.Format("{0} {1}", data.name, fields.GetString(i));
                            }
                            recordopen = true;
                            currentporttype = (PortType)fields.GetInt32(0);
                            recCnt++;
                        }

                        if (fields.GetInt32(0) == 100 || //runway
                            fields.GetInt32(0) == 101 || //sealane
                            fields.GetInt32(0) == 102) //Helipad
                        {
                            RwyData rwydata = new RwyData();

                            rwydata.icao = data.icao;

                            int rwyoffset = 8; //normal
                            int rwyflds = 9;
                            switch (fields.GetInt32(0))
                            {
                                case 100: //runway
                                    rwydata.surface = Int32.Parse(fields.GetString(2));
                                    rwyoffset = 8;
                                    rwyflds = 9;
                                    break;
                                case 101: // seaport
                                    rwydata.surface = 0; //water
                                    rwyoffset = 3;
                                    rwyflds = 3;
                                    break;
                                case 102: //helipad
                                    rwydata.surface = Int32.Parse(fields.GetString(7));
                                    rwyoffset = 1;
                                    break;
                            }

                            if (fields.GetInt32(0) != 102) //heliports & waterports don't have a second 'end'
                            {
                                //first end
                                rwydata.runway1 = fields.GetString(rwyoffset);
                                rwydata.lat1 = Double.Parse(fields.GetString(rwyoffset + 1), CultureInfo.InvariantCulture);
                                rwydata.lng1 = Double.Parse(fields.GetString(rwyoffset + 2), CultureInfo.InvariantCulture);
                                //second end
                                rwydata.runway2 = fields.GetString(rwyoffset + rwyflds);
                                rwydata.lat2 = Double.Parse(fields.GetString(rwyoffset + rwyflds + 1), CultureInfo.InvariantCulture);
                                rwydata.lng2 = Double.Parse(fields.GetString(rwyoffset + rwyflds + 2), CultureInfo.InvariantCulture);

                                rwydata.length = Geo.HaversineInM(rwydata.lat1, rwydata.lng1, rwydata.lat2, rwydata.lng2);
                                rwydata.heading = Geo.GetDirection(rwydata.lat1, rwydata.lng1, rwydata.lat2, rwydata.lng2);
                            }
                            else
                            {
                                //first end
                                rwydata.runway1 = fields.GetString(rwyoffset);
                                rwydata.lat1 = Double.Parse(fields.GetString(rwyoffset + 1), CultureInfo.InvariantCulture);
                                rwydata.lng1 = Double.Parse(fields.GetString(rwyoffset + 2), CultureInfo.InvariantCulture);
                                //
                                rwydata.runway2 = "";
                                rwydata.lat2 = 0;
                                rwydata.lng2 = 0;

                                rwydata.length = 0;
                                rwydata.heading = 0;
                            }
                            rwys.Add(rwydata);

                            if (!locset)
                            {
                                data.lat = rwydata.lat1;
                                data.lng = rwydata.lng1;
                                locset = true;
                            }
                        }

                        if (fields.GetInt32(0) >= 50 && fields.GetInt32(0) <= 56) //radios
                        {
                            try
                            {
                                AtcData Atcdata = new AtcData();

                                Atcdata.freq = (Double.Parse(fields.GetString(1)) / 100).ToString("#.00");
                                if (fields.Count > 2)
                                    Atcdata.atcdesc = fields.GetString(2);
                                else
                                    Atcdata.atcdesc = "Freq";
                                Atcdata.atctype = Int32.Parse(fields.GetString(0));
                                atcs.Add(Atcdata);
                            }
                            catch (Exception ex)
                            {
                                log.Debug("not enough fields for ATC");
                            }
                        }

                    }
                    catch (MalformedLineException ex)
                    {
                        log.Error(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);
                    }
	                cnt++;
	            }
            }

            return aptCollection;
        }

    }

    class AptData
    {
        public string icao { get; set; }
        public string name { get; set; }
        public int elevation { get; set; }
        public Double lat { get; set; }
        public Double lng { get; set; }
        public List<RwyData> rwys { get; set; }
        public List<AtcData> atcs { get; set; }
    }

    class RwyData
    {
        public string icao { get; set; }
        public int surface { get; set; }
        public Double heading { get; set; }
        public int length { get; set; }

        public string runway1 { get; set; }
        public Double lat1 { get; set; }
        public Double lng1 { get; set; }
        public Double ils1 { get; set; }

        public string runway2 { get; set; }
        public Double lat2 { get; set; }
        public Double lng2 { get; set; }
        public Double ils2 { get; set; }
    }

    class AtcData
    {
        public string freq { get; set; }
        public string atcdesc { get; set; }
        public int atctype { get; set; }
    }
}