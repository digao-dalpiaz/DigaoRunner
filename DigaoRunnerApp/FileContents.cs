namespace DigaoRunnerApp
{
    public class FileContents
    {
        public Variables Vars;
        public string Code;
        public int CodeLineRef;

        public class Variables : Dictionary<string, string>;
    }
}
