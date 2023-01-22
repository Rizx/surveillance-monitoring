using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Options
    {
        private const string FILE_NAME = "options.json";
        public static Options Instance = new();
        public Dictionary<string, int> Manager= new();

        public static void ToFile()
        {
            try
            {
                var stream = new MemoryStream();
                var s = new DataContractJsonSerializer(typeof(Options));
                s.WriteObject(stream, Instance);
                File.WriteAllBytes(FILE_NAME, stream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception($"Options.Tofile {ex.Message}");
            }
        }

        public static void FromFile()
        {
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    var stream = File.ReadAllBytes(FILE_NAME);
                    var s = new DataContractJsonSerializer(typeof(Options));
                    Instance = (Options)s.ReadObject(new MemoryStream(stream));
                }
                else
                {
                    Instance = new Options
                    {
                        Manager = new()
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Options.FromFile {ex.Message}");
            }
        }
    }
}