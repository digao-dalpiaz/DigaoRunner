namespace DigaoRunnerApp
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            EdLog = new RichTextBox();
            StatusBar = new StatusStrip();
            StDigaoDalpiaz = new ToolStripStatusLabel();
            StVersion = new ToolStripStatusLabel();
            StAdmin = new ToolStripStatusLabel();
            StElapsed = new ToolStripStatusLabel();
            StStatus = new ToolStripStatusLabel();
            ProgressBar = new ToolStripProgressBar();
            ButtonBar = new ToolStrip();
            BtnRun = new ToolStripButton();
            BtnCancel = new ToolStripButton();
            BtnConfig = new ToolStripButton();
            BtnMore = new ToolStripDropDownButton();
            BtnRegisterExtension = new ToolStripMenuItem();
            TimerControl = new System.Windows.Forms.Timer(components);
            BoxFields = new Panel();
            StatusBar.SuspendLayout();
            ButtonBar.SuspendLayout();
            SuspendLayout();
            // 
            // EdLog
            // 
            EdLog.BorderStyle = BorderStyle.None;
            EdLog.Dock = DockStyle.Fill;
            EdLog.Location = new Point(0, 27);
            EdLog.Name = "EdLog";
            EdLog.ReadOnly = true;
            EdLog.Size = new Size(987, 475);
            EdLog.TabIndex = 0;
            EdLog.Text = "";
            // 
            // StatusBar
            // 
            StatusBar.ImageScalingSize = new Size(20, 20);
            StatusBar.Items.AddRange(new ToolStripItem[] { StDigaoDalpiaz, StVersion, StAdmin, StElapsed, StStatus, ProgressBar });
            StatusBar.Location = new Point(0, 502);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new Size(987, 26);
            StatusBar.TabIndex = 1;
            // 
            // StDigaoDalpiaz
            // 
            StDigaoDalpiaz.IsLink = true;
            StDigaoDalpiaz.Name = "StDigaoDalpiaz";
            StDigaoDalpiaz.Size = new Size(105, 20);
            StDigaoDalpiaz.Text = "Digao Dalpiaz";
            StDigaoDalpiaz.Click += StDigaoDalpiaz_Click;
            // 
            // StVersion
            // 
            StVersion.ForeColor = Color.DimGray;
            StVersion.Name = "StVersion";
            StVersion.Size = new Size(57, 20);
            StVersion.Text = "Version";
            // 
            // StAdmin
            // 
            StAdmin.ForeColor = Color.FromArgb(192, 0, 192);
            StAdmin.Image = Properties.Resources.admin;
            StAdmin.Name = "StAdmin";
            StAdmin.Size = new Size(73, 20);
            StAdmin.Text = "Admin";
            // 
            // StElapsed
            // 
            StElapsed.Image = Properties.Resources.clock;
            StElapsed.Name = "StElapsed";
            StElapsed.Size = new Size(81, 20);
            StElapsed.Text = "Elapsed";
            // 
            // StStatus
            // 
            StStatus.Name = "StStatus";
            StStatus.Size = new Size(49, 20);
            StStatus.Text = "Status";
            // 
            // ProgressBar
            // 
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(200, 18);
            // 
            // ButtonBar
            // 
            ButtonBar.ImageScalingSize = new Size(20, 20);
            ButtonBar.Items.AddRange(new ToolStripItem[] { BtnRun, BtnCancel, BtnConfig, BtnMore });
            ButtonBar.Location = new Point(0, 0);
            ButtonBar.Name = "ButtonBar";
            ButtonBar.ShowItemToolTips = false;
            ButtonBar.Size = new Size(987, 27);
            ButtonBar.TabIndex = 2;
            // 
            // BtnRun
            // 
            BtnRun.Image = Properties.Resources.play;
            BtnRun.ImageTransparentColor = Color.Magenta;
            BtnRun.Name = "BtnRun";
            BtnRun.Size = new Size(100, 24);
            BtnRun.Text = "Run Script";
            BtnRun.Click += BtnRun_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Image = Properties.Resources.cancel;
            BtnCancel.ImageTransparentColor = Color.Magenta;
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(77, 24);
            BtnCancel.Text = "Cancel";
            BtnCancel.Click += BtnCancel_Click;
            // 
            // BtnConfig
            // 
            BtnConfig.Image = Properties.Resources.config;
            BtnConfig.ImageTransparentColor = Color.Magenta;
            BtnConfig.Name = "BtnConfig";
            BtnConfig.Size = new Size(86, 24);
            BtnConfig.Text = "Settings";
            BtnConfig.Click += BtnConfig_Click;
            // 
            // BtnMore
            // 
            BtnMore.DropDownItems.AddRange(new ToolStripItem[] { BtnRegisterExtension });
            BtnMore.Image = Properties.Resources.other;
            BtnMore.ImageTransparentColor = Color.Magenta;
            BtnMore.Name = "BtnMore";
            BtnMore.Size = new Size(78, 24);
            BtnMore.Text = "More";
            // 
            // BtnRegisterExtension
            // 
            BtnRegisterExtension.Name = "BtnRegisterExtension";
            BtnRegisterExtension.Size = new Size(204, 26);
            BtnRegisterExtension.Text = "Register .drs files";
            BtnRegisterExtension.Click += BtnRegisterExtension_Click;
            // 
            // TimerControl
            // 
            TimerControl.Interval = 500;
            TimerControl.Tick += TimerControl_Tick;
            // 
            // BoxFields
            // 
            BoxFields.AutoScroll = true;
            BoxFields.BackColor = SystemColors.Info;
            BoxFields.Dock = DockStyle.Fill;
            BoxFields.Location = new Point(0, 27);
            BoxFields.Name = "BoxFields";
            BoxFields.Size = new Size(987, 475);
            BoxFields.TabIndex = 3;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(987, 528);
            Controls.Add(EdLog);
            Controls.Add(BoxFields);
            Controls.Add(ButtonBar);
            Controls.Add(StatusBar);
            Name = "FrmMain";
            Text = "Digao Runner";
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            StatusBar.ResumeLayout(false);
            StatusBar.PerformLayout();
            ButtonBar.ResumeLayout(false);
            ButtonBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip StatusBar;
        public RichTextBox EdLog;
        private ToolStrip ButtonBar;
        private ToolStripButton BtnCancel;
        private System.Windows.Forms.Timer TimerControl;
        private ToolStripStatusLabel StElapsed;
        public ToolStripProgressBar ProgressBar;
        private ToolStripStatusLabel StVersion;
        private ToolStripButton BtnConfig;
        public ToolStripStatusLabel StStatus;
        private ToolStripButton BtnRun;
        public Panel BoxFields;
        private ToolStripStatusLabel StDigaoDalpiaz;
        private ToolStripStatusLabel StAdmin;
        private ToolStripDropDownButton BtnMore;
        private ToolStripMenuItem BtnRegisterExtension;
    }
}
