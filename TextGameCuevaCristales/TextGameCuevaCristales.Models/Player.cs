using System;
using System.Collections.Generic;
using System.Text;

namespace TextGameCuevaCristales.Models
{
    /// <summary>
    /// Класс игрока, который можно сериализовать для сожранения данных
    /// </summary>
    public class Player
    {
        public Room CurrentRoom { get; set; }
        public List<Item> Items { get; set; }
        public int StepCount { get; set; }
    }
}
