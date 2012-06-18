namespace WebcomicDownloader
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelHeader = new SierraLib.UI.VerticalGradientPanel();
            this.chkDownloadAll = new System.Windows.Forms.CheckBox();
            this.cbComics = new SierraLib.UI.AeroControls.ComboBox();
            this.panelBackground = new SierraLib.UI.VerticalGradientPanel();
            this.panelProgress = new SierraLib.UI.VerticalGradientPanel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblWasted = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new SierraLib.UI.AeroControls.Button();
            this.lblComic = new System.Windows.Forms.Label();
            this.lblTotalSize = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.lblDownloaded = new System.Windows.Forms.Label();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbProgress = new SierraLib.UI.AeroControls.ProgressBar();
            this.panelSettings = new SierraLib.UI.VerticalGradientPanel();
            this.chkTurnOffScreen = new System.Windows.Forms.CheckBox();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.chkPreventStandby = new System.Windows.Forms.CheckBox();
            this.linkCloseSettings = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panelDetails = new SierraLib.UI.VerticalGradientPanel();
            this.linkComic = new System.Windows.Forms.LinkLabel();
            this.lblComicName = new System.Windows.Forms.Label();
            this.linkShowSettings = new System.Windows.Forms.LinkLabel();
            this.panelBrowse = new SierraLib.UI.VerticalGradientPanel();
            this.txtPath = new SierraLib.UI.AeroControls.TextBox();
            this.btnBrowse = new SierraLib.UI.AeroControls.Button();
            this.btnDownload = new SierraLib.UI.AeroControls.Button();
            this.panelHeader.SuspendLayout();
            this.panelBackground.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.panelBrowse.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.Text = "WKD";
            this.trayIcon.Visible = true;
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.AnimateChildren = false;
            this.panelHeader.AnimatedControl = null;
            this.panelHeader.AnimationOrder = SierraLib.UI.Animations.AnimationOrder.Simultaneous;
            this.panelHeader.BackColor = System.Drawing.Color.Transparent;
            this.panelHeader.Controls.Add(this.chkDownloadAll);
            this.panelHeader.Controls.Add(this.cbComics);
            this.panelHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelHeader.GradientBottom = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelHeader.GradientBottomHeight = 50;
            this.panelHeader.GradientCenter = System.Drawing.Color.Black;
            this.panelHeader.GradientTop = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panelHeader.GradientTopHeight = 20;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.RenderingAttributes = null;
            this.panelHeader.Size = new System.Drawing.Size(639, 33);
            this.panelHeader.TabIndex = 4;
            this.panelHeader.UseAeroDefaults = false;
            // 
            // chkDownloadAll
            // 
            this.chkDownloadAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDownloadAll.AutoSize = true;
            this.chkDownloadAll.ForeColor = System.Drawing.Color.White;
            this.chkDownloadAll.Location = new System.Drawing.Point(512, 7);
            this.chkDownloadAll.Name = "chkDownloadAll";
            this.chkDownloadAll.Size = new System.Drawing.Size(97, 19);
            this.chkDownloadAll.TabIndex = 3;
            this.chkDownloadAll.Text = "Download All";
            this.chkDownloadAll.UseVisualStyleBackColor = true;
            this.chkDownloadAll.CheckedChanged += new System.EventHandler(this.chkDownloadAll_CheckedChanged);
            // 
            // cbComics
            // 
            this.cbComics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbComics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComics.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbComics.FormattingEnabled = true;
            this.cbComics.Location = new System.Drawing.Point(13, 5);
            this.cbComics.Name = "cbComics";
            this.cbComics.Size = new System.Drawing.Size(466, 23);
            this.cbComics.TabIndex = 0;
            this.cbComics.SelectedIndexChanged += new System.EventHandler(this.cbComics_SelectedIndexChanged);
            // 
            // panelBackground
            // 
            this.panelBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBackground.AnimateChildren = false;
            this.panelBackground.AnimatedControl = null;
            this.panelBackground.AnimationOrder = SierraLib.UI.Animations.AnimationOrder.Simultaneous;
            this.panelBackground.BackColor = System.Drawing.Color.Transparent;
            this.panelBackground.Controls.Add(this.panelProgress);
            this.panelBackground.Controls.Add(this.panelSettings);
            this.panelBackground.Controls.Add(this.label4);
            this.panelBackground.Controls.Add(this.label2);
            this.panelBackground.Controls.Add(this.linkLabel1);
            this.panelBackground.Controls.Add(this.panelDetails);
            this.panelBackground.Controls.Add(this.linkShowSettings);
            this.panelBackground.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelBackground.GradientBottom = System.Drawing.Color.Silver;
            this.panelBackground.GradientBottomHeight = 100;
            this.panelBackground.GradientCenter = System.Drawing.Color.White;
            this.panelBackground.GradientTop = System.Drawing.Color.DimGray;
            this.panelBackground.GradientTopHeight = 5;
            this.panelBackground.Location = new System.Drawing.Point(0, 32);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.RenderingAttributes = null;
            this.panelBackground.Size = new System.Drawing.Size(639, 292);
            this.panelBackground.TabIndex = 3;
            this.panelBackground.UseAeroDefaults = false;
            // 
            // panelProgress
            // 
            this.panelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProgress.AnimateChildren = false;
            this.panelProgress.AnimatedControl = null;
            this.panelProgress.AnimationOrder = SierraLib.UI.Animations.AnimationOrder.Simultaneous;
            this.panelProgress.BackColor = System.Drawing.Color.Transparent;
            this.panelProgress.Controls.Add(this.lblProgress);
            this.panelProgress.Controls.Add(this.lblWasted);
            this.panelProgress.Controls.Add(this.label1);
            this.panelProgress.Controls.Add(this.btnCancel);
            this.panelProgress.Controls.Add(this.lblComic);
            this.panelProgress.Controls.Add(this.lblTotalSize);
            this.panelProgress.Controls.Add(this.label6);
            this.panelProgress.Controls.Add(this.lblSpeed);
            this.panelProgress.Controls.Add(this.label9);
            this.panelProgress.Controls.Add(this.label3);
            this.panelProgress.Controls.Add(this.lblImageSize);
            this.panelProgress.Controls.Add(this.lblDownloaded);
            this.panelProgress.Controls.Add(this.lblRemaining);
            this.panelProgress.Controls.Add(this.lblStatus);
            this.panelProgress.Controls.Add(this.pbProgress);
            this.panelProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelProgress.GradientBottom = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.panelProgress.GradientBottomHeight = 30;
            this.panelProgress.GradientCenter = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelProgress.GradientTop = System.Drawing.Color.Black;
            this.panelProgress.GradientTopHeight = 1;
            this.panelProgress.Location = new System.Drawing.Point(0, 182);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.RenderingAttributes = null;
            this.panelProgress.Size = new System.Drawing.Size(639, 146);
            this.panelProgress.TabIndex = 0;
            this.panelProgress.UseAeroDefaults = false;
            this.panelProgress.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(302, 16);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(35, 21);
            this.lblProgress.TabIndex = 20;
            this.lblProgress.Text = "--%";
            // 
            // lblWasted
            // 
            this.lblWasted.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWasted.ForeColor = System.Drawing.Color.White;
            this.lblWasted.Location = new System.Drawing.Point(62, 87);
            this.lblWasted.Name = "lblWasted";
            this.lblWasted.Size = new System.Drawing.Size(85, 19);
            this.lblWasted.TabIndex = 19;
            this.lblWasted.Text = "0 KB";
            this.lblWasted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Wasted:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(558, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblComic
            // 
            this.lblComic.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComic.ForeColor = System.Drawing.Color.White;
            this.lblComic.Location = new System.Drawing.Point(335, 43);
            this.lblComic.Name = "lblComic";
            this.lblComic.Size = new System.Drawing.Size(34, 16);
            this.lblComic.TabIndex = 15;
            this.lblComic.Text = "0";
            this.lblComic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSize.ForeColor = System.Drawing.Color.White;
            this.lblTotalSize.Location = new System.Drawing.Point(335, 88);
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size(71, 16);
            this.lblTotalSize.TabIndex = 10;
            this.lblTotalSize.Text = "0 KB";
            this.lblTotalSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(285, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Total:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.ForeColor = System.Drawing.Color.White;
            this.lblSpeed.Location = new System.Drawing.Point(56, 27);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(64, 16);
            this.lblSpeed.TabIndex = 17;
            this.lblSpeed.Text = "0 kb/s";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(280, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Comic:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Speed:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImageSize
            // 
            this.lblImageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImageSize.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImageSize.ForeColor = System.Drawing.Color.White;
            this.lblImageSize.Location = new System.Drawing.Point(562, 43);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(71, 16);
            this.lblImageSize.TabIndex = 15;
            this.lblImageSize.Text = "0 KB";
            this.lblImageSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDownloaded
            // 
            this.lblDownloaded.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownloaded.ForeColor = System.Drawing.Color.White;
            this.lblDownloaded.Location = new System.Drawing.Point(7, 43);
            this.lblDownloaded.Name = "lblDownloaded";
            this.lblDownloaded.Size = new System.Drawing.Size(71, 16);
            this.lblDownloaded.TabIndex = 14;
            this.lblDownloaded.Text = "0 KB";
            this.lblDownloaded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRemaining
            // 
            this.lblRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRemaining.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining.ForeColor = System.Drawing.Color.White;
            this.lblRemaining.Location = new System.Drawing.Point(562, 27);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(71, 16);
            this.lblRemaining.TabIndex = 9;
            this.lblRemaining.Text = "0:00:00";
            this.lblRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(7, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(123, 19);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Downloading";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(10, 62);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(623, 19);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbProgress.TabIndex = 0;
            // 
            // panelSettings
            // 
            this.panelSettings.AnimateChildren = false;
            this.panelSettings.AnimatedControl = null;
            this.panelSettings.AnimationOrder = SierraLib.UI.Animations.AnimationOrder.Simultaneous;
            this.panelSettings.BackColor = System.Drawing.Color.Transparent;
            this.panelSettings.Controls.Add(this.chkTurnOffScreen);
            this.panelSettings.Controls.Add(this.chkCheckUpdates);
            this.panelSettings.Controls.Add(this.chkPreventStandby);
            this.panelSettings.Controls.Add(this.linkCloseSettings);
            this.panelSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelSettings.GradientBottom = System.Drawing.Color.Black;
            this.panelSettings.GradientBottomHeight = 50;
            this.panelSettings.GradientCenter = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panelSettings.GradientTop = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelSettings.GradientTopHeight = 30;
            this.panelSettings.Location = new System.Drawing.Point(438, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.RenderingAttributes = null;
            this.panelSettings.Size = new System.Drawing.Size(200, 83);
            this.panelSettings.TabIndex = 6;
            this.panelSettings.UseAeroDefaults = false;
            // 
            // chkTurnOffScreen
            // 
            this.chkTurnOffScreen.AutoSize = true;
            this.chkTurnOffScreen.ForeColor = System.Drawing.Color.White;
            this.chkTurnOffScreen.Location = new System.Drawing.Point(9, 59);
            this.chkTurnOffScreen.Name = "chkTurnOffScreen";
            this.chkTurnOffScreen.Size = new System.Drawing.Size(109, 19);
            this.chkTurnOffScreen.TabIndex = 3;
            this.chkTurnOffScreen.Text = "Turn Off Screen";
            this.chkTurnOffScreen.UseVisualStyleBackColor = true;
            this.chkTurnOffScreen.CheckedChanged += new System.EventHandler(this.chkTurnOffScreen_CheckedChanged);
            // 
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.ForeColor = System.Drawing.Color.White;
            this.chkCheckUpdates.Location = new System.Drawing.Point(9, 34);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(123, 19);
            this.chkCheckUpdates.TabIndex = 2;
            this.chkCheckUpdates.Text = "Check for Updates";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            this.chkCheckUpdates.CheckedChanged += new System.EventHandler(this.chkCheckUpdates_CheckedChanged);
            // 
            // chkPreventStandby
            // 
            this.chkPreventStandby.AutoSize = true;
            this.chkPreventStandby.ForeColor = System.Drawing.Color.White;
            this.chkPreventStandby.Location = new System.Drawing.Point(9, 9);
            this.chkPreventStandby.Name = "chkPreventStandby";
            this.chkPreventStandby.Size = new System.Drawing.Size(112, 19);
            this.chkPreventStandby.TabIndex = 1;
            this.chkPreventStandby.Text = "Prevent Standby";
            this.chkPreventStandby.UseVisualStyleBackColor = true;
            this.chkPreventStandby.CheckedChanged += new System.EventHandler(this.chkPreventStandby_CheckedChanged);
            // 
            // linkCloseSettings
            // 
            this.linkCloseSettings.AutoSize = true;
            this.linkCloseSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkCloseSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkCloseSettings.LinkColor = System.Drawing.Color.White;
            this.linkCloseSettings.Location = new System.Drawing.Point(182, 2);
            this.linkCloseSettings.Name = "linkCloseSettings";
            this.linkCloseSettings.Size = new System.Drawing.Size(14, 15);
            this.linkCloseSettings.TabIndex = 0;
            this.linkCloseSettings.TabStop = true;
            this.linkCloseSettings.Text = "X";
            this.linkCloseSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkCloseSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCloseSettings_LinkClicked);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(130, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(379, 88);
            this.label4.TabIndex = 5;
            this.label4.Text = resources.GetString("label4.Text");
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(133, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Instructions";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(128, 206);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(381, 15);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.sierrasoftworks.com/wkd";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panelDetails
            // 
            this.panelDetails.AnimateChildren = false;
            this.panelDetails.AnimatedControl = null;
            this.panelDetails.AnimationOrder = SierraLib.UI.Animations.AnimationOrder.Simultaneous;
            this.panelDetails.BackColor = System.Drawing.Color.Transparent;
            this.panelDetails.Controls.Add(this.linkComic);
            this.panelDetails.Controls.Add(this.lblComicName);
            this.panelDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelDetails.GradientBottom = System.Drawing.Color.Black;
            this.panelDetails.GradientBottomHeight = 30;
            this.panelDetails.GradientCenter = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelDetails.GradientTop = System.Drawing.Color.Black;
            this.panelDetails.GradientTopHeight = 1;
            this.panelDetails.Location = new System.Drawing.Point(0, 0);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.RenderingAttributes = null;
            this.panelDetails.Size = new System.Drawing.Size(314, 50);
            this.panelDetails.TabIndex = 2;
            this.panelDetails.UseAeroDefaults = false;
            this.panelDetails.Visible = false;
            // 
            // linkComic
            // 
            this.linkComic.LinkColor = System.Drawing.Color.Silver;
            this.linkComic.Location = new System.Drawing.Point(7, 23);
            this.linkComic.Name = "linkComic";
            this.linkComic.Size = new System.Drawing.Size(298, 23);
            this.linkComic.TabIndex = 1;
            this.linkComic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkComic.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkComic_LinkClicked);
            // 
            // lblComicName
            // 
            this.lblComicName.AutoEllipsis = true;
            this.lblComicName.ForeColor = System.Drawing.Color.White;
            this.lblComicName.Location = new System.Drawing.Point(7, 4);
            this.lblComicName.Name = "lblComicName";
            this.lblComicName.Size = new System.Drawing.Size(304, 19);
            this.lblComicName.TabIndex = 0;
            this.lblComicName.Text = "Select a Comic";
            // 
            // linkShowSettings
            // 
            this.linkShowSettings.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.linkShowSettings.AutoSize = true;
            this.linkShowSettings.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkShowSettings.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.linkShowSettings.Location = new System.Drawing.Point(581, 8);
            this.linkShowSettings.Name = "linkShowSettings";
            this.linkShowSettings.Size = new System.Drawing.Size(49, 15);
            this.linkShowSettings.TabIndex = 7;
            this.linkShowSettings.TabStop = true;
            this.linkShowSettings.Text = "Settings";
            this.linkShowSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkShowSettings_LinkClicked);
            // 
            // panelBrowse
            // 
            this.panelBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBrowse.AnimateChildren = false;
            this.panelBrowse.AnimatedControl = null;
            this.panelBrowse.AnimationOrder = SierraLib.UI.Animations.AnimationOrder.Simultaneous;
            this.panelBrowse.BackColor = System.Drawing.Color.Transparent;
            this.panelBrowse.Controls.Add(this.txtPath);
            this.panelBrowse.Controls.Add(this.btnBrowse);
            this.panelBrowse.Controls.Add(this.btnDownload);
            this.panelBrowse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelBrowse.GradientBottom = System.Drawing.Color.Black;
            this.panelBrowse.GradientBottomHeight = 30;
            this.panelBrowse.GradientCenter = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelBrowse.GradientTop = System.Drawing.Color.Black;
            this.panelBrowse.GradientTopHeight = 1;
            this.panelBrowse.Location = new System.Drawing.Point(0, 323);
            this.panelBrowse.Name = "panelBrowse";
            this.panelBrowse.RenderingAttributes = null;
            this.panelBrowse.Size = new System.Drawing.Size(639, 50);
            this.panelBrowse.TabIndex = 4;
            this.panelBrowse.UseAeroDefaults = false;
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.CueBannerText = "Click Browse to choose the directory to save comics to";
            this.txtPath.Location = new System.Drawing.Point(6, 7);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(465, 23);
            this.txtPath.TabIndex = 3;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowse.Location = new System.Drawing.Point(477, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDownload.Location = new System.Drawing.Point(558, 6);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 362);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelBackground);
            this.Controls.Add(this.panelBrowse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "WKD";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelBackground.ResumeLayout(false);
            this.panelBackground.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.panelDetails.ResumeLayout(false);
            this.panelBrowse.ResumeLayout(false);
            this.panelBrowse.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private SierraLib.UI.AeroControls.ComboBox cbComics;
        private SierraLib.UI.AeroControls.Button btnDownload;
        private SierraLib.UI.VerticalGradientPanel panelBackground;
        private SierraLib.UI.VerticalGradientPanel panelHeader;
        private SierraLib.UI.VerticalGradientPanel panelProgress;
        private SierraLib.UI.AeroControls.ProgressBar pbProgress;
        private SierraLib.UI.AeroControls.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkDownloadAll;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalSize;
        private System.Windows.Forms.Label lblRemaining;
        private SierraLib.UI.AeroControls.Button btnCancel;
        private System.Windows.Forms.Label lblComic;
        private System.Windows.Forms.Label label9;
        private SierraLib.UI.AeroControls.TextBox txtPath;
        private SierraLib.UI.VerticalGradientPanel panelDetails;
        private SierraLib.UI.VerticalGradientPanel panelBrowse;
        private System.Windows.Forms.Label lblComicName;
        private System.Windows.Forms.LinkLabel linkComic;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.Label lblDownloaded;
        private System.Windows.Forms.Label lblWasted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private SierraLib.UI.VerticalGradientPanel panelSettings;
        private System.Windows.Forms.LinkLabel linkCloseSettings;
        private System.Windows.Forms.CheckBox chkCheckUpdates;
        private System.Windows.Forms.CheckBox chkPreventStandby;
        private System.Windows.Forms.CheckBox chkTurnOffScreen;
        private System.Windows.Forms.LinkLabel linkShowSettings;
    }
}