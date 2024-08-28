using DigaoRunnerApp.Services;

namespace DigaoRunnerApp
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            EdFont.Text = LogService.Form.EdLog.Font.Name;
            EdSize.Text = LogService.Form.EdLog.Font.Size.ToString();

            BoxNormalColor.BackColor = LogService.Colors.Normal;
            BoxErrorColor.BackColor = LogService.Colors.Error;

            BoxBackColor.BackColor = LogService.Form.EdLog.BackColor;

            ckWordWrap.Checked = LogService.Form.EdLog.WordWrap;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            EdFont.Text = EdFont.Text.Trim();
            if (EdFont.Text == string.Empty)
            {
                Messages.Error("Specify the font name");
                return;
            }

            LoadFonts();
            if (!EdFont.Items.Contains(EdFont.Text))
            {
                Messages.Error("Invalid font name");
                return;
            }

            EdSize.Text = EdSize.Text.Trim();
            if (EdSize.Text == string.Empty)
            {
                Messages.Error("Specify the font size");
                return;
            }

            if (!float.TryParse(EdSize.Text, out var fontSize))
            {
                Messages.Error("Invalid font size");
                return;
            }

            //

            LogService.Form.EdLog.Font = new Font(EdFont.Text, fontSize);

            LogService.Colors.Normal = BoxNormalColor.BackColor;
            LogService.Colors.Error = BoxErrorColor.BackColor;

            LogService.Form.EdLog.BackColor = BoxBackColor.BackColor;

            LogService.Form.EdLog.WordWrap = ckWordWrap.Checked;

            DialogResult = DialogResult.OK;
        }

        private void LoadFonts()
        {
            if (EdFont.Items.Count > 0) return;

            foreach (var item in FontFamily.Families)
            {
                EdFont.Items.Add(item.Name);
            }
        }

        private void EdFont_DropDown(object sender, EventArgs e)
        {
            LoadFonts();
        }

        private void ColorClick(object sender, EventArgs e)
        {
            var panel = (Panel)sender;

            ColorDialog colorDialog = new();
            colorDialog.Color = panel.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panel.BackColor = colorDialog.Color;
            }
        }

        private void BtnDefaults_Click(object sender, EventArgs e)
        {
            EdFont.Text = LogService.DEFAULT_FONT;
            EdSize.Text = LogService.DEFAULT_SIZE.ToString();

            BoxNormalColor.BackColor = LogService.DEFAULT_COLOR_NORMAL;
            BoxErrorColor.BackColor = LogService.DEFAULT_COLOR_ERROR;
            BoxBackColor.BackColor = LogService.DEFAULT_COLOR_BACK;

            ckWordWrap.Checked = LogService.DEFAULT_WORD_WRAP;
        }

    }
}
