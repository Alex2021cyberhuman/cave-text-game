using System;
using System.Collections.Generic;
using System.Text;

namespace TextGameCuevaCristales.Models
{

    public class GameEnd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemTag { get; set; }
        public string AvoidDescription { get; set; }
        public string LoseDescription { get; set; }
        public bool IsAvoidence => ItemTag != null || ItemTag != "";
    }
}
