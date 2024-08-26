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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            edLog = new RichTextBox();
            statusBar = new StatusStrip();
            stDigaoDalpiaz = new ToolStripStatusLabel();
            stVersion = new ToolStripStatusLabel();
            stElapsed = new ToolStripStatusLabel();
            stStatus = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            buttonBar = new ToolStrip();
            btnRun = new ToolStripButton();
            btnCancel = new ToolStripButton();
            btnFont = new ToolStripButton();
            timerControl = new System.Windows.Forms.Timer(components);
            images = new ImageList(components);
            boxFields = new Panel();
            stAdmin = new ToolStripStatusLabel();
            statusBar.SuspendLayout();
            buttonBar.SuspendLayout();
            SuspendLayout();
            // 
            // edLog
            // 
            edLog.BackColor = Color.FromArgb(30, 30, 30);
            edLog.BorderStyle = BorderStyle.None;
            edLog.Dock = DockStyle.Fill;
            edLog.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            edLog.ForeColor = Color.WhiteSmoke;
            edLog.Location = new Point(0, 27);
            edLog.Name = "edLog";
            edLog.ReadOnly = true;
            edLog.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            edLog.Size = new Size(987, 475);
            edLog.TabIndex = 0;
            edLog.Text = "";
            // 
            // statusBar
            // 
            statusBar.ImageScalingSize = new Size(20, 20);
            statusBar.Items.AddRange(new ToolStripItem[] { stDigaoDalpiaz, stVersion, stAdmin, stElapsed, stStatus, progressBar });
            statusBar.Location = new Point(0, 502);
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(987, 26);
            statusBar.TabIndex = 1;
            // 
            // stDigaoDalpiaz
            // 
            stDigaoDalpiaz.IsLink = true;
            stDigaoDalpiaz.Name = "stDigaoDalpiaz";
            stDigaoDalpiaz.Size = new Size(105, 20);
            stDigaoDalpiaz.Text = "Digao Dalpiaz";
            stDigaoDalpiaz.Click += stDigaoDalpiaz_Click;
            // 
            // stVersion
            // 
            stVersion.ForeColor = Color.DimGray;
            stVersion.Name = "stVersion";
            stVersion.Size = new Size(57, 20);
            stVersion.Text = "Version";
            // 
            // stElapsed
            // 
            stElapsed.Image = (Image)resources.GetObject("stElapsed.Image");
            stElapsed.Name = "stElapsed";
            stElapsed.Size = new Size(81, 20);
            stElapsed.Text = "Elapsed";
            // 
            // stStatus
            // 
            stStatus.Name = "stStatus";
            stStatus.Size = new Size(49, 20);
            stStatus.Text = "Status";
            // 
            // progressBar
            // 
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(200, 18);
            // 
            // buttonBar
            // 
            buttonBar.ImageScalingSize = new Size(20, 20);
            buttonBar.Items.AddRange(new ToolStripItem[] { btnRun, btnCancel, btnFont });
            buttonBar.Location = new Point(0, 0);
            buttonBar.Name = "buttonBar";
            buttonBar.Size = new Size(987, 27);
            buttonBar.TabIndex = 2;
            buttonBar.Text = "toolStrip1";
            // 
            // btnRun
            // 
            btnRun.Image = (Image)resources.GetObject("btnRun.Image");
            btnRun.ImageTransparentColor = Color.Magenta;
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(100, 24);
            btnRun.Text = "Run Script";
            btnRun.Click += btnRun_Click;
            // 
            // btnCancel
            // 
            btnCancel.Image = (Image)resources.GetObject("btnCancel.Image");
            btnCancel.ImageTransparentColor = Color.Magenta;
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(77, 24);
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnFont
            // 
            btnFont.Image = (Image)resources.GetObject("btnFont.Image");
            btnFont.ImageTransparentColor = Color.Magenta;
            btnFont.Name = "btnFont";
            btnFont.Size = new Size(86, 24);
            btnFont.Text = "Settings";
            btnFont.Click += btnFont_Click;
            // 
            // timerControl
            // 
            timerControl.Interval = 500;
            timerControl.Tick += timerControl_Tick;
            // 
            // images
            // 
            images.ColorDepth = ColorDepth.Depth32Bit;
            images.ImageStream = (ImageListStreamer)resources.GetObject("images.ImageStream");
            images.TransparentColor = Color.Transparent;
            images.Images.SetKeyName(0, "wait");
            images.Images.SetKeyName(1, "ok");
            images.Images.SetKeyName(2, "error");
            images.Images.SetKeyName(3, "bell");
            // 
            // boxFields
            // 
            boxFields.BackColor = SystemColors.Info;
            boxFields.Dock = DockStyle.Fill;
            boxFields.Location = new Point(0, 27);
            boxFields.Name = "boxFields";
            boxFields.Size = new Size(987, 475);
            boxFields.TabIndex = 3;
            // 
            // stAdmin
            // 
            stAdmin.ForeColor = Color.FromArgb(192, 0, 192);
            stAdmin.Name = "stAdmin";
            stAdmin.Size = new Size(53, 20);
            stAdmin.Text = "Admin";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(987, 528);
            Controls.Add(edLog);
            Controls.Add(boxFields);
            Controls.Add(buttonBar);
            Controls.Add(statusBar);
            Name = "FrmMain";
            Text = "Digao Runner";
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            buttonBar.ResumeLayout(false);
            buttonBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusBar;
        public RichTextBox edLog;
        private ToolStrip buttonBar;
        private ToolStripButton btnCancel;
        private System.Windows.Forms.Timer timerControl;
        private ToolStripStatusLabel stElapsed;
        public ToolStripProgressBar progressBar;
        private ToolStripStatusLabel stVersion;
        private ToolStripButton btnFont;
        public ToolStripStatusLabel stStatus;
        private ToolStripButton btnRun;
        public Panel boxFields;
        public ImageList images;
        private ToolStripStatusLabel stDigaoDalpiaz;
        private ToolStripStatusLabel stAdmin;
    }
}
