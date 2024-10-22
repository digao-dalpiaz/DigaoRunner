namespace DigaoRunnerApp.Theme
{
    internal class MyToolStripRenderer : ToolStripSystemRenderer
    {
        public static void ConfigToolStrip(ToolStrip toolStrip)
        {
            toolStrip.Padding = new Padding(4);
            toolStrip.BackColor = Color.Black;
            toolStrip.ForeColor = Color.White;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.ShowItemToolTips = false;

            foreach (ToolStripItem item in toolStrip.Items)
            {
                item.Padding = new Padding(4);
            }

            toolStrip.Renderer = new MyToolStripRenderer();
        }

        public static void ConfigStatusStrip(StatusStrip statusStrip)
        {
            statusStrip.Padding = new Padding(4, 2, 4, 6);
            statusStrip.BackColor = Color.Black;

            foreach (ToolStripItem item in statusStrip.Items)
            {
                item.Padding = new Padding(0, 0, 8, 0);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.GetType() == typeof(ToolStrip))
            {
                // skip render border
            }
            else
            {
                // do render border
                base.OnRenderToolStripBorder(e);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                Rectangle rectangle = new(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                e.Graphics.FillRectangle(new SolidBrush(ControlPaint.Light(e.Item.Pressed ? Color.FromArgb(40, 40, 40) : Color.Black, 0.4f)), rectangle);
                e.Graphics.DrawRectangle(new Pen(ControlPaint.Light(Color.Black, 1)), rectangle);
                return;
            }

            base.OnRenderButtonBackground(e);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            OnRenderButtonBackground(e);
        }

    }
}
