using DigaoRunnerApp.Services;
using System.Text.Json.Serialization;

namespace DigaoRunnerApp.Model
{
    public class FileContents
    {
        public string Path { get; set; }

        public Variables Vars { get; set; }
        public List<Field> Fields { get; set; }
        public string Code { get; set; }
        public int CodeLineRef { get; set; }

        public class Variables : Dictionary<string, string>;

        public string GetVar(string name)
        {
            if (Vars.TryGetValue(name, out var value)) return value;
            return null;
        }

        public class Field
        {
            [JsonIgnore]
            public string Name;

            public string Label;
            public string Type;
            public object Default;
            public string[] Items;
            public bool Editable;

            [JsonIgnore]
            public Control Control;

            [JsonIgnore]
            public FieldsBuilder.DefControlType DefControlType;
        }
    }
}
