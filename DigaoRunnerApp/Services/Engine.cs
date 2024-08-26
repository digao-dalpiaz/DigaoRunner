using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.Model;
using DigaoRunnerApp.ScriptContext;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace DigaoRunnerApp.Services
{
    public class Engine(FileContents _fileContents, ResolvedFields _resolvedFields, CancellationTokenSource _cancellationTokenSource)
    {

        public void RunScript()
        {
            var scriptOptions = ScriptOptions.Default
                .AddImports("System.IO")
                .AddImports("System.IO.Compression")
                .AddImports("System.Drawing")
                .AddImports("DigaoRunnerApp.ScriptContext")
                .AddReferences(typeof(AbortException).Assembly)
                .AddReferences(typeof(ZipFile).Assembly)
                .WithEmitDebugInformation(true);

            ScriptFunctions functions = new(_cancellationTokenSource.Token, _resolvedFields);

            var script = CSharpScript.Create(code: _fileContents.Code, options: scriptOptions, globalsType: typeof(ScriptFunctions));

            LogService.SetStatus("Compiling...", StatusType.WAIT);
            script.Compile();

            LogService.SetStatus("Running...", StatusType.WAIT);
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
            return lineNumber + _fileContents.CodeLineRef + 1;
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
