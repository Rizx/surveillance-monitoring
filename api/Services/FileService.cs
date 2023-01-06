using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public static class FileService
    {
        public static void WriteImage(string fullPath, byte[] image)
        {
            FileInfo fi = new(fullPath);
            fi.Directory.Create();
            using var fs = fi.Create();
            fs.Write(image, 0, image.Length);
        }
    }
}