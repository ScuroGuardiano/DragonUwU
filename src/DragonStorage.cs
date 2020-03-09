using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DragonsUwU
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

        /// <summary>
        /// It will download Dragon, if succeed will return filename  
        /// Otherwise will return null
        /// </summary>
        public async Task<string> DownloadDragon(string url)
        {
            using(var webClient = new WebClient())
            {
                string extension = Path.GetExtension(url);
                
                if(!IsExtensionLegit(extension))
                    return null;

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
            long time = DateTime.Now.Millisecond;
            return $"DRAGON_{time}{extension}";
        }
        private bool IsExtensionLegit(string extension)
        {
            return allowedExtensions.Contains(extension);
        }
    }
}