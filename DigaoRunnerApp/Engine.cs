using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.ScriptContext;
using DigaoRunnerApp.Services;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Text.RegularExpressions;

namespace DigaoRunnerApp
{
    internal class Engine(CancellationTokenSource _cancellationTokenSource)
    {

        private FileContents _fileContents;

        class FileContents
        {
            public Dictionary<string, string> Variables;
            public string Code;
            public int CodeLineRef;
        }

        public void Execute()
        {
            LoadFile();
            ProcessVariables();
            RunScript();
        }

        private void LoadFile()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 2) throw new ValidationException("Script file not informed");

            string file = args[1];
            if (!File.Exists(file)) throw new ValidationException("Script file not found: " + file);

            var lines = File.ReadAllLines(file).ToList();

            if (lines.Count == 0) throw new ValidationException("Script file is empty");
            if (lines[0] != "@DIGAOSCRIPT") throw new ValidationException("Script file is empty");

            lines.RemoveAt(0);

            var codeIndex = lines.IndexOf("@CODE");
            if (codeIndex == -1) throw new ValidationException("Code identifier not found");

            var code = string.Join(Environment.NewLine, lines[(codeIndex + 1)..]);

            var head = lines[..codeIndex];

            Dictionary<string, string> variables = [];
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

            _fileContents = new FileContents()
            {
                Variables = variables,
                Code = code,
                CodeLineRef = codeIndex
            }; ;
        }

        private void ProcessVariables()
        {
            var variables = _fileContents.Variables;

            if (!variables.TryGetValue("VERSION", out var version)) throw new ValidationException("Version variable not found");
            if (version != "1") throw new ValidationException("Unsupported script version");

            if (variables.TryGetValue("TITLE", out var title))
            {
                LogService.SetTitle(title);
            }
        }

        private void RunScript()
        {
            var scriptOptions = ScriptOptions.Default
                .AddImports("System.IO")
                .AddImports("System.Drawing")
                .AddImports("DigaoRunnerApp.ScriptContext")
                .AddReferences(typeof(AbortException).Assembly)
                .WithEmitDebugInformation(true);

            ScriptFunctions functions = new(_cancellationTokenSource.Token);

            var script = CSharpScript.Create(code: _fileContents.Code, options: scriptOptions, globalsType: typeof(ScriptFunctions));

            LogService.SetStatus("Compiling...");
            script.Compile();

            LogService.SetStatus("Running...");
            try
            {
                var state = script.RunAsync(globals: functions);

                if (state.Exception != null)
                {
                    var inner = state.Exception.InnerException;
                    if (inner is AbortException || inner is CancelException) throw inner;

                    throw new CodeException(inner.Message + Environment.NewLine + AdjustStackTrace(inner.StackTrace));
                }
            }
            catch (CompilationErrorException ex)
            {
                throw new CodeException(ChangeLineNumber(ex.Message));
            }
        }

        private int FixLineNumber(int lineNumber)
        {
            return lineNumber + _fileContents.CodeLineRef + 2;
        }

        private string ChangeLineNumber(string msg)
        {
            string pattern = @"^\((\d+),(\d+)\)";

            return Regex.Replace(msg, pattern, match =>
            {
                int line = int.Parse(match.Groups[1].Value);
                int col = int.Parse(match.Groups[2].Value);

                string remainingText = match.Groups[3].Value;

                line = FixLineNumber(line);

                return $"({line},{col}){remainingText}";
            });
        }

        private string AdjustStackTrace(string stack)
        {
            const string PREFIX = "   at Submission#0.";
            const string MAIN_FUNC = "<<Initialize>>d__0.MoveNext()";
            var regex = new Regex(@" in :line (\d+)$");

            var lines = stack.Split(Environment.NewLine);

            List<string> externalLines = [];

            foreach (var line in lines)
            {
                if (line.StartsWith(PREFIX))
                {
                    string text = line[PREFIX.Length..];

                    if (text.StartsWith(MAIN_FUNC)) text = "Main Function" + text[MAIN_FUNC.Length..];

                    var match = regex.Match(text);
                    if (match.Success)
                    {
                        text = text[..match.Index] + " - Line " + FixLineNumber(int.Parse(match.Groups[1].Value));
                    }

                    externalLines.Add(" > " + text);
                }
            }

            return string.Join(Environment.NewLine, externalLines);
        }

    }
}
