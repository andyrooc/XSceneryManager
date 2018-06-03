using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XSceneryManager.Properties;

namespace XSceneryManager
{
    public partial class LoadOrder : Form
    {
        private SceneryPacks load;
        private DataTable dt = new DataTable();
 
        public LoadOrder()
        {
            InitializeComponent();
        }

        private void LoadOrder_Load(object sender, EventArgs e)
        {
            load = new SceneryPacks(Settings.Default.XPlaneFolder);
            dt = load.GetSceneryPacks();

            dgvLoadOrder.AutoGenerateColumns = false;
            dgvLoadOrder.DataSource = dt;
        }

        private void dgvLoadOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if enabled, show green image
            if (e.ColumnIndex == 0)
            {
                if (((bool)dgvLoadOrder.Rows[e.RowIndex].Cells["enabled"].Value) == true)
                    e.Value = Resources.enabled;
                else
                {
                    e.Value = Resources.disabled;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            load.SavePacks((DataTable)dgvLoadOrder.DataSource);

            this.Close();
        }

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private void dgvLoadOrder_MouseDown(object sender, MouseEventArgs e)
        {
            dgvLoadOrder.ClearSelection();
            if (dgvLoadOrder.HitTest(e.Location.X, e.Location.Y).RowIndex >= 0)
            {
                int idx = dgvLoadOrder.HitTest(e.Location.X, e.Location.Y).RowIndex;
                dgvLoadOrder.Rows[idx].Selected = true;

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    //Set context menu item
                    if (((bool)dgvLoadOrder.Rows[idx].Cells["enabled"].Value) == true)
                        enableToolStripMenuItem1.Text = "Disable";
                    else
                        enableToolStripMenuItem1.Text = "Enable";
                }
                else
                {
                    // Get the index of the item the mouse is below.
                    rowIndexFromMouseDown = dgvLoadOrder.HitTest(e.X, e.Y).RowIndex;
                    if (rowIndexFromMouseDown != -1)
                    {
                        // Remember the point where the mouse down occurred. 
                        // The DragSize indicates the size that the mouse can move 
                        // before a drag event should be started.                
                        Size dragSize = SystemInformation.DragSize;
                        Size rowSize = new Size(dgvLoadOrder.Width,dgvLoadOrder.Rows[idx].Height);

                        // Create a rectangle using the DragSize, with the mouse position being
                        // at the center of the rectangle.
                        dragBoxFromMouseDown = new Rectangle(new Point(e.X - (rowSize.Width / 2),
                                                                       e.Y - (rowSize.Height / 2)),
                                            dragSize);
                    }
                    else
                        // Reset the rectangle if the mouse is not over an item in the ListBox.
                        dragBoxFromMouseDown = Rectangle.Empty;
                }
            }
        }

