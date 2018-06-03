namespace XSceneryManager
{
    partial class CustomLib
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
            this.dgvCustomLibs = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomLibs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCustomLibs
            // 
            this.dgvCustomLibs.AllowUserToAddRows = false;
            this.dgvCustomLibs.AllowUserToDeleteRows = false;
            this.dgvCustomLibs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomLibs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.path});
            this.dgvCustomLibs.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomLibs.Name = "dgvCustomLibs";
            this.dgvCustomLibs.ReadOnly = true;
            this.dgvCustomLibs.RowTemplate.Height = 24;
            this.dgvCustomLibs.Size = new System.Drawing.Size(613, 269);
            this.dgvCustomLibs.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(504, 275);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 31);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // path
            // 
            this.path.DataPropertyName = "path";
            this.path.Frozen = true;
            this.path.HeaderText = "Library Path";
            this.path.MinimumWidth = 400;
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Width = 550;
            // 
            // CustomLib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 313);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvCustomLibs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomLib";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Installed Libraries";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomLibs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCustomLibs;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
    }
}