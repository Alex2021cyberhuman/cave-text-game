using System;
using System.Collections.Generic;
using System.Text;

namespace TextGameCuevaCristales.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public bool IsDestructible { get; set; }
    }
}
