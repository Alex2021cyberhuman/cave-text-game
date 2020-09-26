using System;
using System.Collections.Generic;
using System.Text;

namespace TextGameCuevaCristales.Models
{
    public class Way
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OutsideDescription { get; set; }
        public string InsideDescription { get; set; }
        public Room From { get; set; }
        public Room To { get; set; }
        public string ItemTag { get; set; }
        public bool CompareTag(string tags) => tags.Contains(ItemTag);

        public bool IsLocked => ItemTag != "";
    }
}
