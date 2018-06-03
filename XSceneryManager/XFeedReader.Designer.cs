namespace XSceneryManager
{
    partial class XFeedReader
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
            this.lvRSS = new System.Windows.Forms.ListView();
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.link = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSave = new System.Windows.Forms.Button();
            this.bgwRSS = new System.ComponentModel.BackgroundWorker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbxThrobber = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxThrobber)).BeginInit();
            this.SuspendLayout();
            // 
            // lvRSS
            // 
            this.lvRSS.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvRSS.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvRSS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRSS.CheckBoxes = true;
            this.lvRSS.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.status,
            this.name,
            this.link});
            this.lvRSS.FullRowSelect = true;
            this.lvRSS.HotTracking = true;
            this.lvRSS.HoverSelection = true;
            this.lvRSS.Location = new System.Drawing.Point(0, 0);
            this.lvRSS.Name = "lvRSS";
            this.lvRSS.Size = new System.Drawing.Size(562, 293);
            this.lvRSS.TabIndex = 0;
            this.lvRSS.UseCompatibleStateImageBehavior = false;
            this.lvRSS.View = System.Windows.Forms.View.Details;
            this.lvRSS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRSS_MouseDoubleClick);
            // 
            // status
            // 
            this.status.Text = "";
            this.status.Width = 20;
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 213;
            // 
            // link
            // 
            this.link.Text = "Link";
            this.link.Width = 307;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(394, 299);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // bgwRSS
            // 
            this.bgwRSS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRSS_DoWork);
            this.bgwRSS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRSS_RunWorkerCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(475, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbxThrobber
            // 
            this.pbxThrobber.Image = global::XSceneryManager.Properties.Resources.throbber_fast;
            this.pbxThrobber.Location = new System.Drawing.Point(3, 294);
            this.pbxThrobber.Name = "pbxThrobber";
            this.pbxThrobber.Size = new System.Drawing.Size(36, 31);
            this.pbxThrobber.TabIndex = 3;
            this.pbxThrobber.TabStop = false;
            this.pbxThrobber.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(65, 296);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(325, 31);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Blue highlighted lines are new entries. Potential airport scenery packages are ch" +
    "ecked automatically. Uncheck to remove.";
            // 
            // XFeedReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 326);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pbxThrobber);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lvRSS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "XFeedReader";
            this.ShowInTaskbar = false;
            this.Text = "FeedReader";
            this.Load += new System.EventHandler(this.FeedReader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxThrobber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvRSS;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader link;
        private System.Windows.Forms.Button btnSave;
        private System.ComponentModel.BackgroundWorker bgwRSS;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbxThrobber;
        private System.Windows.Forms.TextBox textBox1;
    }
}