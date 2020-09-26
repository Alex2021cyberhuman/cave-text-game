using System;
using TextGameCuevaCristales.Models;

namespace TextGameCuevaCristales.Game
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
            var game = new GameCycle();
            game.CreateSimpleCave();
            game.GamePlay();
        }
    }
}
