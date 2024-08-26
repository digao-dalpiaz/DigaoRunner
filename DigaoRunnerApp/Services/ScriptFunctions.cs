﻿using DigaoRunnerApp.Exceptions;
using DigaoRunnerApp.Model;

namespace DigaoRunnerApp.Services
{
    public class ScriptFunctions(CancellationToken _stopToken, ResolvedFields _resolvedFields)
    {

        public readonly ResolvedFields Fields = _resolvedFields;

        public void Echo(string text, Color? color = null)
        {
            if (color == null) color = Color.LimeGreen;

            LogService.Log(text, color.Value);
        }

        public void Sleep(int ms)
        {
            Task.Delay(ms).Wait();
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

    }
}
