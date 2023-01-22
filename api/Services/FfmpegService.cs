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
        public static bool Start(string camera, string parameters, string urlSource, string filePath)
        {
            try
            {
                var command = string.Format(parameters, urlSource, filePath);
                Options.FromFile();
                Options.Instance.Manager ??= new();
                if (Options.Instance.Manager.TryGetValue(camera, out int processid))
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
                    Options.Instance.Manager.Remove(camera);
                }

                var process = Process.Start("ffmpeg.exe", command);
                Options.Instance.Manager.Add(camera, process.Id);
                Options.ToFile();
                return true;
            }
            catch
            {
                Options.ToFile();
                return false;
            }
        }
    }
}