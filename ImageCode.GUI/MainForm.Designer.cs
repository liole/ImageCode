namespace ImageCode.GUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.settings = new System.Windows.Forms.Panel();
			this.actionGroup = new System.Windows.Forms.GroupBox();
			this.moveAction = new System.Windows.Forms.RadioButton();
			this.fileAction = new System.Windows.Forms.RadioButton();
			this.lengthLbl = new System.Windows.Forms.Label();
			this.contrastVal = new System.Windows.Forms.Label();
			this.contrastTb = new System.Windows.Forms.TrackBar();
			this.ctLbl = new System.Windows.Forms.Label();
			this.brightnessVal = new System.Windows.Forms.Label();
			this.brightnessTb = new System.Windows.Forms.TrackBar();
			this.brLbl = new System.Windows.Forms.Label();
			this.qrVersion = new System.Windows.Forms.ComboBox();
			this.versionLbl = new System.Windows.Forms.Label();
			this.ecLevel = new System.Windows.Forms.ComboBox();
			this.levelLbl = new System.Windows.Forms.Label();
			this.qrText = new System.Windows.Forms.TextBox();
			this.testLbl = new System.Windows.Forms.Label();
			this.picture = new System.Windows.Forms.PictureBox();
			this.pictureContainer = new System.Windows.Forms.Panel();
			this.scaleBar = new System.Windows.Forms.TrackBar();
			this.readabilityVal = new System.Windows.Forms.Label();
			this.readabilityTb = new System.Windows.Forms.TrackBar();
			this.rdbLbl = new System.Windows.Forms.Label();
			this.settings.SuspendLayout();
			this.actionGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.contrastTb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
			this.pictureContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scaleBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.readabilityTb)).BeginInit();
			this.SuspendLayout();
			// 
			// settings
			// 
			this.settings.Controls.Add(this.readabilityVal);
			this.settings.Controls.Add(this.readabilityTb);
			this.settings.Controls.Add(this.rdbLbl);
			this.settings.Controls.Add(this.actionGroup);
			this.settings.Controls.Add(this.lengthLbl);
			this.settings.Controls.Add(this.contrastVal);
			this.settings.Controls.Add(this.contrastTb);
			this.settings.Controls.Add(this.ctLbl);
			this.settings.Controls.Add(this.brightnessVal);
			this.settings.Controls.Add(this.brightnessTb);
			this.settings.Controls.Add(this.brLbl);
			this.settings.Controls.Add(this.qrVersion);
			this.settings.Controls.Add(this.versionLbl);
			this.settings.Controls.Add(this.ecLevel);
			this.settings.Controls.Add(this.levelLbl);
			this.settings.Controls.Add(this.qrText);
			this.settings.Controls.Add(this.testLbl);
			this.settings.Dock = System.Windows.Forms.DockStyle.Left;
			this.settings.Location = new System.Drawing.Point(0, 0);
			this.settings.Name = "settings";
			this.settings.Size = new System.Drawing.Size(200, 361);
			this.settings.TabIndex = 0;
			// 
			// actionGroup
			// 
			this.actionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.actionGroup.Controls.Add(this.moveAction);
			this.actionGroup.Controls.Add(this.fileAction);
			this.actionGroup.Location = new System.Drawing.Point(12, 305);
			this.actionGroup.Name = "actionGroup";
			this.actionGroup.Size = new System.Drawing.Size(173, 46);
			this.actionGroup.TabIndex = 13;
			this.actionGroup.TabStop = false;
			this.actionGroup.Text = "Action";
			// 
			// moveAction
			// 
			this.moveAction.AutoSize = true;
			this.moveAction.Location = new System.Drawing.Point(81, 19);
			this.moveAction.Name = "moveAction";
			this.moveAction.Size = new System.Drawing.Size(52, 17);
			this.moveAction.TabIndex = 1;
			this.moveAction.Text = "Move";
			this.moveAction.UseVisualStyleBackColor = true;
			// 
			// fileAction
			// 
			this.fileAction.AutoSize = true;
			this.fileAction.Checked = true;
			this.fileAction.Location = new System.Drawing.Point(18, 19);
			this.fileAction.Name = "fileAction";
			this.fileAction.Size = new System.Drawing.Size(41, 17);
			this.fileAction.TabIndex = 0;
			this.fileAction.TabStop = true;
			this.fileAction.Text = "File";
			this.fileAction.UseVisualStyleBackColor = true;
			// 
			// lengthLbl
			// 
			this.lengthLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lengthLbl.Location = new System.Drawing.Point(46, 9);
			this.lengthLbl.Name = "lengthLbl";
			this.lengthLbl.Size = new System.Drawing.Size(135, 13);
			this.lengthLbl.TabIndex = 12;
			this.lengthLbl.Text = "0/0";
			this.lengthLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// contrastVal
			// 
			this.contrastVal.AutoSize = true;
			this.contrastVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.contrastVal.Location = new System.Drawing.Point(67, 197);
			this.contrastVal.Name = "contrastVal";
			this.contrastVal.Size = new System.Drawing.Size(24, 16);
			this.contrastVal.TabIndex = 11;
			this.contrastVal.Text = "75";
			// 
			// contrastTb
			// 
			this.contrastTb.AutoSize = false;
			this.contrastTb.Location = new System.Drawing.Point(15, 216);
			this.contrastTb.Maximum = 255;
			this.contrastTb.Minimum = -255;
			this.contrastTb.Name = "contrastTb";
			this.contrastTb.Size = new System.Drawing.Size(170, 29);
			this.contrastTb.TabIndex = 10;
			this.contrastTb.TickFrequency = 16;
			this.contrastTb.Value = 75;
			this.contrastTb.ValueChanged += new System.EventHandler(this.contrastTb_ValueChanged);
			// 
			// ctLbl
			// 
			this.ctLbl.AutoSize = true;
			this.ctLbl.Location = new System.Drawing.Point(12, 199);
			this.ctLbl.Name = "ctLbl";
			this.ctLbl.Size = new System.Drawing.Size(49, 13);
			this.ctLbl.TabIndex = 9;
			this.ctLbl.Text = "Contrast:";
			// 
			// brightnessVal
			// 
			this.brightnessVal.AutoSize = true;
			this.brightnessVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.brightnessVal.Location = new System.Drawing.Point(77, 148);
			this.brightnessVal.Name = "brightnessVal";
			this.brightnessVal.Size = new System.Drawing.Size(16, 16);
			this.brightnessVal.TabIndex = 8;
			this.brightnessVal.Text = "0";
			// 
			// brightnessTb
			// 
			this.brightnessTb.AutoSize = false;
			this.brightnessTb.Location = new System.Drawing.Point(15, 167);
			this.brightnessTb.Maximum = 255;
			this.brightnessTb.Minimum = -255;
			this.brightnessTb.Name = "brightnessTb";
			this.brightnessTb.Size = new System.Drawing.Size(170, 29);
			this.brightnessTb.TabIndex = 7;
			this.brightnessTb.TickFrequency = 16;
			this.brightnessTb.ValueChanged += new System.EventHandler(this.brightnessTb_ValueChanged);
			// 
			// brLbl
			// 
			this.brLbl.AutoSize = true;
			this.brLbl.Location = new System.Drawing.Point(12, 150);
			this.brLbl.Name = "brLbl";
			this.brLbl.Size = new System.Drawing.Size(59, 13);
			this.brLbl.TabIndex = 6;
			this.brLbl.Text = "Brightness:";
			// 
			// qrVersion
			// 
			this.qrVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.qrVersion.FormattingEnabled = true;
			this.qrVersion.Items.AddRange(new object[] {
            "Auto",
            "1 (21 x 21)",
            "2 (25 x 25)",
            "3 (29 x 29)",
            "4 (33 x 33)",
            "5 (37 x 37)",
            "6 (41 x 41) (recomended)",
            "7 (45 x 45)"});
			this.qrVersion.Location = new System.Drawing.Point(15, 126);
			this.qrVersion.Name = "qrVersion";
			this.qrVersion.Size = new System.Drawing.Size(170, 21);
			this.qrVersion.TabIndex = 5;
			this.qrVersion.SelectedIndexChanged += new System.EventHandler(this.qr_Changed);
			// 
			// versionLbl
			// 
			this.versionLbl.AutoSize = true;
			this.versionLbl.Location = new System.Drawing.Point(12, 109);
			this.versionLbl.Name = "versionLbl";
			this.versionLbl.Size = new System.Drawing.Size(88, 13);
			this.versionLbl.TabIndex = 4;
			this.versionLbl.Text = "QR Version (size)";
			// 
			// ecLevel
			// 
			this.ecLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ecLevel.FormattingEnabled = true;
			this.ecLevel.Items.AddRange(new object[] {
            "High (30%) (recomended)",
            "Quartile (25%)",
            "Medium (15%)",
            "Low (7%)"});
			this.ecLevel.Location = new System.Drawing.Point(15, 85);
			this.ecLevel.Name = "ecLevel";
			this.ecLevel.Size = new System.Drawing.Size(170, 21);
			this.ecLevel.TabIndex = 3;
			this.ecLevel.SelectedIndexChanged += new System.EventHandler(this.qr_Changed);
			// 
			// levelLbl
			// 
			this.levelLbl.AutoSize = true;
			this.levelLbl.Location = new System.Drawing.Point(12, 68);
			this.levelLbl.Name = "levelLbl";
			this.levelLbl.Size = new System.Drawing.Size(165, 13);
			this.levelLbl.TabIndex = 2;
			this.levelLbl.Text = "Error correction level (% recovery)";
			// 
			// qrText
			// 
			this.qrText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.qrText.Location = new System.Drawing.Point(15, 25);
			this.qrText.Multiline = true;
			this.qrText.Name = "qrText";
			this.qrText.Size = new System.Drawing.Size(170, 40);
			this.qrText.TabIndex = 1;
			this.qrText.TextChanged += new System.EventHandler(this.qr_Changed);
			// 
			// testLbl
			// 
			this.testLbl.AutoSize = true;
			this.testLbl.Location = new System.Drawing.Point(12, 9);
			this.testLbl.Name = "testLbl";
			this.testLbl.Size = new System.Drawing.Size(28, 13);
			this.testLbl.TabIndex = 0;
			this.testLbl.Text = "Text";
			// 
			// picture
			// 
			this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picture.Location = new System.Drawing.Point(0, 0);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(354, 361);
			this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picture.TabIndex = 1;
			this.picture.TabStop = false;
			this.picture.Paint += new System.Windows.Forms.PaintEventHandler(this.picture_Paint);
			this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picture_MouseDown);
			this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picture_MouseMove);
			// 
			// pictureContainer
			// 
			this.pictureContainer.AllowDrop = true;
			this.pictureContainer.Controls.Add(this.picture);
			this.pictureContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureContainer.Location = new System.Drawing.Point(200, 0);
			this.pictureContainer.Name = "pictureContainer";
			this.pictureContainer.Size = new System.Drawing.Size(354, 361);
			this.pictureContainer.TabIndex = 2;
			this.pictureContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureContainer_DragDrop);
			this.pictureContainer.DragEnter += new System.Windows.Forms.DragEventHandler(this.picture_DragEnter);
			this.pictureContainer.DragLeave += new System.EventHandler(this.picture_DragLeave);
			// 
			// scaleBar
			// 
			this.scaleBar.AutoSize = false;
			this.scaleBar.Dock = System.Windows.Forms.DockStyle.Right;
			this.scaleBar.Location = new System.Drawing.Point(554, 0);
			this.scaleBar.Maximum = 40;
			this.scaleBar.Minimum = -40;
			this.scaleBar.Name = "scaleBar";
			this.scaleBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.scaleBar.Size = new System.Drawing.Size(30, 361);
			this.scaleBar.TabIndex = 2;
			this.scaleBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.scaleBar.ValueChanged += new System.EventHandler(this.qr_Changed);
			// 
			// readabilityVal
			// 
			this.readabilityVal.AutoSize = true;
			this.readabilityVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.readabilityVal.Location = new System.Drawing.Point(80, 251);
			this.readabilityVal.Name = "readabilityVal";
			this.readabilityVal.Size = new System.Drawing.Size(37, 16);
			this.readabilityVal.TabIndex = 16;
			this.readabilityVal.Text = "75%";
			// 
			// readabilityTb
			// 
			this.readabilityTb.AutoSize = false;
			this.readabilityTb.Location = new System.Drawing.Point(15, 270);
			this.readabilityTb.Maximum = 100;
			this.readabilityTb.Minimum = 50;
			this.readabilityTb.Name = "readabilityTb";
			this.readabilityTb.Size = new System.Drawing.Size(170, 29);
			this.readabilityTb.TabIndex = 15;
			this.readabilityTb.TickFrequency = 5;
			this.readabilityTb.Value = 75;
			this.readabilityTb.ValueChanged += new System.EventHandler(this.readabilityTb_ValueChanged);
			// 
			// rdbLbl
			// 
			this.rdbLbl.AutoSize = true;
			this.rdbLbl.Location = new System.Drawing.Point(12, 253);
			this.rdbLbl.Name = "rdbLbl";
			this.rdbLbl.Size = new System.Drawing.Size(62, 13);
			this.rdbLbl.TabIndex = 14;
			this.rdbLbl.Text = "Readability:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.Controls.Add(this.pictureContainer);
			this.Controls.Add(this.scaleBar);
			this.Controls.Add(this.settings);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(340, 160);
			this.Name = "MainForm";
			this.Text = "Image QR Code GUI";
			this.Resize += new System.EventHandler(this.qr_Changed);
			this.settings.ResumeLayout(false);
			this.settings.PerformLayout();
			this.actionGroup.ResumeLayout(false);
			this.actionGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.contrastTb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
			this.pictureContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scaleBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.readabilityTb)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel settings;
		private System.Windows.Forms.TextBox qrText;
		private System.Windows.Forms.Label testLbl;
		private System.Windows.Forms.PictureBox picture;
		private System.Windows.Forms.ComboBox ecLevel;
		private System.Windows.Forms.Label levelLbl;
		private System.Windows.Forms.ComboBox qrVersion;
		private System.Windows.Forms.Label versionLbl;
		private System.Windows.Forms.Panel pictureContainer;
		private System.Windows.Forms.Label contrastVal;
		private System.Windows.Forms.TrackBar contrastTb;
		private System.Windows.Forms.Label ctLbl;
		private System.Windows.Forms.Label brightnessVal;
		private System.Windows.Forms.TrackBar brightnessTb;
		private System.Windows.Forms.Label brLbl;
		private System.Windows.Forms.Label lengthLbl;
		private System.Windows.Forms.GroupBox actionGroup;
		private System.Windows.Forms.RadioButton moveAction;
		private System.Windows.Forms.RadioButton fileAction;
		private System.Windows.Forms.TrackBar scaleBar;
		private System.Windows.Forms.Label readabilityVal;
		private System.Windows.Forms.TrackBar readabilityTb;
		private System.Windows.Forms.Label rdbLbl;
	}
}