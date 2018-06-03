namespace XSceneryManager
{
    partial class LoadOrder
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
            this.dgvLoadOrder = new System.Windows.Forms.DataGridView();
            this.enabled = new System.Windows.Forms.DataGridViewImageColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsLoadOrder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveToTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.enableToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadOrder)).BeginInit();
            this.cmsLoadOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLoadOrder
            // 
            this.dgvLoadOrder.AllowDrop = true;
            this.dgvLoadOrder.AllowUserToAddRows = false;
            this.dgvLoadOrder.AllowUserToDeleteRows = false;
            this.dgvLoadOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoadOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabled,
            this.type,
            this.pack});
            this.dgvLoadOrder.ContextMenuStrip = this.cmsLoadOrder;
            this.dgvLoadOrder.Location = new System.Drawing.Point(16, 15);
            this.dgvLoadOrder.Margin = new System.Windows.Forms.Padding(4);
            this.dgvLoadOrder.Name = "dgvLoadOrder";
            this.dgvLoadOrder.ReadOnly = true;
            this.dgvLoadOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoadOrder.ShowCellToolTips = false;
            this.dgvLoadOrder.Size = new System.Drawing.Size(817, 277);
            this.dgvLoadOrder.TabIndex = 0;
            this.dgvLoadOrder.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLoadOrder_CellFormatting);
            this.dgvLoadOrder.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvLoadOrder_DragDrop);
            this.dgvLoadOrder.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvLoadOrder_DragOver);
            this.dgvLoadOrder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvLoadOrder_MouseDown);
            this.dgvLoadOrder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvLoadOrder_MouseMove);
            // 
            // enabled
            // 
            this.enabled.DataPropertyName = "enabled";
            this.enabled.HeaderText = "Status";
            this.enabled.Image = global::XSceneryManager.Properties.Resources.enabled;
            this.enabled.Name = "enabled";
            this.enabled.ReadOnly = true;
            this.enabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.enabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.enabled.Width = 50;
            // 
            // type
            // 
            this.type.DataPropertyName = "state";
            this.type.HeaderText = "State";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Visible = false;
            // 
            // pack
            // 
            this.pack.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pack.DataPropertyName = "path";
            this.pack.HeaderText = "Scenery Pack";
            this.pack.Name = "pack";
            this.pack.ReadOnly = true;
            // 
            // cmsLoadOrder
            // 
            this.cmsLoadOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToTopToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.moveToBottomToolStripMenuItem,
            this.toolStripMenuItem1,
            this.enableToolStripMenuItem1});
            this.cmsLoadOrder.Name = "cmsLoadOrder";
            this.cmsLoadOrder.Size = new System.Drawing.Size(188, 130);
            this.cmsLoadOrder.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLoadOrder_Opening);
            // 
            // moveToTopToolStripMenuItem
            // 
            this.moveToTopToolStripMenuItem.Name = "moveToTopToolStripMenuItem";
            this.moveToTopToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.moveToTopToolStripMenuItem.Text = "Move to Top";
            this.moveToTopToolStripMenuItem.Click += new System.EventHandler(this.moveToTopToolStripMenuItem_Click);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // moveToBottomToolStripMenuItem
            // 
            this.moveToBottomToolStripMenuItem.Name = "moveToBottomToolStripMenuItem";
            this.moveToBottomToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.moveToBottomToolStripMenuItem.Text = "Move to Bottom";
            this.moveToBottomToolStripMenuItem.Click += new System.EventHandler(this.moveToBottomToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 6);
            // 
            // enableToolStripMenuItem1
            // 
            this.enableToolStripMenuItem1.Name = "enableToolStripMenuItem1";
            this.enableToolStripMenuItem1.Size = new System.Drawing.Size(187, 24);
            this.enableToolStripMenuItem1.Text = "Enable";
            this.enableToolStripMenuItem1.Click += new System.EventHandler(this.enableToolStripMenuItem1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(733, 299);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(625, 299);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(17, 299);
            this.btnPlus.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(49, 28);
            this.btnPlus.TabIndex = 3;
            this.btnPlus.Tag = "Move Up";
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(75, 299);
            this.btnMinus.Margin = new System.Windows.Forms.Padding(4);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(47, 28);
            this.btnMinus.TabIndex = 4;
            this.btnMinus.Tag = "Move Down";
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // LoadOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 335);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvLoadOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoadOrder";
            this.ShowInTaskbar = false;
            this.Text = "LoadOrder";
            this.Load += new System.EventHandler(this.LoadOrder_Load);
            this.Resize += new System.EventHandler(this.LoadOrder_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadOrder)).EndInit();
            this.cmsLoadOrder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLoadOrder;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.ContextMenuStrip cmsLoadOrder;
        private System.Windows.Forms.ToolStripMenuItem moveToTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem1;
        private System.Windows.Forms.DataGridViewImageColumn enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn pack;
    }
}