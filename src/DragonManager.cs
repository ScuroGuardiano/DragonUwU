using System.Collections.Generic;
using System.Threading.Tasks;
using DragonsUwU.Database.Services;
using DragonsUwU.Storage;

namespace DragonsUwU
{
    class DragonManager : DiscordService.IDiscordDragonManager
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
            await dragonService.AddDragonAsync(tags, filename);
            return true;
        }
        
        /// <summary>
        /// Will return file path to Dragon if there's at least one Dragon that match tags  
        /// Otherwise it will return null
        /// </summary>
        public async Task<string> GetRandomDragonByTagsAsync(List<string> tags)
        {
            var dragon = await dragonService.FindRandomDragonAsync(tags);
            if(dragon == null)
                return null;
            string dragonPath = dragonStorage.GetFullDragonPath(dragon.Filename);
            return dragonPath;
        }
    }
}