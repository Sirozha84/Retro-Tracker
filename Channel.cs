using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTracker
{
    class Channel
    {
        //Настройки канала... бла бла бла.. ещё не придумал что тут сделать пря щас
        //В будущем тут будет тип и настройки волны, и возможно семплы
        public const int VolumeMin = 0;     //Интервал Громкостей
        public const int VolumeMax = 100;   
        public const int VolumeStep = 5;
        public const int NoteMin = -24;     //Интервал нот
        public const int NoteMax = 24;
        public const int FreqMin = -100;    //Интервал искажения частот
        public const int FreqMax = 100;
        public const int FreqStep = 5;
        public Ornament[] ornament;
        public int Volume = 100;            //Мастер-громкость канала в микшере
        public int Atack = 0;               //Параметры синтезатора
        public int Hold = 500;
        public int FadeOut = 500;
        public int Len = 1;
        public int Repeat = 0;
        /// <summary>
        /// Конструктор канала
        /// </summary>
        public Channel()
        {
            ornament = new Ornament[Audio.MaxChannels];
            for (int i = 0; i < Audio.MaxChannels; i++)
            {
                ornament[i].Wave = 0;
                ornament[i].Volume = 100;
                ornament[i].Note = 0;
                ornament[i].Frequency = 0;
            }
        }

        public Channel Copy()
        {
            return (Channel)this.MemberwiseClone();
        }
    }


    struct Ornament
    {
        public int Wave;
        public int Volume;
        public int Note;
        public int Frequency;
    }
}
