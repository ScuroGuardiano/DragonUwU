using System.Collections.Generic;
using System.Threading.Tasks;
using DragonsUwU.Database.Services;

namespace DragonsUwU
{
    class DragonManager
    {
        private DragonService dragonService;
        private DragonStorage dragonStorage;

        public DragonManager(DragonService dragonService, DragonStorage dragonStorage)
        {
            this.dragonService = dragonService;
            this.dragonStorage = dragonStorage;
        }
        public async Task<bool> AddDragonAsync(List<string> tags, string url)
        {
            string filename = await dragonStorage.DownloadDragonAsync(url);
            if(filename == null)
                return false;
            dragonService.AddDragon(tags, filename);
            return true;
        }
    }
}