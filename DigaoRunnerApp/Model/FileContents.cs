namespace DigaoRunnerApp.Model
{
    public class FileContents
    {
        public Variables Vars { get; set; }
        public string Code { get; set; }
        public int CodeLineRef { get; set; }

        public class Variables : Dictionary<string, string>;

        public string GetVar(string name)
        {
            if (Vars.TryGetValue(name, out var value)) return value;
            return null;
        }

        public List<Field> Fields { get; set; }
        public class Field
        {
            public Control Control;
            public string Name;
            public string ValueProp;
        }
    }
}