        private void cmsLoadOrder_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLoadOrder.SelectedRows.Count < 1)
                e.Cancel = true;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            MoveUp();
        }


        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void moveToTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLoadOrder.SelectedRows.Count != 1 && dgvLoadOrder.SelectedCells.Count > 0)
            {
                dgvLoadOrder.Rows[dgvLoadOrder.SelectedCells[0].RowIndex].Selected = true;
            }

            if (dgvLoadOrder.SelectedRows.Count == 1)
            {
                int idx = dgvLoadOrder.SelectedRows[0].Index;

                DataRow rw = dt.NewRow();
                rw["state"] = dt.Rows[idx]["state"];
                rw["path"] = dt.Rows[idx]["path"];
                rw["enabled"] = dt.Rows[idx]["enabled"];

                if (idx > 0)
                {
                    dt.Rows.RemoveAt(idx);
                    dt.Rows.InsertAt(rw, 0);

                    dgvLoadOrder.ClearSelection();
                    dgvLoadOrder.Rows[0].Selected = true;
                }
            }
        }

        private void moveToBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLoadOrder.SelectedRows.Count != 1 && dgvLoadOrder.SelectedCells.Count > 0)
            {
                dgvLoadOrder.Rows[dgvLoadOrder.SelectedCells[0].RowIndex].Selected = true;
            }

            if (dgvLoadOrder.SelectedRows.Count == 1)
            {
                int idx = dgvLoadOrder.SelectedRows[0].Index;

                DataRow rw = dt.NewRow();
                rw["state"] = dt.Rows[idx]["state"];
                rw["path"] = dt.Rows[idx]["path"];
                rw["enabled"] = dt.Rows[idx]["enabled"];

                if (idx < dt.Rows.Count - 1)
                {
                    dt.Rows.RemoveAt(idx);
                    dt.Rows.InsertAt(rw, dt.Rows.Count);

                    dgvLoadOrder.ClearSelection();
                    dgvLoadOrder.Rows[dt.Rows.Count - 1].Selected = true;
                }
            }
        }

        private void MoveUp()
        {
            if (dgvLoadOrder.SelectedRows.Count != 1 && dgvLoadOrder.SelectedCells.Count > 0)
            {
                dgvLoadOrder.Rows[dgvLoadOrder.SelectedCells[0].RowIndex].Selected = true;
            }

            if (dgvLoadOrder.SelectedRows.Count == 1)
            {
                int idx = dgvLoadOrder.SelectedRows[0].Index;

                DataRow rw = dt.NewRow();
                rw["state"] = dt.Rows[idx]["state"];
                rw["path"] = dt.Rows[idx]["path"];
                rw["enabled"] = dt.Rows[idx]["enabled"];

                if (idx > 0)
                {
                    dt.Rows.RemoveAt(idx);
                    dt.Rows.InsertAt(rw, idx-1);

                    dgvLoadOrder.ClearSelection();
                    dgvLoadOrder.Rows[idx - 1].Selected = true;
                }

                if (dgvLoadOrder.SelectedCells[0].RowIndex <= dgvLoadOrder.FirstDisplayedScrollingRowIndex)
                {
                    dgvLoadOrder.FirstDisplayedScrollingRowIndex = dgvLoadOrder.SelectedCells[0].RowIndex;
                    dgvLoadOrder.Refresh();
                }
                
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void MoveDown()
        {
            if (dgvLoadOrder.SelectedRows.Count != 1 && dgvLoadOrder.SelectedCells.Count > 0)
            {
                dgvLoadOrder.Rows[dgvLoadOrder.SelectedCells[0].RowIndex].Selected = true;
            }

            if (dgvLoadOrder.SelectedRows.Count == 1)
            {
                int idx = dgvLoadOrder.SelectedRows[0].Index;
                DataRow rw = dt.NewRow();
                rw["state"] = dt.Rows[idx]["state"];
                rw["path"] = dt.Rows[idx]["path"];
                rw["enabled"] = dt.Rows[idx]["enabled"];

                if (idx < dgvLoadOrder.Rows.Count - 1)
                {
                    dt.Rows.RemoveAt(idx);
                    dt.Rows.InsertAt(rw, idx + 1);
                    dgvLoadOrder.ClearSelection();
                    dgvLoadOrder.Rows[idx + 1].Selected = true;
                }

                //scroll into view
                if (dgvLoadOrder.SelectedCells[0].RowIndex > dgvLoadOrder.FirstDisplayedScrollingRowIndex + dgvLoadOrder.DisplayedRowCount(true) - 1)
                {
                    dgvLoadOrder.FirstDisplayedScrollingRowIndex += 1;
                    dgvLoadOrder.Refresh();
                }
            }
        }

        private void enableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int idx = dgvLoadOrder.SelectedRows[0].Index;

            if (idx >= 0)
            {
                if (((bool) dt.Rows[idx]["enabled"]) == true)
                {
                    dt.Rows[idx]["enabled"] = false;
                    dt.Rows[idx]["state"] = SceneryPacks.DISABLED;
                }
                else
                {
                    dt.Rows[idx]["enabled"] = true;
                    dt.Rows[idx]["state"] = SceneryPacks.ENABLED;
                }
            }
        }

        private void dgvLoadOrder_MouseMove(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dgvLoadOrder.DoDragDrop(
                    dgvLoadOrder.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }

        }

        int lastDragOverRow = 0;
        private void dgvLoadOrder_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            Point clientPoint = dgvLoadOrder.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            if (lastDragOverRow >= 0 && lastDragOverRow < dgvLoadOrder.Rows.Count)
                dgvLoadOrder.Rows[lastDragOverRow].DividerHeight = 0;
            lastDragOverRow =
                dgvLoadOrder.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (lastDragOverRow >=0 && lastDragOverRow < dgvLoadOrder.Rows.Count)
                dgvLoadOrder.Rows[lastDragOverRow].DividerHeight = 2;

            if (lastDragOverRow == (dgvLoadOrder.FirstDisplayedScrollingRowIndex + dgvLoadOrder.DisplayedRowCount(true) - 1) &&
                            (dgvLoadOrder.FirstDisplayedScrollingRowIndex + dgvLoadOrder.DisplayedRowCount(true) - 1) < dgvLoadOrder.RowCount)
            {
                dgvLoadOrder.FirstDisplayedScrollingRowIndex += 1;
                dgvLoadOrder.Refresh();
            }
            else if (lastDragOverRow < dgvLoadOrder.FirstDisplayedScrollingRowIndex && dgvLoadOrder.FirstDisplayedScrollingRowIndex > 0)
            {
                dgvLoadOrder.FirstDisplayedScrollingRowIndex -= 1;
                dgvLoadOrder.Refresh();
            }
        }

        private void dgvLoadOrder_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dgvLoadOrder.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dgvLoadOrder.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                if (lastDragOverRow >= 0 && lastDragOverRow < dgvLoadOrder.Rows.Count)
                    dgvLoadOrder.Rows[lastDragOverRow].DividerHeight = 0;
                if (rowIndexFromMouseDown < rowIndexOfItemUnderMouseToDrop)
                {
                    for (int i=rowIndexFromMouseDown; i < rowIndexOfItemUnderMouseToDrop; i++)
                        MoveDown();
                }
                else
                {
                    if (rowIndexFromMouseDown > rowIndexOfItemUnderMouseToDrop)
                    {
                        for (int i = rowIndexFromMouseDown; i > rowIndexOfItemUnderMouseToDrop; i--)
                            MoveUp();
                    }
                }

            }
        }

        private void LoadOrder_Resize(object sender, EventArgs e)
        {
            dgvLoadOrder.Size = new System.Drawing.Size(this.Width - 50, this.Height - 98);

            int btntops = this.Height - 76;
            btnPlus.Location = new Point(17, btntops);
            btnMinus.Location = new Point(75, btntops);

            btnSave.Location = new Point(this.Width - 242, btntops);
            btnCancel.Location = new Point(this.Width - 134, btntops);
        }


    }
}
