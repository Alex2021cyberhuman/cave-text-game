using System;
using System.Collections.Generic;
using System.Text;

namespace TextGameCuevaCristales.Models
{
    public class Player
    {
        public Room CurrentRoom { get; set; }
        public List<Item> Items { get; set; }
        public int StepCount { get; set; }
    }
}
