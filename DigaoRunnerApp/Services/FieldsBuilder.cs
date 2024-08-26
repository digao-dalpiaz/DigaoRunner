using DigaoRunnerApp.Exceptions;

namespace DigaoRunnerApp.Services
{
    public class FieldsBuilder(FileContents _fileContents)
    {

        public void BuildScreen()
        {
            _fileContents.Fields = [];

            var panel = LogService.Form.boxFields;

            int y = 20;
            foreach (var item in _fileContents.Vars.Where(x => x.Key.StartsWith('$')))
            {
                var parts = item.Value.Split("|");

                string name = item.Key[1..];
                string defaultValue = parts.Length > 2 ? parts[2] : null;
                Control control;

                string valueProp;

                if (parts.Length < 2) throw new ValidationException($"Field '{item.Key}' must contain at least two value parameters");

                var type = parts[1];
                switch (type)
                {
                    case "text":
                        TextBox ed = new();
                        control = ed;
                        ed.Width = 500;
                        ed.Text = defaultValue;

                        valueProp = "Text";
                        break;

                    case "check":
                        CheckBox ck = new();
                        control = ck;
                        ck.Text = parts[0];
                        ck.Checked = defaultValue == "true";

                        valueProp = "Checked";
                        break;

                    case "combo":
                        ComboBox combo = new();
                        control = combo;
                        combo.Width = 500;
                        if (parts.Length > 3) combo.Items.AddRange(parts[3].Split(","));
                        combo.Text = defaultValue;

                        valueProp = "Text";
                        break;

                    default:
                        throw new ValidationException($"Field '{item.Key}' contains invalid type '{type}'");
                }

                if (control is not CheckBox)
                {
                    Label lb = new();
                    lb.Parent = panel;
                    lb.Left = 17;
                    lb.Top = y;
                    lb.Text = parts[0];
                    lb.AutoSize = true;

                    y = lb.Bottom;
                }

                control.Parent = panel;
                control.Left = 20;
                control.Top = y;
                y = control.Bottom + 20;

                _fileContents.Fields.Add(new FileContents.Field
                {
                    Name = name,
                    Control = control,
                    ValueProp = valueProp
                });
            }
        }

    }
}
