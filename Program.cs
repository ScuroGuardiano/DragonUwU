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
            var discordService = new DiscordService(DragonConfiguration.Config.AdministratorIds);
            await discordService.SetThisShitUpAsync(DragonConfiguration.Config.DiscordToken);

            await Task.Delay(-1);
        }
    }
}
