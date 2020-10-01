using System;
using System.Collections.Generic;
using System.Text;

namespace TextGameCuevaCristales.Models
{

    /// <summary>
    /// Представляет собой клас предмета. Предметы получает игрок в комнате и тратит их в переходах и для избежания концовки.
    /// </summary>
    public class Item
    {        
        /// <value>ID предмета по которому он определяется другими классами</value>
        public int Id { get; set; } = 0;
        /// <value>Название предмета. Выводится на экран</value>
        public string Name { get; set; } = string.Empty;
        /// <value>Описание предмета. Выводится на экран</value>
        public string Description { get; set; } = string.Empty;
        /// <value>Если предмет разрушаемый, то после пременения он изымается у игрока. Иначе остается.</value>
        public bool IsDestructible { get; set; } = false;
    }
}
