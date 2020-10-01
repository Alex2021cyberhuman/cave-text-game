using System;
using System.IO;
using System.Threading.Tasks;
using TextGameCuevaCristales.Models;

namespace TextGameCuevaCristales.Game
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Hello World!");
            var game = new GameCycle();
            /*if (File.Exists("GameMap.json"))
            {
                game.GameMap = await GameMap.ReadFromFileAsync("GameMap.json");
            }
            else
            {
                game.GameMap.CreateExampleMap();
                await game.GameMap.WriteToFileAsync("GameMap.json");
            }*/
            game.GameMap.CreateExampleMap();
            await game.GameMap.WriteToFileAsync("GameMap.json");
            game.GameMap.InitializeMap();
            game.InitializePlayer();
            game.GamePlay();
        }
    }
}
