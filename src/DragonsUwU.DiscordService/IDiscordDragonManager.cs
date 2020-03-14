using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonsUwU.DiscordService
{
    interface IDiscordDragonManager
    {
        /// <summary>
        /// Will add Dragon to database
        /// If adding is not supported or failed just return false
        /// </summary>
        Task<bool> AddDragonAsync(List<string> tags, string url);

        /// <summary>
        /// Will return file path to random Dragon if there's at least one Dragon that match tags  
        /// Otherwise it will return null, this function shouldn't throw any exception  
        ///  
        /// Important! It must be local filepath, not an url, url will propably not work here ^^
        /// </summary>
        Task<string> GetRandomDragonByTagsAsync(List<string> tags);
    }
}