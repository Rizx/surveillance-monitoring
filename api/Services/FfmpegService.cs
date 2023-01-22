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
        private static Process Execute(string command)
        {
            try
            {
                return Process.Start("cmd.exe", $"/k {command}");
            }
            catch
            {
                return null;
            }
        }

        // private static Dictionary<string, Process> processActive = new Dictionary<string, Process>();

        public static bool Start(string camera, string parameters, string urlSource, string filePath)
        {
            try
            {
                // var command = $"ffmpeg -rtsp_transport tcp -i {camera.VideoUrl} -an -c:v libx264 -crf 21 -preset veryfast -fflags nobuffer -flags low_delay -t 20 -f hls -hls_time 1 -hls_list_size 3 -hls_flags delete_segments {filename}";
                var command = string.Format(parameters, urlSource, filePath);
                if(Options.Instance.Manager == null)
                    Options.Instance.Manager = new();

                if (Options.Instance.Manager.TryGetValue(camera, out int processid))
                {
                    var exist = Process.GetProcessById(processid);
                    if (exist != null)
                    {
                        exist.Kill();
                        Thread.Sleep(500);
                    }
                    Options.Instance.Manager.Remove(camera);
                }

                var process = Execute(command);
                Options.Instance.Manager.Add(camera, process.Id);
                Options.ToFile();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // public static bool Stop(string camera)
        // {
        //     try
        //     {
        //         if (!processActive.ContainsKey(camera))
        //             return true;

        //         var process = processActive[camera];
        //         process.Kill();
        //         process.Close();
        //         process.Dispose();
        //         processActive.Remove(camera);
        //         return true;
        //     }
        //     catch
        //     {
        //         return false;
        //     }
        // }
    }
}