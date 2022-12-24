using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public static class CameraService
    {
        // public static List<byte[]> CaptureImage(IEnumerable<Camera> cameras)
        // {
        //     var result = new List<byte[]>();

        //     foreach(var camera in cameras)
        //     {
        //         try
        //         {
        //             HttpWebRequest webRequest = WebRequest.Create(camera.FotoUrl) as HttpWebRequest;
        //             webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
        //             webRequest.Credentials = new NetworkCredential(camera.UserName, camera.Password);
        //             webRequest.PreAuthenticate = true;

        //             using WebResponse myResp = webRequest.GetResponse();
        //             using MemoryStream memoryStream = new();
        //             using Stream stream = myResp.GetResponseStream();
        //             stream.CopyTo(memoryStream);
        //             result.Add(memoryStream.ToArray());
        //         }
        //         catch {
        //         }
        //     }
        //     return result;
        // }

        public static byte[] CaptureImage(Camera camera)
        {
            try
            {
                HttpWebRequest webRequest = WebRequest.Create(camera.FotoUrl) as HttpWebRequest;
                webRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                webRequest.Credentials = new NetworkCredential(camera.UserName, camera.Password);
                webRequest.PreAuthenticate = true;

                using WebResponse myResp = webRequest.GetResponse();
                using MemoryStream memoryStream = new();
                using Stream stream = myResp.GetResponseStream();
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }
    }
}