using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TextGameCuevaCristales.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EndId { get; set; } = int.MinValue;
        [JsonIgnore]
        public GameEnd End { get; set; }
        public List<int> ItemsId { get; set; }
        [JsonIgnore]
        public List<Item> Items { get; set; }
    }
}
