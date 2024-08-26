using DigaoRunnerApp.Exceptions;

namespace DigaoRunnerApp.Services
{
    internal class ScriptLoader
    {

        public FileContents LoadFile()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 2) throw new ValidationException("Script file not informed");

            string file = args[1];
            if (!File.Exists(file)) throw new ValidationException("Script file not found: " + file);

            var lines = File.ReadAllLines(file).ToList();

            if (lines.Count == 0) throw new ValidationException("Script file is empty");
            if (lines[0] != "@DIGAOSCRIPT") throw new ValidationException("Invalid first line identifier");

            var codeIndex = lines.IndexOf("@CODE");
            if (codeIndex == -1) throw new ValidationException("Code identifier not found");

            var head = lines[1..codeIndex];
            var code = string.Join(Environment.NewLine, lines[(codeIndex + 1)..]);

            var variables = ReadVariables(head);
            ProcessVariables(variables);

            return new FileContents()
            {
                Variables = variables,
                Code = code,
                CodeLineRef = codeIndex
            };
        }

        private Variables ReadVariables(List<string> head)
        {
            Variables variables = [];
            foreach (var line in head.Select(x => x.Trim()))
            {
                if (line.StartsWith("//")) continue;

                var separator = line.IndexOf('=');
                if (separator != -1)
                {
                    string name = line[..separator].Trim();
                    string value = line[(separator + 1)..].Trim();

                    if (variables.ContainsKey(name)) throw new ValidationException($"Header variable '{name}' duplicated");

                    variables.Add(name, value);
                }
            }

            return variables;
        }

        private void ProcessVariables(Variables variables)
        {
            if (!variables.TryGetValue("VERSION", out var version)) throw new ValidationException("Version variable not found");
            if (version != "1") throw new ValidationException("Unsupported script version");

            if (variables.TryGetValue("TITLE", out var title))
            {
                LogService.Form.Text = title;
            }
        }

    }
}
