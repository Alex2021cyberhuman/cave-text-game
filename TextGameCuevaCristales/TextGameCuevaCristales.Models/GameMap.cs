using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextGameCuevaCristales.Models
{
    public class GameMap
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Ending> Endings { get; set; } = new List<Ending>();
        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Way> Ways { get; set; } = new List<Way>();

        public void CreateExampleMap()
        {
            Items.Add(new Item() { Id = 1, Name = "Предмет 1", Description = "Открывает переход из комнаты 2 в комнату 3.", IsDestructible = false });
            Items.Add(new Item() { Id = 2, Name = "Предмет 2", Description = "Предотвращает конец игры в комнате 4.", IsDestructible = false });
            Items.Add(new Item() { Id = 3, Name = "Предмет 3", Description = "Предотвращает конец игры в комнате 5.", IsDestructible = true });

            Endings.Add(new Ending() { Id = 1, Name = "Концовка 1", Description = "Конец игры в комнате 7",
                LoseDescription = "Сообщение конца игры. Вы проиграли." });
            Endings.Add(new Ending() { Id = 2, Name = "Концовка 2", Description = "Конец игры в комнате 4",
                LoseDescription = "Сообщение конца игры. Вы проиграли. Надо было взять Предмет 2 в комнате 3.",
                AvoidDescription = "Сообщение продолжения игры. Вы взяли и использовали Предмет 2 из комнаты 3",
                AvoidItemId = 2});
            Endings.Add(new Ending() { Id = 3, Name = "Концовка 3", Description = "Конец игры в комнате 5",
                LoseDescription = "Сообщение конца игры. Вы проиграли. Надо было взять Предмет 3 в комнате 4",
                AvoidDescription = "Сообщение продолжения игры. Вы взяли и использовали Предмет 3 из комнаты 4",
                AvoidItemId = 3});
            Endings.Add(new Ending() { Id = 4, Name = "Концовка 4", Description = "Конец игры в комнате 6",
                LoseDescription = "Сообщение конца игры. Вы выйграли."});

            Rooms.Add(new Room() { Id = 1, Name = "Комната 1", Description = "Описание комнаты 1. Start." });
            Rooms.Add(new Room() { Id = 2, Name = "Комната 2", Description = "Описание комнаты 2.", ItemId = 1 });
            Rooms.Add(new Room() { Id = 3, Name = "Комната 3", Description = "Описание комнаты 3.", ItemId = 2 });
            Rooms.Add(new Room() { Id = 4, Name = "Комната 4", Description = "Описание комнаты 4.", ItemId = 3, EndingId = 2 });
            Rooms.Add(new Room() { Id = 5, Name = "Комната 5", Description = "Описание комнаты 5.", EndingId = 3 });
            Rooms.Add(new Room() { Id = 6, Name = "Комната 6", Description = "Описание комнаты 6. End.", EndingId = 4 });
            Rooms.Add(new Room() { Id = 7, Name = "Комната 7", Description = "Описание комнаты 7.", EndingId = 1 });

            Ways.Add(new Way() { Id = 1, Name = "Переход 1", OutsideDescription = "Описание перехода 1 снаружи", InsideDescription = "Описание перехода 1 по ту сторону",
                FromRoomId = 1, ToRoomId = 2});
            Ways.Add(new Way() { Id = 2, Name = "Переход 2", OutsideDescription = "Описание перехода 2 снаружи", InsideDescription = "Описание перехода 2 по ту сторону",
                FromRoomId = 2, ToRoomId = 3, OpeningItemId = 1});
            Ways.Add(new Way() { Id = 3, Name = "Переход 3", OutsideDescription = "Описание перехода 3 снаружи", InsideDescription = "Описание перехода 3 по ту сторону",
                FromRoomId = 2, ToRoomId = 4});
            Ways.Add(new Way() { Id = 4, Name = "Переход 4", OutsideDescription = "Описание перехода 4 снаружи", InsideDescription = "Описание перехода 4 по ту сторону",
                FromRoomId = 3, ToRoomId = 4});
            Ways.Add(new Way() { Id = 5, Name = "Переход 5", OutsideDescription = "Описание перехода 5 снаружи", InsideDescription = "Описание перехода 5 по ту сторону",
                FromRoomId = 3, ToRoomId = 4});
            Ways.Add(new Way() { Id = 6, Name = "Переход 6", OutsideDescription = "Описание перехода 6 снаружи", InsideDescription = "Описание перехода 6 по ту сторону",
                FromRoomId = 4, ToRoomId = 5});
            Ways.Add(new Way() { Id = 7, Name = "Переход 7", OutsideDescription = "Описание перехода 7 снаружи", InsideDescription = "Описание перехода 7 по ту сторону",
                FromRoomId = 5, ToRoomId = 6});
            Ways.Add(new Way() { Id = 8, Name = "Переход 8", OutsideDescription = "Описание перехода 8 снаружи", InsideDescription = "Описание перехода 8 по ту сторону",
                FromRoomId = 1, ToRoomId = 7});
        }

        public void InitializeMap()
        {
            foreach (var ending in Endings)
            {
                if (ending.AvoidItemId != 0)
                {
                    ending.AvoidItem = Items.First(i => i.Id == ending.AvoidItemId);
                }
            }

            foreach (var room in Rooms)
            {
                if (room.ItemId != 0)
                {
                    room.Item = Items.First(i => i.Id == room.ItemId);
                }
                if (room.EndingId != 0)
                {
                    room.Ending = Endings.First(e => e.Id == room.EndingId);
                }
            }

            foreach (var way in Ways)
            {
                if (way.FromRoomId != 0)
                {
                    way.FromRoom = Rooms.First(x => x.Id == way.FromRoomId);
                }
                if (way.ToRoomId != 0)
                {
                    way.ToRoom = Rooms.First(x => x.Id == way.ToRoomId);
                }
            }
        }

        public async Task WriteToFileAsync(string fileName)
        {
            var opt = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };

            using (StreamWriter stringWriter = new StreamWriter(fileName))
            {
                string json = JsonSerializer.Serialize(this, opt);
                await stringWriter.WriteAsync(json);
            }
        }
        public static async Task<GameMap> ReadFromFileAsync(string fileName)
        {
            var opt = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string json = await streamReader.ReadToEndAsync();
                return JsonSerializer.Deserialize<GameMap>(json, opt);
            }
        }
    }
}
