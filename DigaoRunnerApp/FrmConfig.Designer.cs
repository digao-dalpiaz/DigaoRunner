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
            label1 = new Label();
            edFont = new ComboBox();
            label2 = new Label();
            edSize = new TextBox();
            label3 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            btnDefaults = new Button();
            label5 = new Label();
            boxBackColor = new Panel();
            boxErrorColor = new Panel();
            boxNormalColor = new Panel();
            btnOk = new Button();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 40);
            label1.Name = "label1";
            label1.Size = new Size(38, 20);
            label1.TabIndex = 0;
            label1.Text = "Font";
            // 
            // edFont
            // 
            edFont.FormattingEnabled = true;
            edFont.Location = new Point(24, 64);
            edFont.Name = "edFont";
            edFont.Size = new Size(376, 28);
            edFont.TabIndex = 0;
            edFont.DropDown += edFont_DropDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(413, 40);
            label2.Name = "label2";
            label2.Size = new Size(36, 20);
            label2.TabIndex = 2;
            label2.Text = "Size";
            // 
            // edSize
            // 
            edSize.Location = new Point(416, 64);
            edSize.Name = "edSize";
            edSize.Size = new Size(96, 27);
            edSize.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(189, 112);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 4;
            label3.Text = "Error color";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 112);
            label4.Name = "label4";
            label4.Size = new Size(97, 20);
            label4.TabIndex = 5;
            label4.Text = "Normal color";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDefaults);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(boxBackColor);
            groupBox1.Controls.Add(boxErrorColor);
            groupBox1.Controls.Add(boxNormalColor);
            groupBox1.Controls.Add(edFont);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(edSize);
            groupBox1.Location = new Point(16, 16);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(536, 264);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Console";
            // 
            // btnDefaults
            // 
            btnDefaults.Location = new Point(24, 200);
            btnDefaults.Name = "btnDefaults";
            btnDefaults.Size = new Size(120, 40);
            btnDefaults.TabIndex = 10;
            btnDefaults.Text = "Load Defaults";
            btnDefaults.UseVisualStyleBackColor = true;
            btnDefaults.Click += btnDefaults_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(357, 112);
            label5.Name = "label5";
            label5.Size = new Size(126, 20);
            label5.TabIndex = 8;
            label5.Text = "Background color";
            // 
            // boxBackColor
            // 
            boxBackColor.Cursor = Cursors.Hand;
            boxBackColor.Location = new Point(360, 136);
            boxBackColor.Name = "boxBackColor";
            boxBackColor.Size = new Size(152, 40);
            boxBackColor.TabIndex = 9;
            boxBackColor.Click += ColorClick;
            // 
            // boxErrorColor
            // 
            boxErrorColor.Cursor = Cursors.Hand;
            boxErrorColor.Location = new Point(192, 136);
            boxErrorColor.Name = "boxErrorColor";
            boxErrorColor.Size = new Size(152, 40);
            boxErrorColor.TabIndex = 7;
            boxErrorColor.Click += ColorClick;
            // 
            // boxNormalColor
            // 
            boxNormalColor.Cursor = Cursors.Hand;
            boxNormalColor.Location = new Point(24, 136);
            boxNormalColor.Name = "boxNormalColor";
            boxNormalColor.Size = new Size(152, 40);
            boxNormalColor.TabIndex = 6;
            boxNormalColor.Click += ColorClick;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(160, 288);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(120, 40);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(288, 288);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmConfig
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(568, 340);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfig";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += FrmConfig_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox edFont;
        private Label label2;
        private TextBox edSize;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
        private Panel boxErrorColor;
        private Panel boxNormalColor;
        private Button btnOk;
        private Button btnCancel;
        private Label label5;
        private Panel boxBackColor;
        private Button btnDefaults;
    }
}