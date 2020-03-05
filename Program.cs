using System;
using System.Collections.Generic;
using System.Diagnostics;
using DragonsUwU.Database;

namespace DragonsUwU
{
    class Program
    {
        static void Main(string[] arg) {
            var service = new DragonService();
            //service.AddDragon(new List<string>{"dragon", "owo", "uwu"}, "acva.dragon");
            //service.AddDragon(new List<string> { "dragon", "TwT", "uwu" }, "faf.dragon");
            var dragon = service.FindDragons(new List<string> { "dragon", "owo" });
            Console.WriteLine(dragon?.Count);
        }
    }
}
