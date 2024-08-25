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
            stVersion = new ToolStripStatusLabel();
            stElapsed = new ToolStripStatusLabel();
            stStatus = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            buttonBar = new ToolStrip();
            btnCancel = new ToolStripButton();
            btnFont = new ToolStripButton();
            timerControl = new System.Windows.Forms.Timer(components);
            images = new ImageList(components);
            statusBar.SuspendLayout();
            buttonBar.SuspendLayout();
            SuspendLayout();
            // 
            // edLog
            // 
            edLog.BackColor = Color.FromArgb(30, 30, 30);
            edLog.BorderStyle = BorderStyle.None;
            edLog.Dock = DockStyle.Fill;
            edLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            statusBar.Items.AddRange(new ToolStripItem[] { stVersion, stElapsed, stStatus, progressBar });
            statusBar.Location = new Point(0, 502);
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(987, 26);
            statusBar.TabIndex = 1;
            // 
            // stVersion
            // 
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
            buttonBar.Items.AddRange(new ToolStripItem[] { btnCancel, btnFont });
            buttonBar.Location = new Point(0, 0);
            buttonBar.Name = "buttonBar";
            buttonBar.Size = new Size(987, 27);
            buttonBar.TabIndex = 2;
            buttonBar.Text = "toolStrip1";
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
            btnFont.Size = new Size(119, 24);
            btnFont.Text = "Font Settings";
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
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(987, 528);
            Controls.Add(edLog);
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
        private ImageList images;
    }
}
