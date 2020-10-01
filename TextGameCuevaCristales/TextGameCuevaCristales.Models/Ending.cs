using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TextGameCuevaCristales.Models
{
    /// <summary>
    /// Представляет класс возможной концовки. Концовка может быть хорошей как успешный побег героя из пещер, 
    /// так и плохой, как гибель главного героя от отравления, потому что у него не было противогаза.
    /// Указанную концовку можно избежать если есть нужный предмет. (как во 2 примере "противогаз")
    /// </summary>
    public class Ending
    {
        /// <value>ID концовки</value>
        public int Id { get; set; } = 0;
        /// <value>Название концовки</value>
        public string Name { get; set; } = string.Empty;
        /// <value>Описание которое игрок видит в момент когда он попадает в комнату с концовкой</value>
        public string Description { get; set; } = string.Empty;
        /// <value>Описание выводимое игроку при применении предмета для продолжения игры.</value>
        public string AvoidDescription { get; set; } = string.Empty;
        /// <value>Описание выводимое игроку при окончании игры.</value>
        public string LoseDescription { get; set; } = string.Empty;
        /// <value>ID предмета необходимого для продолжения игры. Если 0 - конец игры нельзя отменить.</value>
        public int AvoidItemId { get; set; } = 0;
        [JsonIgnore] public Item AvoidItem { get; set; } = null;
        public bool IsAvoidence => AvoidItemId != 0;
    }
}
