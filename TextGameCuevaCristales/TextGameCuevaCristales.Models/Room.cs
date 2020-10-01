using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TextGameCuevaCristales.Models
{
    /// <summary>
    /// Класс узла направленного граффа. Комната в которой может лежать предмет и произойти событие прерывающее игру.
    /// </summary>
    public class Room
    {
        /// <value>ID комнаты, с помощью которого другие объекты ассоциируют себя с ней</value>
        public int Id { get; set; } = 0;
        /// <value>Название комнаты, выводится на экран</value>
        public string Name { get; set; } = string.Empty;
        /// <value>Описание комнаты</value>
        public string Description { get; set; } = string.Empty;
        /// <value>ID концовки. Если 0 - то концовки не произойдет</value>
        public int EndingId { get; set; } = 0;
        [JsonIgnore] public Ending Ending { get; set; } = null;
        /// <value>ID предмета который переместится игроку в инвентарь. Если 0, то игроку ничего не попадет.</value>
        public int ItemId { get; set; } = 0;
        [JsonIgnore] public Item Item { get; set; } = null;
    }
}
