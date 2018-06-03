using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using XSceneryManager.Properties;
using ICSharpCode.SharpZipLib;
using System.IO;

namespace XSceneryManager
{
    public partial class AddScenery : Form
    {
        private const string EARTHNAVDATA = "Earth nav data";
        public AddScenery()
        {
            InitializeComponent();

            tbSceneryFile.Text = Settings.Default.LastSceneryDownloadFolder;
            tbZipFileDesc.Text = "Drop your scenery file here, or use the browse button above to locate it.";

        }

        public void AddSceneryPath(string path)
        {
            tbSceneryFile.Text = path;
            LookInZip();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFileSearch_Click(object sender, EventArgs e)
        {
            ofdNewScenery.InitialDirectory = tbSceneryFile.Text;
            if (ofdNewScenery.ShowDialog() == DialogResult.OK)
            {
                tbSceneryFile.Text = ofdNewScenery.FileName;
                Settings.Default.LastSceneryDownloadFolder = ofdNewScenery.FileName.Substring(0,
                                                                                              ofdNewScenery.FileName.LastIndexOf(@"\"));
                Settings.Default.Save();
                LookInZip();
            }
        }

        private void LookInZip()
        {
            char[] seps = {'/'};
            
            if (tbSceneryFile.Text.EndsWith(".zip"))
            {
                //try opening it
                bool aptFound = false;
                bool earthNavDataFound = false;
                bool libraryFound = false;
                
                lblEarthNav.Text = "";
                lblZipType.Text = "";

                try
                {
                    ICSharpCode.SharpZipLib.Zip.ZipFile zf = new ZipFile(tbSceneryFile.Text);
                    tbZipFileDesc.Text = String.Format("Contains {0} files", zf.Count);
                    pbZipProgress.Maximum = (int)zf.Count;

                    foreach (ZipEntry ze in zf)
                    {
                        if (ze.Name.Contains(@"library.txt"))
                            libraryFound = true;

                        if (ze.Name.Contains(@"Earth nav data/apt.dat"))
                        {
                            aptFound = true;
                            tbZipFileDesc.Text = String.Format("{0}\r\nAirport (apt.dat) found!", tbZipFileDesc.Text);
                            tbZipFileDesc.Text = String.Format("{0}\r\n{1}", tbZipFileDesc.Text, ze.Name);
                            lblZipType.Text = "A";
                        }

                        if (ze.Name.Contains(EARTHNAVDATA) && ze.IsDirectory && !earthNavDataFound)
                        {
                            earthNavDataFound = true;

                            tbZipFileDesc.Text = String.Format("{0}\r\n{1}", tbZipFileDesc.Text, ze.Name);
                            string[] parts = ze.Name.Split(seps);
                            if (parts.Length > 1 && parts[1].Equals(EARTHNAVDATA))
                            {
                                tbZipFileDesc.Text = String.Format("{0}\r\nStruture looks good!", tbZipFileDesc.Text);
                                if (tbDestName.Text == "")
                                    tbDestName.Text = parts[0];

                                tbZipFileDesc.Text = String.Format("{0}\r\nPackage name will be: {1}",
                                                                   tbZipFileDesc.Text,
                                                                   tbDestName.Text);
                                lblEarthNav.Text = "1";
                                btnInstall.Enabled = true;
                            }
                            else if (parts.Length == 1  && parts[0].Equals(EARTHNAVDATA))
                            {
                                tbZipFileDesc.Text = String.Format("{0}\r\nStruture looks OK, though not conventional.",
                                                                   tbZipFileDesc.Text);
                                if (tbDestName.Text == "")
                                    tbDestName.Text = zf.Name;

                                tbZipFileDesc.Text = String.Format("{0}\r\nPackage name will be: {1}",
                                                                   tbZipFileDesc.Text,
                                                                   tbDestName.Text);
                                lblEarthNav.Text = "0";
                                btnInstall.Enabled = true;
                            }
                            else
                            {
                                tbZipFileDesc.Text =
                                    String.Format(
                                        "{0}\r\nThe package is unconventional and cannot be automatically installed. You will have to install this package manually.",
                                        tbZipFileDesc.Text);
                            }

                            //shall we break?
                            if (aptFound)
                                break;
                        }
                    }
                    zf.Close();
                    if (aptFound == false)
                    {
                        tbZipFileDesc.Text =
                            String.Format(
                                "{0}\r\nThis scenery package does not contain an airport", tbZipFileDesc.Text);
                        lblZipType.Text = "S";
                    }
                    if (earthNavDataFound == false && libraryFound == false)
                    {
                        tbZipFileDesc.Text =
                            String.Format(
                                "{0}\r\nThis scenery package does not contain scenery and does not appear to be a library. Please install manually.",
                                tbZipFileDesc.Text);
                    }
                    if (libraryFound == true && aptFound == false && earthNavDataFound == false)
                    {
                        tbZipFileDesc.Text =
                            String.Format(
                                "{0}\r\nThis scenery package looks like a library. We can handle that!",
                                tbZipFileDesc.Text);
                        lblZipType.Text = "L";

                        btnInstall.Enabled = true;
                    }
                }
                catch (Exception)
                {
                    tbZipFileDesc.Text =
                            String.Format(
                                "Didn't like that file!\r\nPlease install manually.");
                }
            }
            else
            {
                MessageBox.Show("I can only install zip files with a .zip extension!", "Error");
            }
        }

        private void tbSceneryFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tbSceneryFile_DragDrop(object sender, DragEventArgs e)
        {
            DragZipFile(e);
        }

        private void tbZipFileDesc_DragDrop(object sender, DragEventArgs e)
        {
            DragZipFile(e);
        }

        private void tbZipFileDesc_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void DragZipFile(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in  
                // case the user has selected multiple files. 
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    // Assign the first image to the picture variable. 
                    tbSceneryFile.Text = files[0];
                    // Set the picture location equal to the drop point. 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            LookInZip();
        }

        /// <summary>
        /// Install a zip package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstall_Click(object sender, EventArgs e)
        {
            int pbCnt = 0;
            btnInstall.Enabled = false;
            pbZipProgress.Enabled = true;
            lblInstalling.Enabled = true;

            tbZipFileDesc.Text = "Installing package...\r\n";

            string installPath = String.Format("{0}\\Custom Scenery\\{1}", Settings.Default.XPlaneFolder, tbDestName.Text);
            if (Directory.Exists(installPath))
            {
                if (MessageBox.Show("The scenery path already exists. Overwrite?", "Alert", MessageBoxButtons.YesNo) ==
                    DialogResult.No)
                    return;
                if (MessageBox.Show("Are you sure?", "Alert", MessageBoxButtons.YesNo) ==
                    DialogResult.No)
                    return;

                //clear the path
                tbZipFileDesc.Text = tbZipFileDesc.Text + "Deleting old scenery...\r\n";
                try
                {
                    Directory.Delete(installPath, true);
                }
                catch (Exception ex)
                {
                    //try file by file delete
                    DeleteXPPath(installPath);
                }
                
            }

            char[] sep = { '\\' };
            int eseg = Int32.Parse(lblEarthNav.Text);

            //let the games begin!
            // ----------------------------------------
            try
            {
                string InputPathOfZipFile = tbSceneryFile.Text;
                if (File.Exists(InputPathOfZipFile))
                {
                    //string baseDirectory = Path.GetDirectoryName(InputPathOfZipFile);

                    using (ZipInputStream ZipStream = new ZipInputStream(File.OpenRead(InputPathOfZipFile)))
                    {
                        ZipEntry theEntry;
                        while ((theEntry = ZipStream.GetNextEntry()) != null)
                        {
                            string destName = theEntry.Name.Replace(@"/",@"\");
                            if (eseg == 1)
                            {
                                destName = destName.Substring(destName.IndexOf(@"\"));
                            }
                            destName = String.Format("{0}{1}", installPath, destName);

                            if (theEntry.IsFile)
                            {
                                if (theEntry.Name != "")
                                {
                                    string strNewFile =  destName; 
                                    if (File.Exists(strNewFile))
                                    {
                                        continue;
                                    }

                                    //construct directory structure
                                    if (!Directory.Exists(destName.Substring(0,destName.LastIndexOf(@"\") - 1)))
                                    {
                                        string[] dir = strNewFile.Split(sep);
                                        string pth = String.Format("{0}\\Custom Scenery", Settings.Default.XPlaneFolder);
                                        for (int i = 3; i < dir.Length - 1; i++ ) // -1 because this is a file
                                        {
                                            pth = String.Format(@"{0}\{1}", pth, dir[i]);
                                            if (!Directory.Exists(pth))
                                                Directory.CreateDirectory(pth);
                                        }
                                    }

                                    using (FileStream streamWriter = File.Create(strNewFile))
                                    {
                                        int size = 2048;
                                        byte[] data = new byte[2048];
                                        while (true)
                                        {
                                            size = ZipStream.Read(data, 0, data.Length);
                                            if (size > 0)
                                                streamWriter.Write(data, 0, size);
                                            else
                                                break;
                                        }
                                        streamWriter.Close();
                                    }
                                }
                            }
                            else if (theEntry.IsDirectory)
                            {
                                string strNewDirectory = destName; 
                                if (!Directory.Exists(strNewDirectory))
                                {
                                    Directory.CreateDirectory(strNewDirectory);
                                }
                            }
                            pbZipProgress.Value = pbCnt++;
                        }
                        ZipStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Oh noes, something went wrong! Install manually please.");
                this.Close();
            }
            this.Close();

            btnInstall.Enabled = true;
            pbZipProgress.Enabled = false;
            lblInstalling.Enabled = false;
        }

        private bool DeleteXPPath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            bool success = false;

            if (di.Exists)
            {
                foreach (FileInfo fi in di.GetFiles("*.*"))
                {
                    try
                    {
                        File.Delete(fi.FullName);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                foreach (DirectoryInfo inf in di.GetDirectories())
                {
                    success = DeleteXPPath(inf.FullName);
                }
                try
                {
                    Directory.Delete(path);
                    success = true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return success;
        }

    }
}
/*
    public static bool UnZipFile(string InputPathOfZipFile)  
            {  
                bool ret = true;  
                try  
                {  
                    if (File.Exists(InputPathOfZipFile))  
                    {  
                        string baseDirectory = Path.GetDirectoryName(InputPathOfZipFile);  
      
                        using (ZipInputStream ZipStream = new   
      
    ZipInputStream(File.OpenRead(InputPathOfZipFile)))  
                        {  
                            ZipEntry theEntry;  
                            while ((theEntry = ZipStream.GetNextEntry()) != null)  
                            {  
                                if (theEntry.IsFile)  
                                {  
                                    if (theEntry.Name != "")  
                                    {  
                                        string strNewFile = @"" + baseDirectory + @"\" +   
      
    theEntry.Name;  
                                        if (File.Exists(strNewFile))  
                                        {  
                                            continue;  
                                        }  
      
                                        using (FileStream streamWriter = File.Create(strNewFile))  
                                        {  
                                            int size = 2048;  
                                            byte[] data = new byte[2048];  
                                            while (true)  
                                            {  
                                                size = ZipStream.Read(data, 0, data.Length);  
                                                if (size > 0)  
                                                    streamWriter.Write(data, 0, size);  
                                                else  
                                                    break;  
                                            }  
                                            streamWriter.Close();  
                                        }  
                                    }  
                                }  
                                else if (theEntry.IsDirectory)  
                                {  
                                    string strNewDirectory = @"" + baseDirectory + @"\" +   
      
    theEntry.Name;  
                                    if (!Directory.Exists(strNewDirectory))  
                                    {  
                                        Directory.CreateDirectory(strNewDirectory);  
                                    }  
                                }  
                            }  
                            ZipStream.Close();  
                        }  
                    }  
                }  
                catch (Exception ex)  
                {  
                    ret = false;  
                }  
                return ret;  
            }  
*/