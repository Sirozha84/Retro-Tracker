using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTracker
{
    class Pattern
    {
        /// <summary>
        /// Максимальная длина паттерна
        /// </summary>
        public const int MaxPatternLen = 256;
        /// <summary>
        /// Матрица нот [Канал, время]
        /// </summary>
        public byte[,] Note = new byte[Audio.MaxChannels, MaxPatternLen];
        /// <summary>
        /// Длина паттерна
        /// </summary>
        public int Len;
        /// <summary>
        /// Конструктор паттерна
        /// </summary>
        /// <param name="length">Длина</param>
        public Pattern(int length) { Len = length; }
        /// <summary>
        /// Изменение размера паттерна в абсолютной величине
        /// </summary>
        /// <param name="lenhth">Новый размер</param>
        public void Resize(int length) { Len = length; }
        /// <summary>
        /// Изменение размера в относительной величине
        /// </summary>
        /// <param name="inc">Размер инкремента</param>
        public void ResizeBy(int inc)
        {
            Len += inc;
            if (Len < 4) Len = 4;
            if (Len > MaxPatternLen) Len = MaxPatternLen;
        }
    }
}
