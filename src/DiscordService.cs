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

        public DiscordService(List<ulong> administrators)
        {
            this.administrators = administrators;

            var settings = new DiscordSocketConfig {
                LogLevel = LogSeverity.Info
            };

            client = new DiscordSocketClient(settings);
            client.Log += Log;
            client.MessageReceived += OnMessageReceivedAsync;
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
        // Message format: $$tag1 tag2 tag3...
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
        // Prefix is not used here, so format is: tag1 tag2 tag3... and image in attachment
        private async Task ProcessDirectMessage(
            SocketMessage message,
            SocketDMChannel channel
        ) {
            List<Attachment> attachments = message.Attachments.ToList();
            if(attachments.Count > 0)
            {
                if(administrators.Contains(message.Author.Id))
                {
                    if(message.Content.Trim() == "")
                    {
                        await channel.SendMessageAsync("You can't post dragon without tags");
                        return;
                    }
                    List<string> tags = message.Content.Split(" ").ToList();
                    Console.WriteLine(string.Join(" ", tags));
                    Console.WriteLine(attachments[0].Url);
                    //We can add fucking dragon here
                }
            }
        }

        private async Task SendRandomDragon(List<string> tags, ISocketMessageChannel channel)
        {
            await channel.SendMessageAsync("Shit not implemented yet");
        }
    }
}