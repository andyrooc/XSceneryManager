namespace XSceneryManager
{
    partial class AddScenery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbSceneryFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFileSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.ofdNewScenery = new System.Windows.Forms.OpenFileDialog();
            this.tbDestName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbZipFileDesc = new System.Windows.Forms.TextBox();
            this.lblEarthNav = new System.Windows.Forms.Label();
            this.lblInstalling = new System.Windows.Forms.Label();
            this.lblZipType = new System.Windows.Forms.Label();
            this.pbZipProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // tbSceneryFile
            // 
            this.tbSceneryFile.AllowDrop = true;
            this.tbSceneryFile.Location = new System.Drawing.Point(151, 6);
            this.tbSceneryFile.Name = "tbSceneryFile";
            this.tbSceneryFile.Size = new System.Drawing.Size(276, 20);
            this.tbSceneryFile.TabIndex = 0;
            this.tbSceneryFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbSceneryFile_DragDrop);
            this.tbSceneryFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbSceneryFile_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scenery Package (zip file):";
            // 
            // btnFileSearch
            // 
            this.btnFileSearch.Location = new System.Drawing.Point(433, 3);
            this.btnFileSearch.Name = "btnFileSearch";
            this.btnFileSearch.Size = new System.Drawing.Size(24, 23);
            this.btnFileSearch.TabIndex = 2;
            this.btnFileSearch.Text = "...";
            this.btnFileSearch.UseVisualStyleBackColor = true;
            this.btnFileSearch.Click += new System.EventHandler(this.btnFileSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(383, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Enabled = false;
            this.btnInstall.Location = new System.Drawing.Point(287, 257);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(90, 23);
            this.btnInstall.TabIndex = 4;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // ofdNewScenery
            // 
            this.ofdNewScenery.Filter = "Zip File|*.zip";
            // 
            // tbDestName
            // 
            this.tbDestName.Location = new System.Drawing.Point(151, 33);
            this.tbDestName.Name = "tbDestName";
            this.tbDestName.Size = new System.Drawing.Size(307, 20);
            this.tbDestName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name Your Package:";
            // 
            // tbZipFileDesc
            // 
            this.tbZipFileDesc.AllowDrop = true;
            this.tbZipFileDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbZipFileDesc.Location = new System.Drawing.Point(13, 63);
            this.tbZipFileDesc.Multiline = true;
            this.tbZipFileDesc.Name = "tbZipFileDesc";
            this.tbZipFileDesc.ReadOnly = true;
            this.tbZipFileDesc.Size = new System.Drawing.Size(445, 188);
            this.tbZipFileDesc.TabIndex = 7;
            this.tbZipFileDesc.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbZipFileDesc_DragDrop);
            this.tbZipFileDesc.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbZipFileDesc_DragEnter);
            // 
            // lblEarthNav
            // 
            this.lblEarthNav.AutoSize = true;
            this.lblEarthNav.Location = new System.Drawing.Point(267, 267);
            this.lblEarthNav.Name = "lblEarthNav";
            this.lblEarthNav.Size = new System.Drawing.Size(13, 13);
            this.lblEarthNav.TabIndex = 8;
            this.lblEarthNav.Text = "1";
            this.lblEarthNav.Visible = false;
            // 
            // lblInstalling
            // 
            this.lblInstalling.AutoSize = true;
            this.lblInstalling.Location = new System.Drawing.Point(15, 260);
            this.lblInstalling.Name = "lblInstalling";
            this.lblInstalling.Size = new System.Drawing.Size(51, 13);
            this.lblInstalling.TabIndex = 9;
            this.lblInstalling.Text = "Installing:";
            this.lblInstalling.Visible = false;
            // 
            // lblZipType
            // 
            this.lblZipType.AutoSize = true;
            this.lblZipType.Location = new System.Drawing.Point(273, 270);
            this.lblZipType.Name = "lblZipType";
            this.lblZipType.Size = new System.Drawing.Size(0, 13);
            this.lblZipType.TabIndex = 10;
            this.lblZipType.Visible = false;
            // 
            // pbZipProgress
            // 
            this.pbZipProgress.Location = new System.Drawing.Point(67, 255);
            this.pbZipProgress.Name = "pbZipProgress";
            this.pbZipProgress.Size = new System.Drawing.Size(167, 23);
            this.pbZipProgress.TabIndex = 11;
            this.pbZipProgress.Visible = false;
            // 
            // AddScenery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 292);
            this.Controls.Add(this.pbZipProgress);
            this.Controls.Add(this.lblZipType);
            this.Controls.Add(this.lblInstalling);
            this.Controls.Add(this.lblEarthNav);
            this.Controls.Add(this.tbZipFileDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDestName);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFileSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSceneryFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddScenery";
            this.ShowInTaskbar = false;
            this.Text = "Add Scenery";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSceneryFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFileSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.OpenFileDialog ofdNewScenery;
        private System.Windows.Forms.TextBox tbDestName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbZipFileDesc;
        private System.Windows.Forms.Label lblEarthNav;
        private System.Windows.Forms.Label lblInstalling;
        private System.Windows.Forms.Label lblZipType;
        private System.Windows.Forms.ProgressBar pbZipProgress;
    }
}