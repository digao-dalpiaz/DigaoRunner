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
            LoadCustomization(Customization.Instance);
        }

        private void LoadCustomization(Customization customization)
        {
            EdFont.Text = customization.Font;
            EdSize.Text = customization.Size.ToString();

            BoxNormalColor.BackColor = customization.ColorNormal;
            BoxErrorColor.BackColor = customization.ColorError;
            BoxBackColor.BackColor = customization.ColorBack;

            BoxProcNormalColor.BackColor = customization.ColorProcNormal;
            BoxProcErrorColor.BackColor = customization.ColorProcError;

            ckWordWrap.Checked = customization.WordWrap;
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

            var customization = Customization.Instance;

            customization.Font = EdFont.Text;
            customization.Size = fontSize;

            customization.ColorNormal = BoxNormalColor.BackColor;
            customization.ColorError = BoxErrorColor.BackColor;
            customization.ColorBack = BoxBackColor.BackColor;

            customization.ColorProcNormal = BoxProcNormalColor.BackColor;
            customization.ColorProcError = BoxProcErrorColor.BackColor;

            customization.WordWrap = ckWordWrap.Checked;

            //

            customization.Save();
            customization.LoadVisual();

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
            LoadCustomization(new Customization());
        }

    }
}
