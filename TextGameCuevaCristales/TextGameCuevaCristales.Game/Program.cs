using System;
using TextGameCuevaCristales.Models;

namespace TextGameCuevaCristales.Game
{
    class Program
    {
        static async System.Threading.Tasks.Task Main()
        {
            Console.WriteLine("Hello World!");
            var game = new GameCycle();
            await game.CreateCaveFromFiles("mapItems.json", "mapEnds.json", "mapRooms.json", "mapWays.json");
            game.GamePlay();
        }
    }
}
