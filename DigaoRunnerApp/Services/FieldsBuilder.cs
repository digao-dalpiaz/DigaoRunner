using DigaoRunnerApp.Model;
using System.Reflection;

namespace DigaoRunnerApp.Services
{
    public class FieldsBuilder(FileContents _fileContents)
    {

        public class DefControlType
        {
            public string Type;
            public Type Class;
            public string ValueProp;

            private PropertyInfo _propInfo;
            public PropertyInfo PropInfo 
            {
                get
                {
                    if (_propInfo == null) _propInfo = Class.GetProperty(ValueProp);
                    return _propInfo;
                }
            }
        }

        public readonly static List<DefControlType> DEF_CONTROLS =
        [
            new() { Type = "text", Class = typeof(TextBox), ValueProp = "Text" },
            new() { Type = "check", Class = typeof(CheckBox), ValueProp = "Checked" },
            new() { Type = "combo", Class = typeof(ComboBox), ValueProp = "Text" },
        ];

        public void BuildScreen()
        { 
            var panel = LogService.Form.boxFields;

            int y = 20;
            foreach (var field in _fileContents.Fields)
            {
                var control = (Control)Activator.CreateInstance(field.DefControlType.Class);
                field.Control = control;

                if (control is CheckBox check)
                {
                    check.Text = field.Label;
                }
                else
                {
                    if (control is ComboBox combo)
                    {
                        combo.Items.AddRange(field.Items);
                        combo.DropDownStyle = field.Editable ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
                    }

                    Label lb = new();
                    lb.Parent = panel;
                    lb.Left = 17;
                    lb.Top = y;
                    lb.Text = field.Label;
                    lb.AutoSize = true;

                    control.Width = 500;

                    y = lb.Bottom + 2;
                }

                control.Parent = panel;
                control.Left = 20;
                control.Top = y;
                y = control.Bottom + 20;

                if (field.Default != null) field.DefControlType.PropInfo.SetValue(control, field.Default);
            }
        }

    }
}
