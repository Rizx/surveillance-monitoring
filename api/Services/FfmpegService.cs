using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public static class FfmpegService
    {
        private static readonly Dictionary<string, int> processActive = new ();

        public static bool Start(string camera, string parameters, string urlSource, string filePath)
        {
            try
            {
                var command = string.Format(parameters, urlSource, filePath);
                if (processActive.TryGetValue(camera, out int processid))
                {
                    try
                    {
                        var exist = Process.GetProcessById(processid);
                        if (exist != null)
                        {
                            exist.Kill();
                            Thread.Sleep(500);
                        }
                    }
                    catch { }
                    processActive.Remove(camera);
                }

                var process = Process.Start("ffmpeg.exe", command);
                processActive.Add(camera, process.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}