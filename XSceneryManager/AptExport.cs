using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using TextFileParsers;

namespace XSceneryManager
{
    class AptExport
    {
        public AptExport()
        {
            string xpaptpth = @"D:\X-Plane9\Resources\default scenery\default apt dat\Earth nav data\apt.dat";

            AptParser apt = new AptParser(xpaptpth, @"D:\X-PlaneDev\xp-ap-data.csv");
        }
    }



    class AptParser
    {
        private StreamWriter fs;

        public AptParser(string xppath, string outfile)
        {
            fs = new StreamWriter(outfile);
            ParseIt(xppath, outfile);

        }
            
        void ParseIt(string xppath, string outfile)  {
            bool recordopen=false;
            int cnt = 0;
            int recCnt = 0;

            char[] delims = { ' ', '\t' };

            using (DelimitedFieldParser parser = new DelimitedFieldParser(xppath))
            {
                parser.SetDelimiters(delims);
	            parser.HasFieldsEnclosedInQuotes = false;
                parser.TrimWhiteSpace = true;
                parser.SqueezeDelimiters = true;
                recData data = null;
	
	            while (!parser.EndOfFile)
	            {
		            try
		            {
			            // Reads the next record.
			            TextFields fields = parser.ReadFields();
                        if (cnt == 0 || fields.Count <= 1)
                        {
                            cnt++;
                            continue; //skip first record
                        }

                        if (fields.GetInt32(0) == 1)
                        {
                            if (recordopen)
                            {
                                WriteRecord(data, recCnt);
                                
                                recordopen = false;
                            }
                            data = new recData();

                            data.elevation = fields.GetString(1); 
                            data.icao = fields.GetString(4);
                            data.name = fields.GetString(5);
                            for (int i = 6; i < fields.Count; i++)
                            {
                                data.name = String.Format("{0} {1}", data.name,fields.GetString(i));
                            }
                            recordopen = true;
                        }

                        if (recordopen && fields.GetInt32(0) == 100)
                        {
                            data.lat = fields.GetString(9);
                            data.lng = fields.GetString(10);
                            WriteRecord(data, recCnt);
                            recordopen = false;
                            recCnt++;
                        }

		            }
		            catch (MalformedLineException e)
		            {
			            // Handle exception here.
		            }
                    cnt++;
                    if (cnt % 100 == 0)
                        System.Console.WriteLine("{0} records scanned, {1} records written", cnt, recCnt);
	            }
            }

            fs.Close();
        }

        void WriteRecord(recData data, int rowid)
        {
            fs.WriteLine("{5},\"{0}\",\"{1}\",{2},{3},{4}", data.icao, data.name, data.elevation, data.lat, data.lng, rowid + 10000);
        }

    }

    class recData
    {
        public string icao { get; set; }
        public string name { get; set; }
        public string elevation { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
