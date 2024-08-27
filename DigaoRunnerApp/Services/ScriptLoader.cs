using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.Model;
using Newtonsoft.Json;
using static DigaoRunnerApp.Model.FileContents;

namespace DigaoRunnerApp.Services
{
    public class ScriptLoader
    {

        public static FileContents LoadFile()
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
            var fields = ReadFields(variables);

            return new FileContents()
            {
                Path = file,
                Vars = variables,
                Fields = fields,
                Code = code,
                CodeLineRef = codeIndex
            };
        }

        private static Variables ReadVariables(List<string> head)
        {
            Variables variables = new();
            foreach (var line in head.Select(x => x.Trim()))
            {
                if (line.StartsWith("//")) continue;

                var separator = line.IndexOf('=');
                if (separator != -1)
                {
                    string name = line[..separator].Trim();
                    string value = line[(separator + 1)..].Trim();

                    if (name.Contains(' ')) throw new ValidationException($"Header variable '{name}' must not contain spaces");

                    if (variables.ContainsKey(name)) throw new ValidationException($"Header variable '{name}' duplicated");

                    variables.Add(name, value);
                }
            }

            //validate version
            if (!variables.TryGetValue("VERSION", out var version)) throw new ValidationException("Version variable not found");
            if (version != "1") throw new ValidationException("Unsupported script version");

            return variables;
        }

        private static List<Field> ReadFields(Variables variables)
        {
            List<Field> fields = [];

            foreach (var variable in variables.Where(x => x.Key.StartsWith('$')))
            {
                try
                {
                    Field field;
                    try
                    {
                        field = JsonConvert.DeserializeObject<Field>(variable.Value);
                    }
                    catch (Exception ex)
                    {
                        throw new ValidationException("Error reading JSON: " + ex.Message);
                    }
                    
                    var defControlType = FieldsBuilder.DEF_CONTROLS.Find(x => x.Type == field.Type);
                    if (defControlType == null) throw new ValidationException($"Invalid type '{field.Type}'");

                    if (field.Default != null)
                    {
                        var valueType = field.Default.GetType();
                        var expectedType = defControlType.PropInfo.PropertyType;

                        if (expectedType != valueType) throw new ValidationException(
                            $"Expected default value of type '{expectedType.Name}', but found '{valueType.Name}' instead");
                    }

                    field.Name = variable.Key[1..];
                    field.DefControlType = defControlType;
                    fields.Add(field);
                }
                catch (ValidationException ex) 
                {
                    throw new ValidationException($"Failed to read field '{variable.Key}': {ex.Message}");
                }
                
            }

            return fields;
        }

    }
}
