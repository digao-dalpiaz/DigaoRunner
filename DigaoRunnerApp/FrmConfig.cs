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
            edFont.Text = LogService.Form.edLog.Font.Name;
            edSize.Text = LogService.Form.edLog.Font.Size.ToString();

            boxNormalColor.BackColor = LogService.Colors.Normal;
            boxErrorColor.BackColor = LogService.Colors.Error;

            boxBackColor.BackColor = LogService.Form.edLog.BackColor;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            edFont.Text = edFont.Text.Trim();
            if (edFont.Text == string.Empty)
            {
                MessageBox.Show("Specify the font name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadFonts();
            if (!edFont.Items.Contains(edFont.Text))
            {
                MessageBox.Show("Invalid font name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            edSize.Text = edSize.Text.Trim();
            if (edSize.Text == string.Empty)
            {
                MessageBox.Show("Specify the font size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(edSize.Text, out var fontSize))
            {
                MessageBox.Show("Invalid font size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //

            LogService.Form.edLog.Font = new Font(edFont.Text, fontSize);

            LogService.Colors.Normal = boxNormalColor.BackColor;
            LogService.Colors.Error = boxErrorColor.BackColor;

            LogService.Form.edLog.BackColor = boxBackColor.BackColor;

            DialogResult = DialogResult.OK;
        }

        private void LoadFonts()
        {
            if (edFont.Items.Count > 0) return;

            foreach (var item in FontFamily.Families)
            {
                edFont.Items.Add(item.Name);
            }
        }

        private void edFont_DropDown(object sender, EventArgs e)
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

    }
}
