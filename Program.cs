using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DragonsUwU
{
    class Program
    {
        static void Main(string[] arg) {
            var service = new DragonService();
            //service.AddDragon(new List<string>{"dragon", "owo", "uwu"}, "acva.dragon");
            //service.AddDragon(new List<string> { "dragon", "TwT", "uwu" }, "faf.dragon");
            var sw = new Stopwatch();
            
            sw.Start();
                service.FindRandomDragon(new List<string> { "dragon" });
            sw.Stop();
            Console.WriteLine("First find (with database conn init) {0}ms", sw.ElapsedMilliseconds);
            
            sw.Restart();
            for(int i = 0; i < 1000; i++) {
                service.FindRandomDragon(new List<string> { "dragon" });
            }
            sw.Stop();
            Console.WriteLine(
                "1000 random searches with one tag {0}ms ({1}ms per search)",
                sw.ElapsedMilliseconds,
                sw.ElapsedMilliseconds / 1000
            );

            sw.Restart();
            for (int i = 0; i < 1000; i++) {
                service.FindRandomDragon(new List<string> { "dragon", "owo", "uwu" });
            }
            sw.Stop();
            Console.WriteLine(
                "1000 random searches with three tag {0}ms ({1}ms per search)",
                sw.ElapsedMilliseconds,
                sw.ElapsedMilliseconds / 1000
            );

        }
    }
}
