using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextGameCuevaCristales.Models
{
    public class GameCycle
    {
        private void PrintWays(List<Way> waysFromRoom)
        {
            Console.WriteLine("Вы можете выбрать дорогу.");
            for (int i = 0, count = waysFromRoom.Count; i < count; i++)
            {
                Console.WriteLine($"{i + 1}:***{waysFromRoom[i].Name}***");
                Console.WriteLine($"описание: {waysFromRoom[i].OutsideDescription}");
            }
        }

        private void PrintGameEnd()
        {
            Console.WriteLine("Конец...");
            Console.WriteLine($"Вы совершили {Player.StepCount} шага.");

            Environment.Exit(0);
        }

        private void InitiateEnding(Ending end)
        {

            if (end != null)
            {
                Console.WriteLine($"{end.Name}");
                Console.WriteLine($"описание: {end.Description}");

                if (end.IsAvoidence && Player.Items.FirstOrDefault(i => i.Id == end.AvoidItemId) is Item item && ConsoleAdditions.AskYesOrNo($"Вы хотите испльзовать '{item.Name}' что бы избежать '{end.Name}'?"))
                {
                    if (item.IsDestructible)
                    {
                        Player.Items.Remove(item);
                    }
                    return;
                }
                Console.WriteLine(end.LoseDescription);
                PrintGameEnd();
            }
        }

        private Way GetEnteredWay(List<Way> waysFromRoom)
        {
            do
            {
                Console.Write("Введите номер выхода: ");
                string answerLine = Console.ReadLine();
                if (int.TryParse(answerLine, out int answer) && answer > 0 && answer <= waysFromRoom.Count)
                {
                    Way choisedWay = waysFromRoom[answer - 1];
                    if (choisedWay.IsLocked)
                    {
                        if (Player.Items.FirstOrDefault(i => i.Id == choisedWay.OpeningItemId) is Item item && ConsoleAdditions.AskYesOrNo($"Вы хотите испльзовать '{item.Name}' что бы пройти по '{choisedWay.Name}'?"))
                        {
                            if (item.IsDestructible)
                            {
                                Player.Items.Remove(item);
                            }
                            return choisedWay;
                        }
                        else
                        {
                            Console.WriteLine($"У вас нет {choisedWay.OpeningItem.Name} что бы воспользоваться этим путем.");
                        }
                    }
                    else if (ConsoleAdditions.AskYesOrNo($"Вы хотите испльзовать '{choisedWay.Name}'? Возможно, назад дороги нет."))
                    {
                        return choisedWay;
                    }
                }
                Console.WriteLine("Неправильный номер");
            } while (true);

        }

        public void InitializePlayer()
        {
            Player.CurrentRoom = GameMap.Rooms.First(r => r.Id == 1);
            Player.StepCount = 0;
            Player.Items = new List<Item>();
        }

        public void GamePlay()
        {


            Console.WriteLine("**************************************************");
            Console.WriteLine("     Добро пожаловать в игру Cueva Cristales      ");
            Console.WriteLine("**************************************************");
            Console.WriteLine("Вы играет за персонажа который должен");
            Console.WriteLine("выбратся из подземелья");
            Console.WriteLine("Игра может закончится в любую минуту");
            while (true)
            {
                Room currentRoom = Player.CurrentRoom;
                Console.WriteLine(currentRoom.Name);
                Console.WriteLine(currentRoom.Description);

                InitiateEnding(currentRoom.Ending);
                if (currentRoom.ItemId != 0)
                {
                    Player.Items.Add(currentRoom.Item);
                    currentRoom.Item = null;
                    currentRoom.ItemId = 0;
                }

                Console.WriteLine("У вас есть:");
                foreach (var item in Player.Items)
                {
                    Console.WriteLine($"{item.Name} {item.Description}");
                }

                List<Way> waysFromRoom = GameMap.Ways.Where(x => x.FromRoomId == currentRoom.Id).ToList();

                PrintWays(waysFromRoom);

                Way enteredWay = GetEnteredWay(waysFromRoom);

                Console.WriteLine(enteredWay.InsideDescription);
                Player.CurrentRoom = enteredWay.ToRoom;
                Player.StepCount++;
            }
        }

        public Player Player { get; set; } = new Player();
        public GameMap GameMap { get; set; } = new GameMap();
    }
}
