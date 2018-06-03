using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using XSceneryManager.Properties;

namespace XSceneryManager
{
    class KMLGen
    {
        private string placemarkTemplate = "";
        CustomSceneryAirports.CustomAirportsDataTable tble = new CustomSceneryAirports.CustomAirportsDataTable();
        CustomSceneryAirports.CustomDSFDataTable dsftble = new CustomSceneryAirports.CustomDSFDataTable();

        public KMLGen(CustomSceneryAirports.CustomAirportsDataTable tb)
        {
            placemarkTemplate = Resources.KMLTemplatePlacemark;
            tble = tb;
            CustomSceneryAirports.CustomAirportsRow rw;
            tble.DefaultView.RowFilter = "enabled=1";
        }

        public KMLGen(CustomSceneryAirports.CustomDSFDataTable tb)
        {
            placemarkTemplate = Resources.KMLPolygonPlacemark;
            dsftble = tb;
        }

        public string WriteKMLFile(string path)
        {
            string fname = String.Format("{0}\\XPlaneScenery.kml", path);

            string hdr = Resources.KMLTemplateHeader;
            StringBuilder placemarks = new StringBuilder();

            for (int i = 0; i < tble.Rows.Count; i++)
            {
                placemarks.Append(GetPlaceMarkForICAO(i));
            }

            StreamWriter f = new StreamWriter(fname);
            f.Write(String.Format(hdr, placemarks.ToString()));
            f.Close();


            return fname;
        }

        private string GetPlaceMarkForICAO(int idx)
        {
            string name = tble.Rows[idx]["name"].ToString(); //.Replace(@"&",@"&amp;");
            name = System.Uri.EscapeDataString(name);
            name = name.Replace(@"%20", " ");

            if (name != null && name.Length > 5 && name.Substring(0, 4).Equals(tble.Rows[idx]["icao"].ToString()))
                name = tble.Rows[idx]["name"].ToString().Substring(4);

            return String.Format(Resources.KMLTemplatePlacemark,
                                tble.Rows[idx]["icao"],
                                Convert.ToString(tble.Rows[idx]["longitude"], CultureInfo.InvariantCulture),
                                Convert.ToString(tble.Rows[idx]["latitude"], CultureInfo.InvariantCulture),
                                tble.Rows[idx]["elevation"],
                                name);

        }


        public string WriteDSFKMLFile(string path)
        {
            string fname = String.Format("{0}\\XPlaneDSFCoverage.kml", path);

            string hdr = Resources.KMLDSFTemplateDoc;
            StringBuilder placemarks = new StringBuilder();

            for (int i = 0; i < dsftble.Rows.Count; i++)
            {
                placemarks.Append(GetPolygonForDSF(i));
            }

            StreamWriter f = new StreamWriter(fname);
            f.Write(String.Format(hdr, placemarks.ToString()));
            f.Close();


            return fname;
        }

        private string GetPolygonForDSF(int idx)
        {
            char[] sep = { ' ', '(', ')', '_', '.' };

            string coord = dsftble.Rows[idx]["dsffile"].ToString(); //.Replace(@"&",@"&amp;");

            if (coord.EndsWith(".dsf"))
            {
                coord = coord.Split(sep)[0];

                int part2 = coord.LastIndexOf('+');
                if (part2 < 1)
                    part2 = coord.LastIndexOf('-');

                int coord2 = Int32.Parse(coord.Substring(0, part2));
                int coord1 = Int32.Parse(coord.Substring(part2));

                //Extract the package from the path
                string pkgname = dsftble.Rows[idx]["dsfFolder"].ToString() + " (" + dsftble.Rows[idx]["dsffile"].ToString() + ")";

                return String.Format(Resources.KMLPolygonPlacemark,
                                    pkgname,
                                    coord1, coord2, //top-left                                
                                    coord1, coord2 > 0 ? coord2 + 1 : coord2 - 1, //top-right   
                                    coord1 > 0 ? coord1 + 1 : coord1 - 1, coord2 > 0 ? coord2 + 1 : coord2 - 1,
                                    coord1 > 0 ? coord1 + 1 : coord1 - 1, coord2 //bottom-left                                
                                    ); //bottom-right);
            }
            else
                return "";
        }

        public string GNS430Gen(string path, CustomSceneryAirports.RunwaysDataTable rwys)
        {
            string fname = path;
            string[] surfaces = { "3", "1", "0", "3", "2", "2", "3", "3", "3", "3", "3", "3", "2", "3", "2", "3" };

            using (StreamWriter f = new StreamWriter(fname))
            {
                string header = String.Format("X,{0}{1},01{3}{2}{3}/{0},{0}{1},01{3}{2}{3}/{0}", 
                            DateTime.Now.ToString("yy"),
                            DateTime.Now.ToString("MM"),
                            DateTime.Now.ToString("dd"),
                            DateTime.Now.ToString("MMM"));
                f.WriteLine(header);
                

                CustomSceneryAirports.RunwaysDataTable runways = rwys;
                foreach (CustomSceneryAirports.CustomAirportsRow ap in tble)
                {
                    //A record, ICAO
                    string arecord = String.Format("A,{0},{1},{2},{3},{4},{5},{6},[LLR]",
                                            ap.icao, ap.name, ap.latitude, ap.longitude, ap.longitude, ap.elevation, 0, 0);
                    //R records
                    double maxlen = 0;
                    ArrayList rwylst = new ArrayList();

                    foreach (CustomSceneryAirports.RunwaysRow rw in runways.Select(String.Format("id='{0}'", ap.id)))
                    {
                        if (rw.runway1.Length == 0 || rw.runway1.StartsWith("H"))
                            continue;

                        //R=Runway, id, hdg, len, width, ils av, ils_freq, ils_hdg, thresh_lat, thresh_lon, thresh_elv, gs_ang, thresh_overfly_alt, surf type, rwy status
                        string rrecord = String.Format("R,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                            rw.runway1, rw.heading, rw.length, 50, rw.ils1 > 0 ? 1 : 0, rw.ils1, rw.heading, rw.latitude1, rw.longitude1, ap.elevation, 0, 200, surfaces[rw.surface], 0);
                        rwylst.Add(rrecord);

                        double hding = rw.heading - 180;
                        string rrecord2 = String.Format("R,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                            rw.runway2, hding < 0 ? 360 + hding : hding, rw.length, 50, rw.ils2 > 0 ? 1 : 0, rw.ils2, rw.heading, rw.latitude2, rw.longitude2, ap.elevation, 0, 200, surfaces[rw.surface], 0);
                        rwylst.Add(rrecord2);

                        if (rw.length > maxlen)
                            maxlen = rw.length;
                    }
                    arecord = arecord.Replace("[LLR]", String.Format("{0}", maxlen));
                    f.WriteLine("");
                    f.WriteLine(arecord);
                    for (int i = 0; i < rwylst.Count; i++)
                    {
                        f.WriteLine(rwylst[i].ToString());
                    }

                }

            }
         

            return fname;
        }
    }

}
