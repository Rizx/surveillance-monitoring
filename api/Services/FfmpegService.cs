using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

        public static bool Start(string camera, string command)
        {
            try
            {
                var fileInfo = new FileInfo(Path.Combine("videos", camera, "index.m3u8"));
                if (fileInfo.Exists && fileInfo.LastWriteTime.AddMinutes(1) > DateTime.Now)
                    return true;
                    
                var process = Execute(command);
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