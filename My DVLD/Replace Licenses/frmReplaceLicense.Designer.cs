namespace My_DVLD
{
    partial class frmReplaceLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplaceLicense));
            this.gbReplacementFor = new System.Windows.Forms.GroupBox();
            this.rbLostLicense = new System.Windows.Forms.RadioButton();
            this.rbDamagedLicense = new System.Windows.Forms.RadioButton();
            this.gbApplicationInfoFor = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblOldLicenseID = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblReplacedLicenseID = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.lblReplaceLicenseApplicationID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LlblShowLicensesInfo = new System.Windows.Forms.LinkLabel();
            this.LlblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnIssueReplacement = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlDriverLicenseInfoWithFilter1 = new My_DVLD.ctrlDriverLicenseInfoWithFilter();
            this.lblReplacementTitle = new System.Windows.Forms.Label();
            this.gbReplacementFor.SuspendLayout();
            this.gbApplicationInfoFor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // gbReplacementFor
            // 
            this.gbReplacementFor.Controls.Add(this.rbLostLicense);
            this.gbReplacementFor.Controls.Add(this.rbDamagedLicense);
            this.gbReplacementFor.Location = new System.Drawing.Point(586, 41);
            this.gbReplacementFor.Name = "gbReplacementFor";
            this.gbReplacementFor.Size = new System.Drawing.Size(255, 94);
            this.gbReplacementFor.TabIndex = 25;
            this.gbReplacementFor.TabStop = false;
            this.gbReplacementFor.Text = "Replacement For:";
            // 
            // rbLostLicense
            // 
            this.rbLostLicense.AutoSize = true;
            this.rbLostLicense.Location = new System.Drawing.Point(6, 56);
            this.rbLostLicense.Name = "rbLostLicense";
            this.rbLostLicense.Size = new System.Drawing.Size(103, 20);
            this.rbLostLicense.TabIndex = 1;
            this.rbLostLicense.TabStop = true;
            this.rbLostLicense.Text = "Lost License";
            this.rbLostLicense.UseVisualStyleBackColor = true;
            this.rbLostLicense.CheckedChanged += new System.EventHandler(this.rbLostLicense_CheckedChanged);
            // 
            // rbDamagedLicense
            // 
            this.rbDamagedLicense.AutoSize = true;
            this.rbDamagedLicense.Location = new System.Drawing.Point(6, 28);
            this.rbDamagedLicense.Name = "rbDamagedLicense";
            this.rbDamagedLicense.Size = new System.Drawing.Size(139, 20);
            this.rbDamagedLicense.TabIndex = 0;
            this.rbDamagedLicense.TabStop = true;
            this.rbDamagedLicense.Text = "Damaged License";
            this.rbDamagedLicense.UseVisualStyleBackColor = true;
            this.rbDamagedLicense.CheckedChanged += new System.EventHandler(this.rbDamagedLicense_CheckedChanged);
            // 
            // gbApplicationInfoFor
            // 
            this.gbApplicationInfoFor.Controls.Add(this.label22);
            this.gbApplicationInfoFor.Controls.Add(this.pictureBox15);
            this.gbApplicationInfoFor.Controls.Add(this.pictureBox17);
            this.gbApplicationInfoFor.Controls.Add(this.pictureBox18);
            this.gbApplicationInfoFor.Controls.Add(this.lblCreatedBy);
            this.gbApplicationInfoFor.Controls.Add(this.lblOldLicenseID);
            this.gbApplicationInfoFor.Controls.Add(this.label27);
            this.gbApplicationInfoFor.Controls.Add(this.lblReplacedLicenseID);
            this.gbApplicationInfoFor.Controls.Add(this.label29);
            this.gbApplicationInfoFor.Controls.Add(this.label6);
            this.gbApplicationInfoFor.Controls.Add(this.pictureBox10);
            this.gbApplicationInfoFor.Controls.Add(this.pictureBox12);
            this.gbApplicationInfoFor.Controls.Add(this.lblApplicationFees);
            this.gbApplicationInfoFor.Controls.Add(this.lblApplicationDate);
            this.gbApplicationInfoFor.Controls.Add(this.label15);
            this.gbApplicationInfoFor.Controls.Add(this.pictureBox9);
            this.gbApplicationInfoFor.Controls.Add(this.lblReplaceLicenseApplicationID);
            this.gbApplicationInfoFor.Controls.Add(this.label4);
            this.gbApplicationInfoFor.Location = new System.Drawing.Point(12, 517);
            this.gbApplicationInfoFor.Name = "gbApplicationInfoFor";
            this.gbApplicationInfoFor.Size = new System.Drawing.Size(862, 146);
            this.gbApplicationInfoFor.TabIndex = 26;
            this.gbApplicationInfoFor.TabStop = false;
            this.gbApplicationInfoFor.Text = "Application Info for license ??";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(355, 105);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(96, 18);
            this.label22.TabIndex = 54;
            this.label22.Text = "Created By:";
            // 
            // pictureBox15
            // 
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(524, 105);
            this.pictureBox15.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(18, 18);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox15.TabIndex = 65;
            this.pictureBox15.TabStop = false;
            // 
            // pictureBox17
            // 
            this.pictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox17.Image")));
            this.pictureBox17.Location = new System.Drawing.Point(524, 73);
            this.pictureBox17.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(18, 18);
            this.pictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox17.TabIndex = 63;
            this.pictureBox17.TabStop = false;
            // 
            // pictureBox18
            // 
            this.pictureBox18.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox18.Image")));
            this.pictureBox18.Location = new System.Drawing.Point(524, 36);
            this.pictureBox18.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.Size = new System.Drawing.Size(18, 18);
            this.pictureBox18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox18.TabIndex = 62;
            this.pictureBox18.TabStop = false;
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(557, 105);
            this.lblCreatedBy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(26, 18);
            this.lblCreatedBy.TabIndex = 61;
            this.lblCreatedBy.Text = "??";
            // 
            // lblOldLicenseID
            // 
            this.lblOldLicenseID.AutoSize = true;
            this.lblOldLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldLicenseID.Location = new System.Drawing.Point(557, 73);
            this.lblOldLicenseID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOldLicenseID.Name = "lblOldLicenseID";
            this.lblOldLicenseID.Size = new System.Drawing.Size(26, 18);
            this.lblOldLicenseID.TabIndex = 58;
            this.lblOldLicenseID.Text = "??";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(355, 73);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(123, 18);
            this.label27.TabIndex = 57;
            this.label27.Text = "Old License ID:";
            // 
            // lblReplacedLicenseID
            // 
            this.lblReplacedLicenseID.AutoSize = true;
            this.lblReplacedLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplacedLicenseID.Location = new System.Drawing.Point(557, 35);
            this.lblReplacedLicenseID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReplacedLicenseID.Name = "lblReplacedLicenseID";
            this.lblReplacedLicenseID.Size = new System.Drawing.Size(26, 18);
            this.lblReplacedLicenseID.TabIndex = 56;
            this.lblReplacedLicenseID.Text = "??";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(355, 35);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(167, 18);
            this.label29.TabIndex = 55;
            this.label29.Text = "Replaced License ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 105);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 18);
            this.label6.TabIndex = 43;
            this.label6.Text = "Application Fees:";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(147, 105);
            this.pictureBox10.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(18, 18);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 47;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(147, 73);
            this.pictureBox12.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(18, 18);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 45;
            this.pictureBox12.TabStop = false;
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationFees.Location = new System.Drawing.Point(169, 105);
            this.lblApplicationFees.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(26, 18);
            this.lblApplicationFees.TabIndex = 44;
            this.lblApplicationFees.Text = "??";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.Location = new System.Drawing.Point(169, 73);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(26, 18);
            this.lblApplicationDate.TabIndex = 40;
            this.lblApplicationDate.Text = "??";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(5, 73);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(135, 18);
            this.label15.TabIndex = 39;
            this.label15.Text = "Application Date:";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(147, 35);
            this.pictureBox9.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(18, 18);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 38;
            this.pictureBox9.TabStop = false;
            // 
            // lblReplaceLicenseApplicationID
            // 
            this.lblReplaceLicenseApplicationID.AutoSize = true;
            this.lblReplaceLicenseApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplaceLicenseApplicationID.Location = new System.Drawing.Point(169, 35);
            this.lblReplaceLicenseApplicationID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReplaceLicenseApplicationID.Name = "lblReplaceLicenseApplicationID";
            this.lblReplaceLicenseApplicationID.Size = new System.Drawing.Size(26, 18);
            this.lblReplaceLicenseApplicationID.TabIndex = 37;
            this.lblReplaceLicenseApplicationID.Text = "??";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 18);
            this.label4.TabIndex = 36;
            this.label4.Text = "L.R.ApplicationID:";
            // 
            // LlblShowLicensesInfo
            // 
            this.LlblShowLicensesInfo.AutoSize = true;
            this.LlblShowLicensesInfo.Enabled = false;
            this.LlblShowLicensesInfo.Location = new System.Drawing.Point(181, 687);
            this.LlblShowLicensesInfo.Name = "LlblShowLicensesInfo";
            this.LlblShowLicensesInfo.Size = new System.Drawing.Size(121, 16);
            this.LlblShowLicensesInfo.TabIndex = 73;
            this.LlblShowLicensesInfo.TabStop = true;
            this.LlblShowLicensesInfo.Text = "Show Licenses Info";
            this.LlblShowLicensesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblShowLicensesInfo_LinkClicked);
            // 
            // LlblShowLicenseHistory
            // 
            this.LlblShowLicenseHistory.AutoSize = true;
            this.LlblShowLicenseHistory.Enabled = false;
            this.LlblShowLicenseHistory.Location = new System.Drawing.Point(19, 687);
            this.LlblShowLicenseHistory.Name = "LlblShowLicenseHistory";
            this.LlblShowLicenseHistory.Size = new System.Drawing.Size(135, 16);
            this.LlblShowLicenseHistory.TabIndex = 72;
            this.LlblShowLicenseHistory.TabStop = true;
            this.LlblShowLicenseHistory.Text = "Show License History";
            this.LlblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblShowLicenseHistory_LinkClicked);
            // 
            // btnIssueReplacement
            // 
            this.btnIssueReplacement.Enabled = false;
            this.btnIssueReplacement.Image = ((System.Drawing.Image)(resources.GetObject("btnIssueReplacement.Image")));
            this.btnIssueReplacement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueReplacement.Location = new System.Drawing.Point(673, 673);
            this.btnIssueReplacement.Margin = new System.Windows.Forms.Padding(2);
            this.btnIssueReplacement.Name = "btnIssueReplacement";
            this.btnIssueReplacement.Size = new System.Drawing.Size(201, 44);
            this.btnIssueReplacement.TabIndex = 71;
            this.btnIssueReplacement.Text = "Issue Replacement";
            this.btnIssueReplacement.UseVisualStyleBackColor = true;
            this.btnIssueReplacement.Click += new System.EventHandler(this.btnIssueReplacement_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(536, 673);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 44);
            this.btnClose.TabIndex = 70;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlDriverLicenseInfoWithFilter1
            // 
            this.ctrlDriverLicenseInfoWithFilter1.LicenseID = 0;
            this.ctrlDriverLicenseInfoWithFilter1.Location = new System.Drawing.Point(12, 32);
            this.ctrlDriverLicenseInfoWithFilter1.Name = "ctrlDriverLicenseInfoWithFilter1";
            this.ctrlDriverLicenseInfoWithFilter1.Size = new System.Drawing.Size(864, 489);
            this.ctrlDriverLicenseInfoWithFilter1.TabIndex = 24;
            this.ctrlDriverLicenseInfoWithFilter1.OnLicenseSelected += new System.Action<Business.clsLicense>(this.ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected);
            // 
            // lblReplacementTitle
            // 
            this.lblReplacementTitle.AutoSize = true;
            this.lblReplacementTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplacementTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblReplacementTitle.Location = new System.Drawing.Point(228, 7);
            this.lblReplacementTitle.Name = "lblReplacementTitle";
            this.lblReplacementTitle.Size = new System.Drawing.Size(48, 32);
            this.lblReplacementTitle.TabIndex = 74;
            this.lblReplacementTitle.Text = "??";
            // 
            // frmReplaceLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 723);
            this.Controls.Add(this.lblReplacementTitle);
            this.Controls.Add(this.LlblShowLicensesInfo);
            this.Controls.Add(this.gbApplicationInfoFor);
            this.Controls.Add(this.LlblShowLicenseHistory);
            this.Controls.Add(this.gbReplacementFor);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnIssueReplacement);
            this.Controls.Add(this.ctrlDriverLicenseInfoWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmReplaceLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Replace License";
            this.Load += new System.EventHandler(this.frmReplaceLicense_Load);
            this.gbReplacementFor.ResumeLayout(false);
            this.gbReplacementFor.PerformLayout();
            this.gbApplicationInfoFor.ResumeLayout(false);
            this.gbApplicationInfoFor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ctrlDriverLicenseInfoWithFilter ctrlDriverLicenseInfoWithFilter1;
        private System.Windows.Forms.GroupBox gbReplacementFor;
        private System.Windows.Forms.RadioButton rbLostLicense;
        private System.Windows.Forms.RadioButton rbDamagedLicense;
        private System.Windows.Forms.GroupBox gbApplicationInfoFor;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Windows.Forms.PictureBox pictureBox18;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblOldLicenseID;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblReplacedLicenseID;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Label lblReplaceLicenseApplicationID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel LlblShowLicensesInfo;
        private System.Windows.Forms.LinkLabel LlblShowLicenseHistory;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnIssueReplacement;
        private System.Windows.Forms.Label lblReplacementTitle;
    }
}