using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DragonsUwU
{
    class Program
    {
        static async Task Main(string[] arg) {
            DragonConfiguration.LoadConfiguration();
            var config = DragonConfiguration.Config;

            var dragonService = new Database.Services.DragonService();
            var dragonStorage = new DragonStorage(config.DragonStoragePath, config.AllowedExtensions);
            var dragonManager = new DragonManager(dragonService, dragonStorage);

            var discordService = new DiscordService(config.AdministratorIds, dragonManager);
            await discordService.SetThisShitUpAsync(DragonConfiguration.Config.DiscordToken);

            await Task.Delay(-1);
        }
    }
}
