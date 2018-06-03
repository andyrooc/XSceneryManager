namespace XSceneryManager
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvAirports = new System.Windows.Forms.DataGridView();
            this.enabled = new System.Windows.Forms.DataGridViewImageColumn();
            this.icao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elevation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showOnOpenStreetMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnBingMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skyVectorChartsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.metarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flightStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldAeroDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openThisFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwCustom = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installNewSceneryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSSFeedsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportKMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setXPlaneBasePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageLoadOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.librariesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.broseCustomDSFsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDuplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDisabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGlobalAirportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelScan = new System.Windows.Forms.Button();
            this.pbCustom = new System.Windows.Forms.ProgressBar();
            this.lblCustom = new System.Windows.Forms.Label();
            this.rtbProperties = new System.Windows.Forms.RichTextBox();
            this.btnExpand = new System.Windows.Forms.Button();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.createGNS430FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirports)).BeginInit();
            this.cmsRightClick.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAirports
            // 
            this.dgvAirports.AllowDrop = true;
            this.dgvAirports.AllowUserToAddRows = false;
            this.dgvAirports.AllowUserToDeleteRows = false;
            this.dgvAirports.AllowUserToOrderColumns = true;
            this.dgvAirports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAirports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAirports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabled,
            this.icao,
            this.name,
            this.elevation,
            this.latitude,
            this.longitude,
            this.path,
            this.id});
            this.dgvAirports.ContextMenuStrip = this.cmsRightClick;
            this.dgvAirports.Location = new System.Drawing.Point(4, 33);
            this.dgvAirports.Margin = new System.Windows.Forms.Padding(4);
            this.dgvAirports.Name = "dgvAirports";
            this.dgvAirports.ReadOnly = true;
            this.dgvAirports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAirports.Size = new System.Drawing.Size(969, 404);
            this.dgvAirports.TabIndex = 2;
            this.dgvAirports.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAirports_CellFormatting);
            this.dgvAirports.SelectionChanged += new System.EventHandler(this.dgvAirports_SelectionChanged);
            this.dgvAirports.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvAirports_DragDrop);
            this.dgvAirports.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvAirports_DragEnter);
            this.dgvAirports.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAirports_KeyDown);
            this.dgvAirports.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvAirports_MouseDown);
            // 
            // enabled
            // 
            this.enabled.DataPropertyName = "enabled";
            this.enabled.HeaderText = "";
            this.enabled.Image = global::XSceneryManager.Properties.Resources.enabled;
            this.enabled.Name = "enabled";
            this.enabled.ReadOnly = true;
            this.enabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.enabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.enabled.Width = 19;
            // 
            // icao
            // 
            this.icao.DataPropertyName = "icao";
            this.icao.HeaderText = "ICAO";
            this.icao.Name = "icao";
            this.icao.ReadOnly = true;
            this.icao.Width = 65;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "Aiport Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 111;
            // 
            // elevation
            // 
            this.elevation.DataPropertyName = "elevation";
            this.elevation.HeaderText = "Elevation";
            this.elevation.Name = "elevation";
            this.elevation.ReadOnly = true;
            this.elevation.Width = 91;
            // 
            // latitude
            // 
            this.latitude.DataPropertyName = "latitude";
            this.latitude.HeaderText = "Latitude";
            this.latitude.Name = "latitude";
            this.latitude.ReadOnly = true;
            this.latitude.Width = 84;
            // 
            // longitude
            // 
            this.longitude.DataPropertyName = "longitude";
            this.longitude.HeaderText = "Longitude";
            this.longitude.Name = "longitude";
            this.longitude.ReadOnly = true;
            this.longitude.Width = 96;
            // 
            // path
            // 
            this.path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.path.DataPropertyName = "path";
            this.path.HeaderText = "Package";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 40;
            // 
            // cmsRightClick
            // 
            this.cmsRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showOnOpenStreetMapsToolStripMenuItem,
            this.showOnMapToolStripMenuItem,
            this.showOnBingMapToolStripMenuItem,
            this.skyVectorChartsToolStripMenuItem1,
            this.metarToolStripMenuItem,
            this.flightStatsToolStripMenuItem,
            this.worldAeroDataToolStripMenuItem,
            this.openThisFolderToolStripMenuItem,
            this.toolStripMenuItem2,
            this.disableToolStripMenuItem});
            this.cmsRightClick.Name = "cmsRightClick";
            this.cmsRightClick.Size = new System.Drawing.Size(259, 226);
            this.cmsRightClick.Opening += new System.ComponentModel.CancelEventHandler(this.cmsRightClick_Opening);
            // 
            // showOnOpenStreetMapsToolStripMenuItem
            // 
            this.showOnOpenStreetMapsToolStripMenuItem.Name = "showOnOpenStreetMapsToolStripMenuItem";
            this.showOnOpenStreetMapsToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.showOnOpenStreetMapsToolStripMenuItem.Text = "Show on &Open Street Maps";
            this.showOnOpenStreetMapsToolStripMenuItem.Click += new System.EventHandler(this.showOnOpenStreetMapsToolStripMenuItem_Click);
            // 
            // showOnMapToolStripMenuItem
            // 
            this.showOnMapToolStripMenuItem.Name = "showOnMapToolStripMenuItem";
            this.showOnMapToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.showOnMapToolStripMenuItem.Text = "Show on &Google Map";
            this.showOnMapToolStripMenuItem.Click += new System.EventHandler(this.showOnMapToolStripMenuItem_Click);
            // 
            // showOnBingMapToolStripMenuItem
            // 
            this.showOnBingMapToolStripMenuItem.Name = "showOnBingMapToolStripMenuItem";
            this.showOnBingMapToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.showOnBingMapToolStripMenuItem.Text = "Show on &Bing Map";
            this.showOnBingMapToolStripMenuItem.Click += new System.EventHandler(this.showOnBingMapToolStripMenuItem_Click);
            // 
            // skyVectorChartsToolStripMenuItem1
            // 
            this.skyVectorChartsToolStripMenuItem1.Name = "skyVectorChartsToolStripMenuItem1";
            this.skyVectorChartsToolStripMenuItem1.Size = new System.Drawing.Size(258, 24);
            this.skyVectorChartsToolStripMenuItem1.Text = "&SkyVector Charts";
            this.skyVectorChartsToolStripMenuItem1.Click += new System.EventHandler(this.skyVectorChartsToolStripMenuItem1_Click);
            // 
            // metarToolStripMenuItem
            // 
            this.metarToolStripMenuItem.Name = "metarToolStripMenuItem";
            this.metarToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.metarToolStripMenuItem.Text = "&Metar";
            this.metarToolStripMenuItem.Click += new System.EventHandler(this.metarToolStripMenuItem_Click);
            // 
            // flightStatsToolStripMenuItem
            // 
            this.flightStatsToolStripMenuItem.Name = "flightStatsToolStripMenuItem";
            this.flightStatsToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.flightStatsToolStripMenuItem.Text = "&FlightStats";
            this.flightStatsToolStripMenuItem.Click += new System.EventHandler(this.flightStatsToolStripMenuItem_Click);
            // 
            // worldAeroDataToolStripMenuItem
            // 
            this.worldAeroDataToolStripMenuItem.Name = "worldAeroDataToolStripMenuItem";
            this.worldAeroDataToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.worldAeroDataToolStripMenuItem.Text = "&World Aero Data";
            this.worldAeroDataToolStripMenuItem.Click += new System.EventHandler(this.worldAeroDataToolStripMenuItem_Click);
            // 
            // openThisFolderToolStripMenuItem
            // 
            this.openThisFolderToolStripMenuItem.Name = "openThisFolderToolStripMenuItem";
            this.openThisFolderToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.openThisFolderToolStripMenuItem.Text = "Open This Folder";
            this.openThisFolderToolStripMenuItem.Click += new System.EventHandler(this.openThisFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(255, 6);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(258, 24);
            this.disableToolStripMenuItem.Text = "Disable";
            this.disableToolStripMenuItem.Click += new System.EventHandler(this.disableToolStripMenuItem_Click);
            // 
            // bgwCustom
            // 
            this.bgwCustom.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCustom_DoWork);
            this.bgwCustom.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCustom_ProgressChanged);
            this.bgwCustom.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCustom_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1180, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installNewSceneryToolStripMenuItem,
            this.rSSFeedsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exportKMLToolStripMenuItem,
            this.createGNS430FileToolStripMenuItem,
            this.scanToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.scanToolStripMenuItem.Text = "&File";
            // 
            // installNewSceneryToolStripMenuItem
            // 
            this.installNewSceneryToolStripMenuItem.Name = "installNewSceneryToolStripMenuItem";
            this.installNewSceneryToolStripMenuItem.Size = new System.Drawing.Size(266, 24);
            this.installNewSceneryToolStripMenuItem.Text = "Install New Scenery";
            this.installNewSceneryToolStripMenuItem.Click += new System.EventHandler(this.installNewSceneryToolStripMenuItem_Click);
            // 
            // rSSFeedsToolStripMenuItem
            // 
            this.rSSFeedsToolStripMenuItem.Name = "rSSFeedsToolStripMenuItem";
            this.rSSFeedsToolStripMenuItem.Size = new System.Drawing.Size(266, 24);
            this.rSSFeedsToolStripMenuItem.Text = "RSS Feeds";
            this.rSSFeedsToolStripMenuItem.Click += new System.EventHandler(this.rSSFeedsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(263, 6);
            // 
            // exportKMLToolStripMenuItem
            // 
            this.exportKMLToolStripMenuItem.Name = "exportKMLToolStripMenuItem";
            this.exportKMLToolStripMenuItem.Size = new System.Drawing.Size(266, 24);
            this.exportKMLToolStripMenuItem.Text = "Export KML To Google Earth";
            this.exportKMLToolStripMenuItem.Click += new System.EventHandler(this.exportKMLToolStripMenuItem_Click);
            // 
            // scanToolStripMenuItem1
            // 
            this.scanToolStripMenuItem1.Name = "scanToolStripMenuItem1";
            this.scanToolStripMenuItem1.Size = new System.Drawing.Size(266, 24);
            this.scanToolStripMenuItem1.Text = "Scan Custom Scenery";
            this.scanToolStripMenuItem1.Click += new System.EventHandler(this.scanToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(263, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(266, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setXPlaneBasePathToolStripMenuItem,
            this.manageLoadOrderToolStripMenuItem,
            this.toolStripMenuItem5,
            this.librariesToolStripMenuItem,
            this.broseCustomDSFsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // setXPlaneBasePathToolStripMenuItem
            // 
            this.setXPlaneBasePathToolStripMenuItem.Name = "setXPlaneBasePathToolStripMenuItem";
            this.setXPlaneBasePathToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.setXPlaneBasePathToolStripMenuItem.Text = "Set X-Plane Base Path";
            this.setXPlaneBasePathToolStripMenuItem.Click += new System.EventHandler(this.setXPlaneBasePathToolStripMenuItem_Click);
            // 
            // manageLoadOrderToolStripMenuItem
            // 
            this.manageLoadOrderToolStripMenuItem.Name = "manageLoadOrderToolStripMenuItem";
            this.manageLoadOrderToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.manageLoadOrderToolStripMenuItem.Text = "&Manage Load Order";
            this.manageLoadOrderToolStripMenuItem.Click += new System.EventHandler(this.manageLoadOrderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(219, 6);
            // 
            // librariesToolStripMenuItem
            // 
            this.librariesToolStripMenuItem.Name = "librariesToolStripMenuItem";
            this.librariesToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.librariesToolStripMenuItem.Text = "&Libraries";
            this.librariesToolStripMenuItem.Click += new System.EventHandler(this.librariesToolStripMenuItem_Click);
            // 
            // broseCustomDSFsToolStripMenuItem
            // 
            this.broseCustomDSFsToolStripMenuItem.Name = "broseCustomDSFsToolStripMenuItem";
            this.broseCustomDSFsToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.broseCustomDSFsToolStripMenuItem.Text = "&Browse Custom DSFs";
            this.broseCustomDSFsToolStripMenuItem.Click += new System.EventHandler(this.browseCustomDSFsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDuplicatesToolStripMenuItem,
            this.showDisabledToolStripMenuItem,
            this.showGlobalAirportsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // showDuplicatesToolStripMenuItem
            // 
            this.showDuplicatesToolStripMenuItem.CheckOnClick = true;
            this.showDuplicatesToolStripMenuItem.Name = "showDuplicatesToolStripMenuItem";
            this.showDuplicatesToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.showDuplicatesToolStripMenuItem.Text = "Show Duplicates";
            this.showDuplicatesToolStripMenuItem.Click += new System.EventHandler(this.showDuplicatesToolStripMenuItem_Click);
            // 
            // showDisabledToolStripMenuItem
            // 
            this.showDisabledToolStripMenuItem.Checked = true;
            this.showDisabledToolStripMenuItem.CheckOnClick = true;
            this.showDisabledToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showDisabledToolStripMenuItem.Name = "showDisabledToolStripMenuItem";
            this.showDisabledToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.showDisabledToolStripMenuItem.Text = "Show Disabled";
            this.showDisabledToolStripMenuItem.Click += new System.EventHandler(this.showDisabledToolStripMenuItem_Click);
            // 
            // showGlobalAirportsToolStripMenuItem
            // 
            this.showGlobalAirportsToolStripMenuItem.Checked = true;
            this.showGlobalAirportsToolStripMenuItem.CheckOnClick = true;
            this.showGlobalAirportsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGlobalAirportsToolStripMenuItem.Name = "showGlobalAirportsToolStripMenuItem";
            this.showGlobalAirportsToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.showGlobalAirportsToolStripMenuItem.Text = "Show Global Airports";
            this.showGlobalAirportsToolStripMenuItem.Click += new System.EventHandler(this.showGlobalAirportsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(119, 24);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // btnCancelScan
            // 
            this.btnCancelScan.Location = new System.Drawing.Point(596, 444);
            this.btnCancelScan.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelScan.Name = "btnCancelScan";
            this.btnCancelScan.Size = new System.Drawing.Size(72, 28);
            this.btnCancelScan.TabIndex = 9;
            this.btnCancelScan.Text = "Cancel";
            this.btnCancelScan.UseVisualStyleBackColor = true;
            this.btnCancelScan.Visible = false;
            this.btnCancelScan.Click += new System.EventHandler(this.btnCancelScan_Click);
            // 
            // pbCustom
            // 
            this.pbCustom.Location = new System.Drawing.Point(320, 444);
            this.pbCustom.Margin = new System.Windows.Forms.Padding(4);
            this.pbCustom.Name = "pbCustom";
            this.pbCustom.Size = new System.Drawing.Size(268, 28);
            this.pbCustom.TabIndex = 3;
            this.pbCustom.Visible = false;
            // 
            // lblCustom
            // 
            this.lblCustom.AutoSize = true;
            this.lblCustom.Location = new System.Drawing.Point(1, 450);
            this.lblCustom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustom.Name = "lblCustom";
            this.lblCustom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCustom.Size = new System.Drawing.Size(174, 17);
            this.lblCustom.TabIndex = 4;
            this.lblCustom.Text = "Scanning Custom Scenery";
            // 
            // rtbProperties
            // 
            this.rtbProperties.BackColor = System.Drawing.Color.Black;
            this.rtbProperties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbProperties.ForeColor = System.Drawing.Color.White;
            this.rtbProperties.Location = new System.Drawing.Point(975, 33);
            this.rtbProperties.Margin = new System.Windows.Forms.Padding(4);
            this.rtbProperties.Name = "rtbProperties";
            this.rtbProperties.ReadOnly = true;
            this.rtbProperties.Size = new System.Drawing.Size(205, 404);
            this.rtbProperties.TabIndex = 12;
            this.rtbProperties.Text = "";
            this.rtbProperties.Visible = false;
            // 
            // btnExpand
            // 
            this.btnExpand.Location = new System.Drawing.Point(940, 2);
            this.btnExpand.Margin = new System.Windows.Forms.Padding(4);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(29, 27);
            this.btnExpand.TabIndex = 13;
            this.btnExpand.Text = "+";
            this.btnExpand.UseVisualStyleBackColor = true;
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(784, 6);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(149, 22);
            this.tbFilter.TabIndex = 14;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lblFilter.Location = new System.Drawing.Point(735, 9);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(43, 17);
            this.lblFilter.TabIndex = 15;
            this.lblFilter.Text = "Filter:";
            // 
            // createGNS430FileToolStripMenuItem
            // 
            this.createGNS430FileToolStripMenuItem.Name = "createGNS430FileToolStripMenuItem";
            this.createGNS430FileToolStripMenuItem.Size = new System.Drawing.Size(266, 24);
            this.createGNS430FileToolStripMenuItem.Text = "Create GNS430 file";
            this.createGNS430FileToolStripMenuItem.Click += new System.EventHandler(this.createGNS430FileToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 481);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.btnExpand);
            this.Controls.Add(this.rtbProperties);
            this.Controls.Add(this.pbCustom);
            this.Controls.Add(this.btnCancelScan);
            this.Controls.Add(this.lblCustom);
            this.Controls.Add(this.dgvAirports);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "XSceneryManager";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirports)).EndInit();
            this.cmsRightClick.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAirports;
        private System.ComponentModel.BackgroundWorker bgwCustom;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setXPlaneBasePathToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsRightClick;
        private System.Windows.Forms.ToolStripMenuItem showOnMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnBingMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flightStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem worldAeroDataToolStripMenuItem;
        private System.Windows.Forms.ProgressBar pbCustom;
        private System.Windows.Forms.Button btnCancelScan;
        private System.Windows.Forms.Label lblCustom;
        private System.Windows.Forms.ToolStripMenuItem exportKMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installNewSceneryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem rSSFeedsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skyVectorChartsToolStripMenuItem1;
        private System.Windows.Forms.RichTextBox rtbProperties;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.ToolStripMenuItem manageLoadOrderToolStripMenuItem;
        private System.Windows.Forms.DataGridViewImageColumn enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn icao;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn elevation;
        private System.Windows.Forms.DataGridViewTextBoxColumn latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDuplicatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDisabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGlobalAirportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openThisFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnOpenStreetMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem librariesToolStripMenuItem;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem broseCustomDSFsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createGNS430FileToolStripMenuItem;
    }
}

