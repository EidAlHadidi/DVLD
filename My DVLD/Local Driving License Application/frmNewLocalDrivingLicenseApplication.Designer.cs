namespace My_DVLD
{
    partial class frmNewLocalDrivingLicenseApplication
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewLocalDrivingLicenseApplication));
			this.lblFormStatus = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpPersonalInfo = new System.Windows.Forms.TabPage();
			this.btnNext = new System.Windows.Forms.Button();
			this.ctrlPersonInformationWithFilter1 = new My_DVLD.ctrlPersonInformationWithFilter();
			this.tpApplicationInfo = new System.Windows.Forms.TabPage();
			this.cbLicenseClasses = new System.Windows.Forms.ComboBox();
			this.lblCreatedBy = new System.Windows.Forms.Label();
			this.lblApplicationFees = new System.Windows.Forms.Label();
			this.lblApplicationDate = new System.Windows.Forms.Label();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.lblApplicationID = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tpPersonalInfo.SuspendLayout();
			this.tpApplicationInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// lblFormStatus
			// 
			this.lblFormStatus.AutoSize = true;
			this.lblFormStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFormStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblFormStatus.Location = new System.Drawing.Point(149, 26);
			this.lblFormStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFormStatus.Name = "lblFormStatus";
			this.lblFormStatus.Size = new System.Drawing.Size(597, 37);
			this.lblFormStatus.TabIndex = 2;
			this.lblFormStatus.Text = "New Local Driving License Application";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpPersonalInfo);
			this.tabControl1.Controls.Add(this.tpApplicationInfo);
			this.tabControl1.Location = new System.Drawing.Point(9, 97);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1029, 629);
			this.tabControl1.TabIndex = 3;
			// 
			// tpPersonalInfo
			// 
			this.tpPersonalInfo.Controls.Add(this.btnNext);
			this.tpPersonalInfo.Controls.Add(this.ctrlPersonInformationWithFilter1);
			this.tpPersonalInfo.Location = new System.Drawing.Point(4, 25);
			this.tpPersonalInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpPersonalInfo.Name = "tpPersonalInfo";
			this.tpPersonalInfo.Padding = new System.Windows.Forms.Padding(4);
			this.tpPersonalInfo.Size = new System.Drawing.Size(1021, 600);
			this.tpPersonalInfo.TabIndex = 0;
			this.tpPersonalInfo.Text = "Personal Info";
			this.tpPersonalInfo.UseVisualStyleBackColor = true;
			// 
			// btnNext
			// 
			this.btnNext.Enabled = false;
			this.btnNext.Image = global::My_DVLD.Properties.Resources.Next_32;
			this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNext.Location = new System.Drawing.Point(853, 542);
			this.btnNext.Margin = new System.Windows.Forms.Padding(4);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(156, 46);
			this.btnNext.TabIndex = 23;
			this.btnNext.Text = "Next";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// ctrlPersonInformationWithFilter1
			// 
			this.ctrlPersonInformationWithFilter1.Location = new System.Drawing.Point(8, 7);
			this.ctrlPersonInformationWithFilter1.Margin = new System.Windows.Forms.Padding(5);
			this.ctrlPersonInformationWithFilter1.Name = "ctrlPersonInformationWithFilter1";
			this.ctrlPersonInformationWithFilter1.Size = new System.Drawing.Size(1005, 530);
			this.ctrlPersonInformationWithFilter1.TabIndex = 24;
			this.ctrlPersonInformationWithFilter1.OnPersonSelected += new My_DVLD.ctrlPersonInformationWithFilter.PersonSelectDataBack(this.ctrlPersonInformationWithFilter1_OnPersonSelected);
			// 
			// tpApplicationInfo
			// 
			this.tpApplicationInfo.Controls.Add(this.cbLicenseClasses);
			this.tpApplicationInfo.Controls.Add(this.lblCreatedBy);
			this.tpApplicationInfo.Controls.Add(this.lblApplicationFees);
			this.tpApplicationInfo.Controls.Add(this.lblApplicationDate);
			this.tpApplicationInfo.Controls.Add(this.pictureBox5);
			this.tpApplicationInfo.Controls.Add(this.pictureBox4);
			this.tpApplicationInfo.Controls.Add(this.pictureBox3);
			this.tpApplicationInfo.Controls.Add(this.pictureBox1);
			this.tpApplicationInfo.Controls.Add(this.pictureBox2);
			this.tpApplicationInfo.Controls.Add(this.lblApplicationID);
			this.tpApplicationInfo.Controls.Add(this.label5);
			this.tpApplicationInfo.Controls.Add(this.label4);
			this.tpApplicationInfo.Controls.Add(this.label2);
			this.tpApplicationInfo.Controls.Add(this.label1);
			this.tpApplicationInfo.Controls.Add(this.label3);
			this.tpApplicationInfo.Location = new System.Drawing.Point(4, 25);
			this.tpApplicationInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpApplicationInfo.Name = "tpApplicationInfo";
			this.tpApplicationInfo.Padding = new System.Windows.Forms.Padding(4);
			this.tpApplicationInfo.Size = new System.Drawing.Size(1021, 600);
			this.tpApplicationInfo.TabIndex = 1;
			this.tpApplicationInfo.Text = "Application Info";
			this.tpApplicationInfo.UseVisualStyleBackColor = true;
			// 
			// cbLicenseClasses
			// 
			this.cbLicenseClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLicenseClasses.FormattingEnabled = true;
			this.cbLicenseClasses.Location = new System.Drawing.Point(405, 212);
			this.cbLicenseClasses.Margin = new System.Windows.Forms.Padding(4);
			this.cbLicenseClasses.Name = "cbLicenseClasses";
			this.cbLicenseClasses.Size = new System.Drawing.Size(233, 24);
			this.cbLicenseClasses.TabIndex = 46;
			// 
			// lblCreatedBy
			// 
			this.lblCreatedBy.AutoSize = true;
			this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCreatedBy.Location = new System.Drawing.Point(400, 331);
			this.lblCreatedBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCreatedBy.Name = "lblCreatedBy";
			this.lblCreatedBy.Size = new System.Drawing.Size(49, 20);
			this.lblCreatedBy.TabIndex = 45;
			this.lblCreatedBy.Text = "[???]";
			// 
			// lblApplicationFees
			// 
			this.lblApplicationFees.AutoSize = true;
			this.lblApplicationFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblApplicationFees.Location = new System.Drawing.Point(400, 281);
			this.lblApplicationFees.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblApplicationFees.Name = "lblApplicationFees";
			this.lblApplicationFees.Size = new System.Drawing.Size(49, 20);
			this.lblApplicationFees.TabIndex = 44;
			this.lblApplicationFees.Text = "[???]";
			// 
			// lblApplicationDate
			// 
			this.lblApplicationDate.AutoSize = true;
			this.lblApplicationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblApplicationDate.Location = new System.Drawing.Point(400, 146);
			this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblApplicationDate.Name = "lblApplicationDate";
			this.lblApplicationDate.Size = new System.Drawing.Size(49, 20);
			this.lblApplicationDate.TabIndex = 43;
			this.lblApplicationDate.Text = "[???]";
			// 
			// pictureBox5
			// 
			this.pictureBox5.Image = global::My_DVLD.Properties.Resources.User_32__2;
			this.pictureBox5.Location = new System.Drawing.Point(313, 326);
			this.pictureBox5.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(45, 36);
			this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox5.TabIndex = 42;
			this.pictureBox5.TabStop = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.Image = global::My_DVLD.Properties.Resources.money_32;
			this.pictureBox4.Location = new System.Drawing.Point(313, 270);
			this.pictureBox4.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(45, 36);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox4.TabIndex = 41;
			this.pictureBox4.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = global::My_DVLD.Properties.Resources.Renew_Driving_License_32;
			this.pictureBox3.Location = new System.Drawing.Point(313, 208);
			this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(45, 36);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox3.TabIndex = 40;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::My_DVLD.Properties.Resources.Calendar_32;
			this.pictureBox1.Location = new System.Drawing.Point(313, 142);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(45, 36);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 39;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(313, 80);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(45, 36);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 38;
			this.pictureBox2.TabStop = false;
			// 
			// lblApplicationID
			// 
			this.lblApplicationID.AutoSize = true;
			this.lblApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblApplicationID.Location = new System.Drawing.Point(400, 85);
			this.lblApplicationID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblApplicationID.Name = "lblApplicationID";
			this.lblApplicationID.Size = new System.Drawing.Size(49, 20);
			this.lblApplicationID.TabIndex = 37;
			this.lblApplicationID.Text = "[???]";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(64, 331);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 20);
			this.label5.TabIndex = 36;
			this.label5.Text = "Created By:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(64, 270);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(148, 20);
			this.label4.TabIndex = 35;
			this.label4.Text = "Application Fees:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(64, 146);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(147, 20);
			this.label2.TabIndex = 34;
			this.label2.Text = "Application Date:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(64, 208);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 20);
			this.label1.TabIndex = 33;
			this.label1.Text = "License Class:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(64, 85);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(160, 20);
			this.label3.TabIndex = 32;
			this.label3.Text = "D.L Application ID:";
			// 
			// btnSave
			// 
			this.btnSave.Image = global::My_DVLD.Properties.Resources.Save_32;
			this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSave.Location = new System.Drawing.Point(868, 734);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(156, 46);
			this.btnSave.TabIndex = 22;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Image = global::My_DVLD.Properties.Resources.Close_32;
			this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnClose.Location = new System.Drawing.Point(697, 734);
			this.btnClose.Margin = new System.Windows.Forms.Padding(4);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(156, 46);
			this.btnClose.TabIndex = 21;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmNewLocalDrivingLicenseApplication
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1044, 806);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblFormStatus);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmNewLocalDrivingLicenseApplication";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New Local Driving License Application";
			this.Load += new System.EventHandler(this.frmNewLocalDrivingLicenseApplication_Load);
			this.tabControl1.ResumeLayout(false);
			this.tpPersonalInfo.ResumeLayout(false);
			this.tpApplicationInfo.ResumeLayout(false);
			this.tpApplicationInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPersonalInfo;
        private System.Windows.Forms.TabPage tpApplicationInfo;
        private System.Windows.Forms.Button btnNext;
        private ctrlPersonInformationWithFilter ctrlPersonInformationWithFilter1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox cbLicenseClasses;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblApplicationFees;
    }
}