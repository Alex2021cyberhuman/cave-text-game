using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace TextGameCuevaCristales.Models
{
    /// <summary>
    /// Представляет собой класс перехода между комнатами. По сути это ребро направленного графа.
    /// Может быть открытым или запертым. Зависит от id предмета
    /// </summary>
    public class Way
    {
        /// <value>ID перехода</value>
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string OutsideDescription { get; set; } = string.Empty;
        public string InsideDescription { get; set; } = string.Empty;
        /// <value>ID предмета необходимого для открытия перехода (двери к примеру)</value>
        public int OpeningItemId { get; set; } = 0;
        [JsonIgnore] public Item OpeningItem { get; set; } = null;
        public int FromRoomId { get; set; } = 0;
        [JsonIgnore] public Room ToRoom { get; set; } = null;
        public int ToRoomId { get; set; } = 0;
        [JsonIgnore] public Room FromRoom { get; set; } = null;

        [JsonIgnore] public bool IsLocked => OpeningItemId != 0;
    }
}
