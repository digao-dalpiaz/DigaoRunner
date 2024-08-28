namespace DigaoRunnerApp
{
    partial class FrmConfig
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
            LbFont = new Label();
            EdFont = new ComboBox();
            LbSize = new Label();
            EdSize = new TextBox();
            LbErrorColor = new Label();
            LbNormalColor = new Label();
            BoxConsole = new GroupBox();
            BoxProcErrorColor = new Panel();
            BoxProcNormalColor = new Panel();
            LbProcErrorColor = new Label();
            LbProcNormalColor = new Label();
            ckWordWrap = new CheckBox();
            BtnDefaults = new Button();
            LbBackColor = new Label();
            BoxBackColor = new Panel();
            BoxErrorColor = new Panel();
            BoxNormalColor = new Panel();
            BtnOK = new Button();
            BtnCancel = new Button();
            BoxConsole.SuspendLayout();
            SuspendLayout();
            // 
            // LbFont
            // 
            LbFont.AutoSize = true;
            LbFont.Location = new Point(21, 40);
            LbFont.Name = "LbFont";
            LbFont.Size = new Size(38, 20);
            LbFont.TabIndex = 0;
            LbFont.Text = "Font";
            // 
            // EdFont
            // 
            EdFont.FormattingEnabled = true;
            EdFont.Location = new Point(24, 64);
            EdFont.Name = "EdFont";
            EdFont.Size = new Size(376, 28);
            EdFont.TabIndex = 0;
            EdFont.DropDown += EdFont_DropDown;
            // 
            // LbSize
            // 
            LbSize.AutoSize = true;
            LbSize.Location = new Point(413, 40);
            LbSize.Name = "LbSize";
            LbSize.Size = new Size(36, 20);
            LbSize.TabIndex = 2;
            LbSize.Text = "Size";
            // 
            // EdSize
            // 
            EdSize.Location = new Point(416, 64);
            EdSize.Name = "EdSize";
            EdSize.Size = new Size(96, 27);
            EdSize.TabIndex = 1;
            // 
            // LbErrorColor
            // 
            LbErrorColor.AutoSize = true;
            LbErrorColor.Location = new Point(189, 112);
            LbErrorColor.Name = "LbErrorColor";
            LbErrorColor.Size = new Size(79, 20);
            LbErrorColor.TabIndex = 4;
            LbErrorColor.Text = "Error color";
            // 
            // LbNormalColor
            // 
            LbNormalColor.AutoSize = true;
            LbNormalColor.Location = new Point(21, 112);
            LbNormalColor.Name = "LbNormalColor";
            LbNormalColor.Size = new Size(97, 20);
            LbNormalColor.TabIndex = 5;
            LbNormalColor.Text = "Normal color";
            // 
            // BoxConsole
            // 
            BoxConsole.Controls.Add(BoxProcErrorColor);
            BoxConsole.Controls.Add(BoxProcNormalColor);
            BoxConsole.Controls.Add(LbProcErrorColor);
            BoxConsole.Controls.Add(LbProcNormalColor);
            BoxConsole.Controls.Add(ckWordWrap);
            BoxConsole.Controls.Add(BtnDefaults);
            BoxConsole.Controls.Add(LbBackColor);
            BoxConsole.Controls.Add(BoxBackColor);
            BoxConsole.Controls.Add(BoxErrorColor);
            BoxConsole.Controls.Add(BoxNormalColor);
            BoxConsole.Controls.Add(EdFont);
            BoxConsole.Controls.Add(LbErrorColor);
            BoxConsole.Controls.Add(LbNormalColor);
            BoxConsole.Controls.Add(LbFont);
            BoxConsole.Controls.Add(LbSize);
            BoxConsole.Controls.Add(EdSize);
            BoxConsole.Location = new Point(16, 16);
            BoxConsole.Name = "BoxConsole";
            BoxConsole.Size = new Size(536, 336);
            BoxConsole.TabIndex = 0;
            BoxConsole.TabStop = false;
            BoxConsole.Text = "Console";
            // 
            // BoxProcErrorColor
            // 
            BoxProcErrorColor.Cursor = Cursors.Hand;
            BoxProcErrorColor.Location = new Point(192, 216);
            BoxProcErrorColor.Name = "BoxProcErrorColor";
            BoxProcErrorColor.Size = new Size(152, 40);
            BoxProcErrorColor.TabIndex = 15;
            BoxProcErrorColor.Click += ColorClick;
            // 
            // BoxProcNormalColor
            // 
            BoxProcNormalColor.Cursor = Cursors.Hand;
            BoxProcNormalColor.Location = new Point(24, 216);
            BoxProcNormalColor.Name = "BoxProcNormalColor";
            BoxProcNormalColor.Size = new Size(152, 40);
            BoxProcNormalColor.TabIndex = 14;
            BoxProcNormalColor.Click += ColorClick;
            // 
            // LbProcErrorColor
            // 
            LbProcErrorColor.AutoSize = true;
            LbProcErrorColor.Location = new Point(189, 192);
            LbProcErrorColor.Name = "LbProcErrorColor";
            LbProcErrorColor.Size = new Size(79, 20);
            LbProcErrorColor.TabIndex = 12;
            LbProcErrorColor.Text = "Error color";
            // 
            // LbProcNormalColor
            // 
            LbProcNormalColor.AutoSize = true;
            LbProcNormalColor.Location = new Point(21, 192);
            LbProcNormalColor.Name = "LbProcNormalColor";
            LbProcNormalColor.Size = new Size(97, 20);
            LbProcNormalColor.TabIndex = 13;
            LbProcNormalColor.Text = "Normal color";
            // 
            // ckWordWrap
            // 
            ckWordWrap.AutoSize = true;
            ckWordWrap.Location = new Point(384, 224);
            ckWordWrap.Name = "ckWordWrap";
            ckWordWrap.Size = new Size(104, 24);
            ckWordWrap.TabIndex = 11;
            ckWordWrap.Text = "Word wrap";
            ckWordWrap.UseVisualStyleBackColor = true;
            // 
            // BtnDefaults
            // 
            BtnDefaults.Location = new Point(24, 280);
            BtnDefaults.Name = "BtnDefaults";
            BtnDefaults.Size = new Size(120, 40);
            BtnDefaults.TabIndex = 10;
            BtnDefaults.Text = "Load Defaults";
            BtnDefaults.UseVisualStyleBackColor = true;
            BtnDefaults.Click += BtnDefaults_Click;
            // 
            // LbBackColor
            // 
            LbBackColor.AutoSize = true;
            LbBackColor.Location = new Point(357, 112);
            LbBackColor.Name = "LbBackColor";
            LbBackColor.Size = new Size(126, 20);
            LbBackColor.TabIndex = 8;
            LbBackColor.Text = "Background color";
            // 
            // BoxBackColor
            // 
            BoxBackColor.Cursor = Cursors.Hand;
            BoxBackColor.Location = new Point(360, 136);
            BoxBackColor.Name = "BoxBackColor";
            BoxBackColor.Size = new Size(152, 40);
            BoxBackColor.TabIndex = 9;
            BoxBackColor.Click += ColorClick;
            // 
            // BoxErrorColor
            // 
            BoxErrorColor.Cursor = Cursors.Hand;
            BoxErrorColor.Location = new Point(192, 136);
            BoxErrorColor.Name = "BoxErrorColor";
            BoxErrorColor.Size = new Size(152, 40);
            BoxErrorColor.TabIndex = 7;
            BoxErrorColor.Click += ColorClick;
            // 
            // BoxNormalColor
            // 
            BoxNormalColor.Cursor = Cursors.Hand;
            BoxNormalColor.Location = new Point(24, 136);
            BoxNormalColor.Name = "BoxNormalColor";
            BoxNormalColor.Size = new Size(152, 40);
            BoxNormalColor.TabIndex = 6;
            BoxNormalColor.Click += ColorClick;
            // 
            // BtnOK
            // 
            BtnOK.Location = new Point(160, 360);
            BtnOK.Name = "BtnOK";
            BtnOK.Size = new Size(120, 40);
            BtnOK.TabIndex = 1;
            BtnOK.Text = "OK";
            BtnOK.UseVisualStyleBackColor = true;
            BtnOK.Click += BtnOk_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.DialogResult = DialogResult.Cancel;
            BtnCancel.Location = new Point(288, 360);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(120, 40);
            BtnCancel.TabIndex = 2;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmConfig
            // 
            AcceptButton = BtnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = BtnCancel;
            ClientSize = new Size(568, 412);
            Controls.Add(BtnCancel);
            Controls.Add(BtnOK);
            Controls.Add(BoxConsole);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfig";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += FrmConfig_Load;
            BoxConsole.ResumeLayout(false);
            BoxConsole.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label LbFont;
        private ComboBox EdFont;
        private Label LbSize;
        private TextBox EdSize;
        private Label LbErrorColor;
        private Label LbNormalColor;
        private GroupBox BoxConsole;
        private Panel BoxErrorColor;
        private Panel BoxNormalColor;
        private Button BtnOK;
        private Button BtnCancel;
        private Label LbBackColor;
        private Panel BoxBackColor;
        private Button BtnDefaults;
        private CheckBox ckWordWrap;
        private Panel BoxProcErrorColor;
        private Panel BoxProcNormalColor;
        private Label LbProcErrorColor;
        private Label LbProcNormalColor;
    }
}