using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Windows.Forms;
using log4net;

namespace XSceneryManager
{
    class DBAccess
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DBAccess));

        public const string DBNAME = "xairportscenery.db";
        public const int DBVERSION = 1;

        private const string CREATE_CUS_TABLE = "CREATE TABLE custompkgs (id NUM, icao TEXT,name TEXT, elevation INT, latitude NUM, longitude NUM, path TEXT, enabled INT)";
        private const string CREATE_DSF_TABLE = "CREATE TABLE customdsfs (id NUM, path TEXT, dsffolder TEXT, dsffile TEXT, enabled INT)";
        private const string CREATE_LIB_TABLE = "CREATE TABLE customlibs (path TEXT)";

        private const string CREATE_RSS_TABLE = "CREATE TABLE rssfeeds (name TEXT, link TEXT, status INT)";
        private const string CREATE_APT_TABLE = "CREATE TABLE airports (id NUM, icao TEXT,name TEXT, elevation INT, latitude NUM, longitude NUM)";
        private const string CREATE_RWY_TABLE = "CREATE TABLE runways (id NUM, icao TEXT,surface TEXT, heading NUM, length NUM, runway1 TEXT, latitude1 NUM, longitude1 NUM, ils1 NUM, runway2 TEXT, latitude2 NUM, longitude2 NUM, ils2 NUM)";
        private const string CREATE_ATC_TABLE = "CREATE TABLE atc (id NUM, freq TEXT, atcdesc TEXT, atctype NUM)";

        private const string INSERT_CUS_REC = "INSERT INTO custompkgs (id,icao,name,elevation,latitude,longitude,path,enabled) values (@id,@icao,@name,@elevation,@latitude,@longitude,@path,@enabled)";
        private const string INSERT_DSF_REC = "INSERT INTO customdsfs (id,path,dsffolder,dsffile,enabled) values (@id,@path,@dsffolder,@dsffile,@enabled)";
        private const string INSERT_APT_REC = "INSERT INTO airports (id,icao,name,elevation,latitude,longitude,path,enabled) values (@id,@icao,@name,@elevation,@latitude,@longitude)";
        private const string INSERT_ATC_REC = "INSERT INTO atc (id, freq, atcdesc, atctype) values (@id,@freq,@atcdesc,@atctype)";
        private const string INSERT_LIB_REC = "INSERT INTO customlibs (path) VALUES (@path)";

        private const string INSERT_RWY_REC =
            "INSERT INTO runways (id, icao,surface, heading, length, runway1, latitude1, longitude1, ils1, runway2, latitude2, longitude2, ils2) VALUES (@id, @icao, @surface, @heading, @length, @runway1, @latitude1, @longitude1, @ils1, @runway2, @latitude2, @longitude2, @ils2)";

        private const string UPDATE_CUS_REC = "UPDATE custompkgs SET icao=@icao,name=@name,elevation=@elevation,latitude=@latitude,longitude=@longitude,path=@path,enabled=@enabled WHERE id=@id";
        private const string UPDATE_DSF_REC = "UPDATE customdsfs SET path=@path,dsffolder=@dsffolder,dsffile=@dsffile,enabled=@enabled WHERE id=@id";

        private const string UPSERT_RSS_REC = "INSERT OR REPLACE INTO rssfeeds (name, link, status) VALUES (@name, @link, @status)";

        private const string ENABLE_AIRPORT = "UPDATE custompkgs SET enabled=@enabled,path=@path WHERE id=@id";
        private const string DELETE_AIRPORT = "DELETE FROM custompkgs WHERE id=@id";

        private SQLiteConnection _conn = null;


        public const string LOGFILE = "xscenerymanager.log";
        
        public DBAccess()
        {
            _conn = GetDB();
        }

        public void Close()
        {
            if (_conn != null)
                _conn.Close();
        }

        
        private SQLiteConnection GetDB()
        {

            int thisVersion = 0;

            if (_conn == null)
            {
                _conn = new SQLiteConnection(String.Format("Data Source={0};Version=3;", DBNAME));
                _conn.Open();


                SQLiteCommand cmd = _conn.CreateCommand();
                try
                {
                    cmd.CommandText = "Create table XSM (DBVersion NUM)";
                    cmd.ExecuteNonQuery();
                    log.Info("Created Version Table");
                }
                catch (Exception)
                {
                    //Probably exists already
                    cmd = _conn.CreateCommand();
                    cmd.CommandText = "SELECT dbversion FROM XSM";
                    SQLiteDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        rd.Read();
                        thisVersion = Int32.Parse(rd["DBVersion"].ToString());
                    }
                    rd.Close();
                }

                if (thisVersion < DBVERSION)
                {
                    if (DBVERSION == 1)
                    {
                        cmd.CommandText = "DROP TABLE IF EXISTS atc";
                        cmd.ExecuteNonQuery();
                    }
                }

                try
                {
                    cmd.CommandText = CREATE_CUS_TABLE;
                    cmd.ExecuteNonQuery();
                    log.Info("Created Custom Airports Table");
                }
                catch (Exception)
                {
                    //Probably exists already
                }

                try
                {
                    cmd.CommandText = CREATE_DSF_TABLE;
                    cmd.ExecuteNonQuery();
                    log.Info("Created Custom DSFs Table");
                }
                catch (Exception)
                {
                }

                try
                {
                    cmd.CommandText = CREATE_LIB_TABLE;
                    cmd.ExecuteNonQuery();
                    log.Info("Created Custom LIBs Table");
                }
                catch (Exception)
                {
                }

                try {
                cmd.CommandText = CREATE_RSS_TABLE;
                    cmd.ExecuteNonQuery();
                    log.Info("Created Rss Table");
                }
                catch (Exception)
                {
                }
                try {
                    cmd.CommandText = CREATE_APT_TABLE;
                    cmd.ExecuteNonQuery();
                    log.Info("Created Global Airports Table");
                }
                catch (Exception)
                {
                }
                try
                {
                    cmd.CommandText = CREATE_RWY_TABLE;
                    cmd.ExecuteNonQuery();
                    log.Info("Created Runways Table");
                }
                catch (Exception)
                {
                }
                try
                {
                    cmd.CommandText = CREATE_ATC_TABLE;
                    cmd.ExecuteNonQuery();
                    log.Info("Created ATC Table");
                }
                catch (Exception)
                {
                }

                try
                {
                    if (thisVersion != DBVERSION)
                    {
                        cmd.CommandText = String.Format("INSERT OR REPLACE INTO XSM (DBVersion) VALUES ({0})", DBVERSION);
                        cmd.ExecuteNonQuery();
                        log.Info("Updated Version");
                    }
                }
                catch (Exception)
                { }
                
            }

            return _conn;
        }


        public CustomSceneryAirports.CustomAirportsDataTable GetAirports()
        {
            CustomSceneryAirports.CustomAirportsDataTable tb = new CustomSceneryAirports.CustomAirportsDataTable();

            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM custompkgs";

                tb.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return tb;
        }

        public CustomSceneryAirports.CustomDSFDataTable GetDSFs()
        {
            CustomSceneryAirports.CustomDSFDataTable tb = new CustomSceneryAirports.CustomDSFDataTable();

            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM customdsfs";

                tb.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return tb;
        }

        public CustomSceneryAirports.CustomLibsDataTable GetLibraries()
        {
            CustomSceneryAirports.CustomLibsDataTable tb = new CustomSceneryAirports.CustomLibsDataTable();

            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM customlibs";

                tb.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return tb;
        }

        public CustomSceneryAirports.RunwaysDataTable GetRunways()
        {
            CustomSceneryAirports.RunwaysDataTable tb = new CustomSceneryAirports.RunwaysDataTable();

            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM runways";

                tb.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return tb;
        }

        public CustomSceneryAirports.ATCDataTable GetATCs()
        {
            CustomSceneryAirports.ATCDataTable tb = new CustomSceneryAirports.ATCDataTable();

            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM atc";

                tb.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return tb;
        }

        public void ClearTables()
        {
            //Clear tables
            try
            {

                SQLiteCommand delCmd = _conn.CreateCommand();
                delCmd.CommandText = "DELETE FROM custompkgs";
                delCmd.ExecuteNonQuery();

                delCmd.CommandText = "DELETE FROM customdsfs";
                delCmd.ExecuteNonQuery();

                delCmd.CommandText = "DELETE FROM customlibs";
                delCmd.ExecuteNonQuery();

                delCmd.CommandText = "DELETE FROM runways";
                delCmd.ExecuteNonQuery();

                delCmd.CommandText = "DELETE FROM atc";
                delCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                log.Error(ex.Message);
            }
        }

        public void InsertScanData(CustomSceneryAirports apts, bool enabled)
        {

            prepareConnectionForBulkInsert(_conn);
            SQLiteTransaction dbTrans = _conn.BeginTransaction();
            foreach (CustomSceneryAirports.CustomAirportsRow rw in apts.CustomAirports)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    //insert a row of data
                    cmd.CommandText = INSERT_CUS_REC;
                    cmd.Parameters.AddWithValue("@id", rw.id);
                    cmd.Parameters.AddWithValue("@icao", rw.icao);
                    cmd.Parameters.AddWithValue("@name", rw.name);
                    cmd.Parameters.AddWithValue("@elevation", rw.elevation);
                    cmd.Parameters.AddWithValue("@latitude", rw.latitude);
                    cmd.Parameters.AddWithValue("@longitude", rw.longitude);
                    cmd.Parameters.AddWithValue("@path", rw.path);
                    cmd.Parameters.AddWithValue("@enabled", enabled);

                    int ret = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    log.Error(ex.Message);
                } 
            } 
            InsertRunwayData(apts.Runways);
            InsertAtcData(apts.ATC);

            //insert dsfs
            foreach (CustomSceneryAirports.CustomDSFRow rw in apts.CustomDSF)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    //insert a row of data
                    cmd.CommandText = INSERT_DSF_REC;
                    cmd.Parameters.AddWithValue("@id", rw.id);
                    cmd.Parameters.AddWithValue("@path", rw.path);
                    cmd.Parameters.AddWithValue("@dsffolder", rw.dsfFolder);
                    cmd.Parameters.AddWithValue("@dsffile", rw.dsfFile);
                    cmd.Parameters.AddWithValue("@enabled", enabled);

                    int ret = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    log.Error(ex.Message);
                }
            }

            //insert libs
            foreach (CustomSceneryAirports.CustomLibsRow rw in apts.CustomLibs)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    //insert a row of data
                    cmd.CommandText = INSERT_LIB_REC;
                    cmd.Parameters.AddWithValue("@path", rw.path);

                    int ret = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    log.Error(ex.Message);
                }
            }

            dbTrans.Commit();
           
        }

        public void InsertRunwayData(CustomSceneryAirports.RunwaysDataTable rwys)
        {
            //prepareConnectionForBulkInsert(_conn);
            //SQLiteTransaction dbTrans = _conn.BeginTransaction();
            foreach (CustomSceneryAirports.RunwaysRow rw in rwys)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    //insert a row of data
                    cmd.CommandText = INSERT_RWY_REC;
                    cmd.Parameters.AddWithValue("@id", rw.id);
                    cmd.Parameters.AddWithValue("@icao", rw.icao);
                    cmd.Parameters.AddWithValue("@surface", rw.surface);
                    cmd.Parameters.AddWithValue("@heading", rw.heading);
                    cmd.Parameters.AddWithValue("@length", rw.length);

                    cmd.Parameters.AddWithValue("@runway1", rw.runway1);
                    cmd.Parameters.AddWithValue("@latitude1", rw.latitude1);
                    cmd.Parameters.AddWithValue("@longitude1", rw.longitude1);
                    cmd.Parameters.AddWithValue("@ils1", rw.ils1);
                    cmd.Parameters.AddWithValue("@runway2", rw.runway2);
                    cmd.Parameters.AddWithValue("@latitude2", rw.latitude2);
                    cmd.Parameters.AddWithValue("@longitude2", rw.longitude2);
                    cmd.Parameters.AddWithValue("@ils2", rw.ils1);


                    int ret = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    log.Error(ex.Message);
                }
            }
            //dbTrans.Commit();
        }

        public void InsertAtcData(CustomSceneryAirports.ATCDataTable atcs)
        {
            //prepareConnectionForBulkInsert(_conn);
            //SQLiteTransaction dbTrans = _conn.BeginTransaction();
            foreach (CustomSceneryAirports.ATCRow rw in atcs)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    //insert a row of data
                    cmd.CommandText = INSERT_ATC_REC;
                    cmd.Parameters.AddWithValue("@id", rw.id);
                    cmd.Parameters.AddWithValue("@freq", rw.freq);
                    cmd.Parameters.AddWithValue("@atcdesc", rw.atcdesc);
                    cmd.Parameters.AddWithValue("@atctype", rw.atctype);

                    int ret = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    log.Error(ex.Message);
                }
            }
            //dbTrans.Commit();
        }

        public void InsertAptData(CustomSceneryAirports apts)
        {

            prepareConnectionForBulkInsert(_conn);
            SQLiteTransaction dbTrans = _conn.BeginTransaction();
            foreach (CustomSceneryAirports.CustomAirportsRow rw in apts.CustomAirports)
            {
                try
                {
                    SQLiteCommand cmd = _conn.CreateCommand();
                    //insert a row of data
                    cmd.CommandText = INSERT_APT_REC;
                    cmd.Parameters.AddWithValue("@id", rw.id);
                    cmd.Parameters.AddWithValue("@icao", rw.icao);
                    cmd.Parameters.AddWithValue("@name", rw.name);
                    cmd.Parameters.AddWithValue("@elevation", rw.elevation);
                    cmd.Parameters.AddWithValue("@latitude", rw.latitude);
                    cmd.Parameters.AddWithValue("@longitude", rw.longitude);

                    int ret = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    log.Error(ex.Message);
                }
            }
        }

        public void InsertRSSData(RSSDataSet.RSSDataDataTable rssDt)
        {
            prepareConnectionForBulkInsert(_conn);
            SQLiteTransaction dbTrans = _conn.BeginTransaction();

            SQLiteCommand cmd = _conn.CreateCommand();
            cmd.CommandText = "DELETE FROM rssfeeds";
            cmd.ExecuteNonQuery();

            foreach (RSSDataSet.RSSDataRow rw in rssDt)
            {
                try
                {
                    cmd = _conn.CreateCommand();
                    //insert a row of data
                    cmd.CommandText = UPSERT_RSS_REC;
                    cmd.Parameters.AddWithValue("@name", rw.name);
                    cmd.Parameters.AddWithValue("@link", rw.link);
                    cmd.Parameters.AddWithValue("@status", rw.status);

                    int ret = cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    log.Error(ex.Message);
                }
            }
            dbTrans.Commit();
        }

        public void DisableRow(object rwidx, string destPath)
        {
            ChangeAirportStatus((int) rwidx, 0, destPath);
        }

        public void EnableRow(object rwidx, string destPath)
        {
            ChangeAirportStatus((int)rwidx, 1, destPath);
        }

        private void ChangeAirportStatus(int idx, int state, string destPath)
        {
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.Parameters.AddWithValue("@id", idx);
                cmd.Parameters.AddWithValue("@path", destPath);
                cmd.Parameters.AddWithValue("@enabled", state);
                cmd.CommandText = ENABLE_AIRPORT;
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                log.Error(ex.Message);
            }
        }

        public void DeleteAirportRecord(int idx)
        {
            try
            {
                SQLiteCommand cmd = _conn.CreateCommand();
                cmd.Parameters.AddWithValue("@id", idx);
                cmd.CommandText = DELETE_AIRPORT;
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                log.Error(ex.Message);
            }
        }

        //RSS
        public RSSDataSet.RSSDataDataTable GetRSSData()
        {
            RSSDataSet.RSSDataDataTable rs = new RSSDataSet.RSSDataDataTable();

            SQLiteCommand cmd = _conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM rssfeeds";

            try
            {
                rs.Load(cmd.ExecuteReader());
            }
            catch (SQLiteException ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
            return rs;
        }

        private void prepareConnectionForBulkInsert(SQLiteConnection cn)
        {

            SQLiteCommand stmt;

            stmt = new SQLiteCommand("PRAGMA synchronous=OFF", cn);
            stmt.ExecuteNonQuery();

            stmt = new SQLiteCommand("PRAGMA count_changes=OFF", cn);
            stmt.ExecuteNonQuery();

            stmt = new SQLiteCommand("PRAGMA journal_mode=MEMORY", cn);
            stmt.ExecuteNonQuery();

            stmt = new SQLiteCommand("PRAGMA temp_store=MEMORY", cn);
            stmt.ExecuteNonQuery();

        }

        ~DBAccess()
        {
            try
            {
                if (_conn != null)
                    _conn.Close();
            }
            catch (Exception)
            {
            }
        } 
    }


}
