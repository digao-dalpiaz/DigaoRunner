using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.Model;
using System.Diagnostics;

namespace DigaoRunnerApp.Services
{
    public class ScriptFunctions(CancellationToken _stopToken, ResolvedFields _resolvedFields)
    {

        public object GetField(string name)
        {
            if (!_resolvedFields.TryGetValue(name, out var value))
            {
                throw new KeyNotFoundException($"Field '{name}' not found");
            }

            return value;
        }

        public void Echo(string text, Color? color = null)
        {
            if (color == null) color = Color.LimeGreen;

            LogService.Log(text, color.Value);
            CheckStop();
        }

        public void Sleep(int ms)
        {
            Task.Delay(ms).Wait();
            CheckStop();
        }

        public void CheckStop()
        {
            if (_stopToken.IsCancellationRequested) throw new CancelException("Script cancelled");
        }

        public void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            if (!File.Exists(sourceFilePath)) throw new FileNotFoundException("File not found: " + sourceFilePath);

            LogService.SwitchProgress(true);
            try
            {
                const int bufferSize = 1024 * 1024; // 1MB buffer
                byte[] buffer = new byte[bufferSize];
                long totalBytes = 0;

                using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                {
                    var fileLength = sourceStream.Length;

                    int bytesRead;
                    while ((bytesRead = sourceStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        destinationStream.Write(buffer, 0, bytesRead);
                        totalBytes += bytesRead;

                        LogService.Progress(totalBytes, fileLength);
                        CheckStop();
                    }
                }

                File.SetLastWriteTime(destinationFilePath, File.GetLastWriteTime(sourceFilePath));
            }
            finally
            {
                LogService.SwitchProgress(false);
            }
        }

        public int RunProcess(string fileName, string arguments)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process p = new();
            p.StartInfo = processInfo;

            p.OutputDataReceived += (sender, e) => { LogService.Log(e.Data, Color.White); if (_stopToken.IsCancellationRequested) p.Kill(); };
            p.ErrorDataReceived += (sender, e) => { LogService.Log(e.Data, Color.Brown); if (_stopToken.IsCancellationRequested) p.Kill(); };

            p.Start();

            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            p.WaitForExit();

            CheckStop();

            return p.ExitCode;
        }

    }
}
