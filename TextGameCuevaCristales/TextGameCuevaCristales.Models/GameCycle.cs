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
        public async Task CreateCaveFromFiles(string itemsFileName, string endsFileName, string roomsFileName, string waysFileName)
        {
            Item GetItem(int id) => Items.First(x => x.Id == id);
            Room GetRoom(int id) => Rooms.First(x => x.Id == id);
            GameEnd GetEnd(int id) => Ends.First(x => x.Id == id);

            
            var opt = new JsonSerializerOptions();
            opt.WriteIndented = true;
            opt.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
            using (StreamReader sr = new StreamReader(itemsFileName))
            {
                string json = await sr.ReadToEndAsync();
                Items = JsonSerializer.Deserialize<List<Item>>(json, opt);
            }
            using (StreamReader sr = new StreamReader(endsFileName))
            {
                string json = await sr.ReadToEndAsync();
                Ends = JsonSerializer.Deserialize<List<GameEnd>>(json, opt);
            }
            using (StreamReader sr = new StreamReader(roomsFileName))
            {
                string json = await sr.ReadToEndAsync();
                Rooms = JsonSerializer.Deserialize<List<Room>>(json, opt);
            }
            using (StreamReader sr = new StreamReader(waysFileName))
            {
                string json = await sr.ReadToEndAsync();
                Ways = JsonSerializer.Deserialize<List<Way>>(json, opt);
            }

            foreach (var room in Rooms)
            {
                if (room.ItemsId != null)
                {
                    room.Items = new List<Item>();
                    foreach (var itemId in room.ItemsId)
                    {
                        room.Items.Add(GetItem(itemId));
                    }
                }
                if (room.EndId != int.MinValue)
                {
                    room.End = GetEnd(room.EndId);
                }
            }

            foreach (var way in Ways)
            {
                if (way.FromId != int.MinValue)
                {
                    way.From = GetRoom(way.FromId);
                }
                if (way.ToId != int.MinValue)
                {
                    way.To = GetRoom(way.ToId);
                }
            }

            Player = new Player()
            {
                CurrentRoom = Rooms.First(x => x.Id == 1),
                Items = new List<Item>(),
                StepCount = 0
            };


        }

        public async Task CreateSimpleCaveAsync()
        {
            Item GetItem(int id) => Items.First(x => x.Id == id);
            Room GetRoom(int id) => Rooms.First(x => x.Id == id);
            GameEnd GetEnd(int id) => Ends.First(x => x.Id == id);

            Items = new List<Item>()
            {
                new Item() {Id = 1, Name = "Солдацкое копье", IsDestructible = true, Tag = "копье оружие"},
                new Item() {Id = 2, Name = "Ржавыый топор", IsDestructible = true, Tag = "топор оружие"},
                new Item() {Id = 3, Name = "Ключ от единственной двери закрытой на ключ", IsDestructible = false, Tag = "keyA"},
                new Item() {Id = 4, Name = "Антидот", IsDestructible = true, Tag = "антидот"},
                new Item() {Id = 5, Name = "Святая вода", IsDestructible = true, Tag = "вода"},
            };

            Ends = new List<GameEnd>()
            {
                new GameEnd() 
                {
                    Id = 1, Name = "Нападение бандита",
                    Description = "На вас напал бандит с киркой.",
                    AvoidDescription = "Вы сумели отразить нападение бандита с помошью оружия.",
                    LoseDescription = "Вас убил бандит.", ItemTag = "оружие"
                },
                new GameEnd()
                {
                    Id = 2, Name = "Нападение медведя",
                    Description = "На вас напал разяренный медведь.",
                    AvoidDescription = "Вы сумели заколоть это животное.", 
                    LoseDescription = "Вас задрал медведь.", ItemTag = "копье"
                },
                new GameEnd()
                {
                    Id = 3, Name = "Нападение оборотня",
                    Description = "На вас напал оборотень. Против него помог бы антидот. Оружие, похоже, бессильно.",
                    AvoidDescription = "Вы вкололи антидот и он превратился в человека, лег на пол и умер.",
                    LoseDescription = "Вас задрал оборотень.", ItemTag = "антидот"
                },
                new GameEnd()
                {
                    Id = 4, Name = "Нападение призрака",
                    Description = "На вас налетел призрак.",
                    AvoidDescription = "Вы обрызгали его святой водой и он исчез.",
                    LoseDescription = "Вас превратили в призрака.", ItemTag = "вода"
                },
                new GameEnd()
                {
                    Id = 5, Name = "Спасение",
                    Description = "Пред вами открылся путь на свододу.",
                    LoseDescription = "Вы не думая выбегаете на свежий воздух.",
                },
            };

            Rooms = new List<Room>()
            {
                new Room()
                {
                    Id = 1, Name = "Старт",
                    Description = "Вы оказались в мрачном подземелье."
                },
                new Room()
                {
                    Id = 2, Name = "Пещера с топором",
                    Description = "Мрачное подземелье. На полу лежит ржавый топор. Вы его подобрали.",
                    ItemsId = new List<int>() {2}
                    
                },
                new Room()
                {
                    Id = 3, Name = "Комната с копьем",
                    Description = "Вы оказались в комнате. Темно, Но вы смогли найти копье.",
                    ItemsId = new List<int>() {1}
                },
                new Room()
                {
                    Id = 4, Name = "Лагерь разбойника",
                    Description = "Вы оказались в мрачном подземелье. В этом зале расхититель гробниц устроил лаагерь.",
                    EndId =1               
                },
                new Room()
                {
                    Id = 5, Name = "Комната с ключем А",
                    Description = "Вы оказались в мрачном подземелье.",
                    ItemsId = new List<int>() {3}

                },
                new Room()
                {
                    Id = 6, Name = "Комната с медведем",
                    Description = "Вы оказались в мрачном подземелье.",
                    EndId = 2
                },
                new Room()
                {
                    Id = 7, Name = "Выход",
                    Description = "Вы оказались в мрачном подземелье.",
                    EndId = 5
                },
                new Room()
                {
                    Id = 8, Name = "Антидот",
                    Description = "Вы оказались в мрачном подземелье.",
                    ItemsId = new List<int>() {4}
                },
                new Room()
                {
                    Id = 9, Name = "Призрак",
                    Description = "Вы оказались в мрачном подземелье.",
                    EndId = 4
                },
                new Room()
                {
                    Id = 10, Name = "Мутант",
                    Description = "Вы оказались в мрачном подземелье.",
                    EndId = 3
                },
                new Room()
                {
                    Id = 11, Name = "Алтарь",
                    Description = "Вы оказались в мрачном подземелье.",
                    ItemsId = new List<int>() {5}
                },
                new Room()
                {
                    Id = 12, Name = "Kopie",
                    Description = "Вы оказались в мрачном подземелье.",
                    ItemsId = new List<int>() {1}
                }
            };

            Ways = new List<Way>()
            {
                new Way()
                {
                    Id = 1,
                    FromId = (1),
                    ToId = (2),
                    InsideDescription = "go",
                    OutsideDescription = "topoe",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 2,
                    FromId = (2),
                    ToId = (3),
                    InsideDescription = "xxx",
                    OutsideDescription = "копье",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 3,
                    FromId = (2),
                    ToId = (4),
                    InsideDescription = "xxx",
                    OutsideDescription = "разб",
                    Name = "][][][",
                    ItemTag = "топор"
                },
                new Way()
                {
                    Id = 4,
                    FromId = (4),
                    ToId = (5),
                    InsideDescription = "xxx",
                    OutsideDescription = "к комнате а",
                    Name = "][][][",
                    ItemTag = "топор"
                },
                new Way()
                {
                    Id = 5,
                    FromId = (5),
                    ToId = (6),
                    InsideDescription = "xxx",
                    OutsideDescription = "medved",
                    Name = "][][][",
                    ItemTag = "keyA"
                },
                new Way()
                {
                    Id = 6,
                    FromId = (6),
                    ToId = (7),
                    InsideDescription = "xxx",
                    OutsideDescription = "vihod",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 7,
                    FromId = (5),
                    ToId = (8),
                    InsideDescription = "xxx",
                    OutsideDescription = "ant",
                    Name = "][][][",
                    ItemTag = "keyA"
                },
                new Way()
                {
                    Id = 8,
                    FromId = (8),
                    ToId = (9),
                    InsideDescription = "xxx",
                    OutsideDescription = "prizrak",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 9,
                    FromId = (9),
                    ToId = (12),
                    InsideDescription = "xxx",
                    OutsideDescription = "kopie 2",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 10,
                    FromId = (8),
                    ToId = (10),
                    InsideDescription = "xxx",
                    OutsideDescription = "mutant",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 11,
                    FromId = (10),
                    ToId = (11),
                    InsideDescription = "xxx",
                    OutsideDescription = "altar",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 12,
                    FromId = (11),
                    ToId = (9),
                    InsideDescription = "xxx",
                    OutsideDescription = "prizrak",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 13,
                    FromId = (12),
                    ToId = (5),
                    InsideDescription = "xxx",
                    OutsideDescription = "kom a",
                    Name = "][][][",
                    ItemTag = ""
                },
                new Way()
                {
                    Id = 14,
                    FromId = (3),
                    ToId = (4),
                    InsideDescription = "xxx",
                    OutsideDescription = "копье",
                    Name = "][][][",
                    ItemTag = ""
                },

            };
            var opt = new JsonSerializerOptions();
            opt.WriteIndented = true;
            opt.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
            using (StreamWriter stringWriter = new StreamWriter("mapItems.json"))
            {
                string json = JsonSerializer.Serialize(Items, opt);
                await stringWriter.WriteAsync(json);
            }

            using (StreamWriter stringWriter = new StreamWriter("mapEnds.json"))
            {
                string json = JsonSerializer.Serialize(Ends, opt);
                await stringWriter.WriteAsync(json);
            }

            using (StreamWriter stringWriter = new StreamWriter("mapRooms.json"))
            {
                string json = JsonSerializer.Serialize(Rooms, opt);
                await stringWriter.WriteAsync(json);
            }

            using (StreamWriter stringWriter = new StreamWriter("mapWays.json"))
            {
                string json = JsonSerializer.Serialize(Ways, opt);
                await stringWriter.WriteAsync(json);
            }

            foreach (var room in Rooms)
            {
                if (room.ItemsId != null)
                {
                    room.Items = new List<Item>();
                    foreach (var itemId in room.ItemsId)
                    {
                        room.Items.Add(GetItem(itemId));
                    }
                }
                if (room.EndId != int.MinValue)
                {
                    room.End = GetEnd(room.EndId);
                }
            }

            foreach (var way in Ways)
            {
                if (way.FromId != int.MinValue)
                {
                    way.From = GetRoom(way.FromId);
                }
                if (way.ToId != int.MinValue)
                {
                    way.To = GetRoom(way.ToId);
                }
            }

            Player = new Player()
            {
                CurrentRoom = Rooms.First(x => x.Id == 1),
                Items = new List<Item>(),
                StepCount = 0
            };

            
        }

        public void GamePlay()
        {
            void PrintGameEnd()
            {
                Console.WriteLine("Конец...");
                Console.WriteLine($"Вы совершили {Player.StepCount} шага.");

                Environment.Exit(0);
            }
            void CanGameEnd(GameEnd end)
            {

                if (end != null)
                {
                    Console.WriteLine(end.Name);
                    Console.WriteLine(end.Description);
                    Item item = Player.Items.FirstOrDefault(x => x.Tag.Contains(end.ItemTag));
                    if (end.IsAvoidence && item != null)
                    {
                        Console.WriteLine(end.AvoidDescription);
                        if (item.IsDestructible)
                        {
                            Player.Items.Remove(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine(end.LoseDescription);
                        PrintGameEnd();
                    }
                }
            }

            static void PrintItem(Item item)
            {                
                Console.WriteLine($"{item.Name}:{item.Id}");
            }

            static void PrintWays(List<Way> ways)
            {
                for (int i = 0; i < ways.Count; i++)
                {
                    Console.WriteLine($"{i} {ways[i].Name}");
                    Console.WriteLine(ways[i].OutsideDescription);
                }
            }
            Way GetEnteredWay(List<Way> waysFromRoom)
            {
                do
                {
                    Console.Write("Введите номер выхода: ");
                    string answerLine = Console.ReadLine();
                    if (int.TryParse(answerLine, out int answer) && answer >= 0 && answer <= waysFromRoom.Count)
                    {
                        Item item = Player.Items.FirstOrDefault(x => x.Tag.Contains(waysFromRoom[answer].ItemTag));
                        if (!waysFromRoom[answer].IsLocked || item != null)
                        {
                            if (item != null && item.IsDestructible)
                            {
                                Player.Items.Remove(item);
                            }
                            return waysFromRoom[answer];
                        }
                        else
                        {
                            Console.WriteLine("У вас нет необходимой вещи что бы открыть этот путь");
                        }
                    }
                    Console.WriteLine("Неправельный номер");
                } while (true);
                
            }

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

                CanGameEnd(currentRoom.End);
                if (currentRoom.Items != null)
                {
                    Player.Items.AddRange(currentRoom.Items);
                    currentRoom.Items = null;
                }               
                
                Console.WriteLine("У вас есть:");
                Player.Items.ForEach(PrintItem);

                List<Way> waysFromRoom = Ways.Where(x => x.From.Id == currentRoom.Id).ToList();

                PrintWays(waysFromRoom);

                Way enteredWay = GetEnteredWay(waysFromRoom);

                Console.WriteLine(enteredWay.InsideDescription);
                Player.CurrentRoom = enteredWay.To;
                Player.StepCount++;
            }
        }


        public Player Player { get; set; }

        public List<Room> Rooms { get; set; }
        public List<Way> Ways { get; set; }
        public List<Item> Items { get; set; }
        public List<GameEnd> Ends { get; set; }
    }
}
