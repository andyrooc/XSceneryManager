namespace XSceneryManager
{
    partial class DSFBrowser
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
            this.dgvDSF = new System.Windows.Forms.DataGridView();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSF)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDSF
            // 
            this.dgvDSF.AllowUserToAddRows = false;
            this.dgvDSF.AllowUserToDeleteRows = false;
            this.dgvDSF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.path,
            this.dsf});
            this.dgvDSF.Location = new System.Drawing.Point(2, 2);
            this.dgvDSF.Name = "dgvDSF";
            this.dgvDSF.RowTemplate.Height = 24;
            this.dgvDSF.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDSF.Size = new System.Drawing.Size(825, 285);
            this.dgvDSF.TabIndex = 0;
            // 
            // path
            // 
            this.path.DataPropertyName = "path";
            this.path.HeaderText = "Path";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Width = 550;
            // 
            // dsf
            // 
            this.dsf.DataPropertyName = "dsffile";
            this.dsf.HeaderText = "DSF";
            this.dsf.Name = "dsf";
            this.dsf.ReadOnly = true;
            this.dsf.Width = 200;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(698, 293);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(119, 29);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(13, 293);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(146, 29);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export KML";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // DSFBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 326);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvDSF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DSFBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DSF Browser";
            this.Load += new System.EventHandler(this.DSFBrowser_Load);
            this.Resize += new System.EventHandler(this.DSFBrowser_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDSF;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.DataGridViewTextBoxColumn dsf;
        private System.Windows.Forms.Button btnExport;
    }
}