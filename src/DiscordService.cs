using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;

namespace DragonsUwU
{
    class DiscordService
    {
        public string CommandPrefix { get; set; } = "$$";

        private DiscordSocketClient client;
        private List<ulong> administrators;
        private DragonService dragonService;

        public DiscordService(List<ulong> administrators)
        {
            this.administrators = administrators;
            dragonService = new DragonService();

            var settings = new DiscordSocketConfig {
                LogLevel = LogSeverity.Info
            };

            client = new DiscordSocketClient(settings);
            client.Log += Log;
        }
        public async Task SetThisShitUpAsync(string discordToken)
        {
            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();
        }
        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine("Discord: {0}", msg.ToString());
            return Task.CompletedTask;
        }
        private async Task OnMessageReceivedAsync(SocketMessage message) {
            if (message.Author.Id == client.CurrentUser.Id)
                return;
            
            if(message.Content.Equals(CommandPrefix + "ping", StringComparison.CurrentCultureIgnoreCase))
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
            else
            {
                if(message.Channel.GetType() == typeof(SocketTextChannel))
                    await ProcessTextChannelMessage(message, (SocketTextChannel)message.Channel);
                if(message.Channel.GetType() == typeof(SocketDMChannel))
                    await ProcessDirectMessage(message, (SocketDMChannel)message.Channel);
            }
        }

        // Will take random Dragon that match tags
        // Message format: $$tag1, tag2, tag3...
        private async Task ProcessTextChannelMessage(
            SocketMessage message, 
            SocketTextChannel channel
        ) {
            if(!message.Content.StartsWith(CommandPrefix))
                return;

            List<string> tags = new List<string>(message.Content.Split(","));
            tags = tags.ConvertAll(el => el.Trim());

            await SendRandomDragon(tags, channel);
        }

        // If the message contains image and sender is bot administrator
        // It will Add new dragon with tags specified in message
        // If message contains only tags it will send random dragon ^^
        // Prefix is not used here
        private async Task ProcessDirectMessage(
            SocketMessage message,
            SocketDMChannel channel
        ) {
            
        }

        private async Task SendRandomDragon(List<string> tags, ISocketMessageChannel channel)
        {
            await channel.SendMessageAsync("Shit not implemented yet");
        }
    }
}