using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Discord.WebSocket;

namespace DragonsUwU
{
    class Program
    {
        static void Main(string[] arg) {
            DragonConfiguration.LoadConfiguration();

            var service = new DragonService();
            //service.AddDragon(new List<string>{"dragon", "owo", "uwu"}, "acva.dragon");
            //service.AddDragon(new List<string> { "dragon", "TwT", "uwu" }, "faf.dragon");
            var dragon = service.FindDragons(new List<string> { "dragon", "owo" });
            Console.WriteLine(dragon?.Count);
        }
    }
}
