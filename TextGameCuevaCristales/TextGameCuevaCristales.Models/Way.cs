using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TextGameCuevaCristales.Models
{
    [Serializable]
    public class Way
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OutsideDescription { get; set; }
        public string InsideDescription { get; set; }
        public int FromId { get; set; } = int.MinValue;
        [JsonIgnore]
        public Room From { get; set; }
        public int ToId { get; set; } = int.MinValue;
        [JsonIgnore]
        public Room To { get; set; }
        public string ItemTag { get; set; }
        public bool CompareTag(string tags) => tags.Contains(ItemTag);

        public bool IsLocked => ItemTag != "";
    }
}
