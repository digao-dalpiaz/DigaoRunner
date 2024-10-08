﻿using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.Model;
using Microsoft.Win32;
using System.Diagnostics;
using System.Text;

namespace DigaoRunnerApp.Services
{
    public class ScriptFunctions(ResolvedFields _resolvedFields, CancellationToken _stopToken)
    {

        private Encoding _consoleEncoding = Encoding.UTF8;

        public int LastExitCode;

        public static void Abort(string message)
        {
            throw new AbortException(message);
        }

        public object GetField(string name)
        {
            if (!_resolvedFields.TryGetValue(name, out var value))
            {
                throw new KeyNotFoundException($"Field '{name}' not found");
            }

            return value;
        }

        public T GetField<T>(string name)
        {
            return (T)GetField(name);
        }

        public void Echo(string text = null, Color? color = null)
        {
            LogService.Log(text, color ?? Color.Empty, color == null ? "N" : null);
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

        public void SetSystemConsoleEncoding()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var reg = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Nls\CodePage");
            var codePage = int.Parse((string)reg.GetValue("OEMCP"));
            var systemEncoding = Encoding.GetEncoding(codePage);

            _consoleEncoding = systemEncoding;
        }

        private ProcessStartInfo GenerateProcessInfo(string fileName, string arguments)
        {
            return new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,

                StandardOutputEncoding = _consoleEncoding,
                StandardErrorEncoding = _consoleEncoding
            };
        }

        public int RunProcess(string fileName, string arguments)
        {
            using Process p = new();
            p.StartInfo = GenerateProcessInfo(fileName, arguments);

            void DoLine(DataReceivedEventArgs e, string kind)
            {
                LogService.Log(e.Data, Color.Empty, "PROC_N"); 

                if (_stopToken.IsCancellationRequested) p.Kill();
            }

            p.OutputDataReceived += (sender, e) => DoLine(e, "PROC_N");
            p.ErrorDataReceived += (sender, e) => DoLine(e, "PROC_E");

            p.Start();

            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            p.WaitForExit();

            CheckStop();

            LastExitCode = p.ExitCode;
            return p.ExitCode;
        }

        public int RunProcessReadOutput(string fileName, string arguments, ref string output)
        {
            using Process p = new();
            p.StartInfo = GenerateProcessInfo(fileName, arguments);

            p.Start();
            p.WaitForExit();

            output = p.StandardOutput.ReadToEnd();

            CheckStop();

            LastExitCode = p.ExitCode;
            return p.ExitCode;
        }

    }
}
