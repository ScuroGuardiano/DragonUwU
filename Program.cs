using System;
using System.Collections.Generic;

namespace DragonsUwU
{
    class Program
    {
        static void Main(string[] arg) {
            var service = new DragonService();
            //service.AddDragon(new List<string>{"dragon", "owo", "uwu"}, "acva.dragon");
            //service.AddDragon(new List<string> { "dragon", "TwT", "uwu" }, "faf.dragon");
            service.FindDragons(new List<string>{"TwT", "owo"})
                .ForEach(d => Console.WriteLine(d.FileName));
        }
    }
}
