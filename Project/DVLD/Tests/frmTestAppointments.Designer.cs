namespace DVLD.Tests.Schdule_Test
{
    partial class frmTestAppointments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAddNewAppointment = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picTest = new System.Windows.Forms.PictureBox();
            this.drgAppointments = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecord = new System.Windows.Forms.Label();
            this.lblRecord1 = new System.Windows.Forms.Label();
            this.ctrlDrivingLicenseApplicationInfo1 = new DVLD.Controls.ApplicationControls.ctrlDrivingLicenseApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drgAppointments)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddNewAppointment
            // 
            this.btnAddNewAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewAppointment.Image = global::DVLD.Properties.Resources.AddAppointment_32;
            this.btnAddNewAppointment.Location = new System.Drawing.Point(874, 548);
            this.btnAddNewAppointment.Name = "btnAddNewAppointment";
            this.btnAddNewAppointment.Size = new System.Drawing.Size(49, 36);
            this.btnAddNewAppointment.TabIndex = 143;
            this.btnAddNewAppointment.UseVisualStyleBackColor = true;
            this.btnAddNewAppointment.Click += new System.EventHandler(this.btnAddNewAppointment_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(267, 123);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(441, 39);
            this.lblTitle.TabIndex = 141;
            this.lblTitle.Text = "Vision Test Appointments";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picTest
            // 
            this.picTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picTest.InitialImage = null;
            this.picTest.Location = new System.Drawing.Point(405, 14);
            this.picTest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picTest.Name = "picTest";
            this.picTest.Size = new System.Drawing.Size(113, 104);
            this.picTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTest.TabIndex = 140;
            this.picTest.TabStop = false;
            // 
            // drgAppointments
            // 
            this.drgAppointments.AllowUserToAddRows = false;
            this.drgAppointments.AllowUserToDeleteRows = false;
            this.drgAppointments.AllowUserToResizeRows = false;
            this.drgAppointments.BackgroundColor = System.Drawing.Color.White;
            this.drgAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drgAppointments.ContextMenuStrip = this.contextMenuStrip1;
            this.drgAppointments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.drgAppointments.Location = new System.Drawing.Point(13, 592);
            this.drgAppointments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.drgAppointments.MultiSelect = false;
            this.drgAppointments.Name = "drgAppointments";
            this.drgAppointments.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.drgAppointments.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.drgAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.drgAppointments.Size = new System.Drawing.Size(909, 161);
            this.drgAppointments.TabIndex = 138;
            this.drgAppointments.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(195, 98);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(194, 36);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Test_32;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(194, 36);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecord.Location = new System.Drawing.Point(117, 758);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(39, 20);
            this.lblRecord.TabIndex = 145;
            this.lblRecord.Text = "???";
            // 
            // lblRecord1
            // 
            this.lblRecord1.AutoSize = true;
            this.lblRecord1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord1.Location = new System.Drawing.Point(21, 758);
            this.lblRecord1.Name = "lblRecord1";
            this.lblRecord1.Size = new System.Drawing.Size(101, 20);
            this.lblRecord1.TabIndex = 146;
            this.lblRecord1.Text = "# Records: ";
            // 
            // ctrlDrivingLicenseApplicationInfo1
            // 
            this.ctrlDrivingLicenseApplicationInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlDrivingLicenseApplicationInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(13, 167);
            this.ctrlDrivingLicenseApplicationInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlDrivingLicenseApplicationInfo1.Name = "ctrlDrivingLicenseApplicationInfo1";
            this.ctrlDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(909, 376);
            this.ctrlDrivingLicenseApplicationInfo1.TabIndex = 144;
            // 
            // frmTestAppointments
            // 
            this.ClientSize = new System.Drawing.Size(940, 790);
            this.Controls.Add(this.lblRecord1);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationInfo1);
            this.Controls.Add(this.btnAddNewAppointment);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picTest);
            this.Controls.Add(this.drgAppointments);
            this.Name = "frmTestAppointments";
            this.Load += new System.EventHandler(this.frmTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drgAppointments)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.Filtering.FilteringUIContext filteringUIContext1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        //private Controls.ApplicationControls.ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnAddNewAppointment;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picTest;
        private System.Windows.Forms.DataGridView drgAppointments;
        private DVLD.Controls.ApplicationControls.ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Label lblRecord1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}