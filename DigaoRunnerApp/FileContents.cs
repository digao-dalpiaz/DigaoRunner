namespace DigaoRunnerApp
{
    public class FileContents
    {
        public Variables Variables;
        public string Code;
        public int CodeLineRef;
    }

    public class Variables : Dictionary<string, string>;
}
