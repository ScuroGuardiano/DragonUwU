using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DragonsUwU.Storage
{
    class DragonStorage
    {
        private string storagePath;
        private List<string> allowedExtensions;

        public DragonStorage(string storagePath, List<string> allowedExtensions)
        {
            this.storagePath = storagePath;
            this.allowedExtensions = allowedExtensions;
        }

        public string GetFullDragonPath(string filename)
        {
            return Path.Join(storagePath, filename);
        }
        /// <summary>
        /// It will download Dragon, if succeed will return filename  
        /// Otherwise will return null
        /// </summary>
        public async Task<string> DownloadDragonAsync(string url)
        {
            using(var webClient = new WebClient())
            {
                string extension = Path.GetExtension(url);
                if(!IsExtensionLegit(extension))
                    return null;
                if(!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }
                string filename = GenerateFilename(extension);
                string pathToSave = Path.Join(storagePath, filename);
                try
                {
                    await webClient.DownloadFileTaskAsync(url, pathToSave);
                    return filename;
                }
                catch(Exception ex) {
                    Console.WriteLine("Some shit failed: {0}", ex);
                    return null;
                }
            }
        }

        private string GenerateFilename(string extension)
        {
            double time = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return $"DRAGON_{time.ToString().Replace(',', '_')}{extension}";
        }
        private bool IsExtensionLegit(string extension)
        {
            return allowedExtensions.Contains(extension);
        }
    }
}