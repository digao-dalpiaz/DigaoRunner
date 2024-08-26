namespace DigaoRunnerApp
{
    public class FileContents
    {
        public Variables Vars { get; set; }
        public string Code { get; set; }
        public int CodeLineRef { get; set; }

        public class Variables : Dictionary<string, string>;
    }
}
