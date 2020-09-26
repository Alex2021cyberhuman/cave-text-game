using System;
using System.Collections.Generic;

namespace TextGameCuevaCristales.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GameEnd End { get; set; }
        public List<Item> Items { get; set; }
    }
}
