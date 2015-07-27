using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTracker
{
    class Sinthesizer
    {
        enum Modes { Attack, Hold, FadeOut }
        /// <summary>
        /// Количество типов волн (максимальный индекс)
        /// </summary>
        public const int NumberOfTypesOfWaves = 5;
        public Project Track;//Ссылка на трек, для внутреннего плеера
        public float Volume { get; set; }
        public bool Played; //Играется ли нота

        Channel channel;    //Ссылка на канал для внутреннего плеера
        int waveLen;        //Длина волны
        int Note;           //Нота
        int n;              //Счётчик длины волны
        int OrN;            //Счётчик ячейки орнамента
        int OrT;            //Счётчик времени ячейки орнамента
        Modes Mode;         //Режим изменения громкости (атака, удержка, затухание)
        int ModeN;          //Счётчик для режима
        float Amplitude;    //Амплитуда ноты по орнаменту
        float NoteVolume;   //Громкость ноты (выведение, затухание)
        Random RND = new Random();

        /// <summary>
        /// "Мьют", если true - канал не играется.
        /// </summary>
        public bool Mute { get; set; }

        /// <summary>
        /// Инициализация синтезатора
        /// </summary>
        /// <param name="ch">Ссылка на канал, в котором орнамент лежит</param>
        public Sinthesizer(Channel ch)
        {
            Mute = false;
            Played = false;
            channel = ch;
        }

        /// <summary>
        /// Генератор звука
        /// </summary>
        /// <returns></returns>
        public float Generator()
        {
            if (Mute | !Played) return 0;
            float x = 0;
            switch (channel.ornament[OrN].Wave)
            //switch (channel.ornament[0].Wave)
            {
                case 0:                                     //_--_--_--_-- квадрат
                    x = 1;
                    if (n < waveLen / 2) x = -1;
                    break;
                case 1:                                     ////////////// пила
                    x = (float)n/waveLen * 2 - 1;
                    break;
                case 2:                                     //\/\/\/\/\/\/ треугольник
                    x = (float)n / waveLen * 4;
                    if (x < 2) x -= 1;
                    else x = 3 - x;
                    break;
                case 3:                                     //_.-._.-._.-. синусоида
                    x = (float)Math.Sin((float)n / waveLen * 3.14159f);
                    break;
                case 4:                                     //_-.-_-_._-.- белый шум
                    x = (float)RND.Next(200) / 100 - 1;
                    break;
                case 5:                                     //_-.-_-_._-.- белый шум с тоном (не знаю как называется)
                    if (n < waveLen / 2) x = -1;
                    else x = (float)RND.Next(200) / 100 - 1;
                    break;
            }
            //Счётчик по длине волны
            n++;
            OrT++;
            if (waveLen > 0 && n >= waveLen)
            {
                n = 0;
                //Счётчик по орнаменту
                if (OrT >= 2500)                                    //В будущем сделать этот параметр изменяемым
                {
                    OrT -= 2500;
                    //Счётчик по списку орнаментов
                    OrN++;
                    if (OrN >= channel.Len) OrN = channel.Repeat;
                    {
                        if (OrN >= channel.Len) { Played = false; return 0; }
                    }
                    OrnamentGet(OrN);
                }
            }
            ModeCalc();
            Volume = Amplitude * NoteVolume * channel.Volume / 100;
            return x * Volume * 0.2f;
        }

        //Применение орнамента
        void OrnamentGet(int num)
        {
            Amplitude = (float)channel.ornament[num].Volume / 100;
            float note = Note + channel.ornament[num].Note + (float)channel.ornament[num].Frequency / 50;
            if (note < 10) note = 10;
            waveLen = (int)(Audio.SampleRate / (440 * Math.Pow(2, (double)(note - 69) / 12)));
        }

        //Вычисление режима нажатия
        void ModeCalc()
        {
            ModeN++;
            if (Mode == Modes.Attack)
            {
                NoteVolume = (float)ModeN / 96 / channel.Atack;
                if (ModeN > channel.Atack * 96) { Mode = Modes.Hold; ModeN = 0; }
            }
            if (Mode == Modes.Hold)
            {
                NoteVolume = 1;
                if (ModeN > channel.Hold * 96) { Mode = Modes.FadeOut; ModeN = 0; }
            }
            if (Mode == Modes.FadeOut & channel.FadeOut > 0)
            {
                NoteVolume = 1 - (float)ModeN / 96 / channel.FadeOut;
                if (ModeN > channel.FadeOut * 96) { Played = false; }
            }
            //NoteVolume = 1;
        }

        /// <summary>
        /// "Включение" ноты
        /// </summary>
        /// <param name="Note">Номер ноты</param>
        public void Play(int note)
        {
            //Здесь надо придумать команду остановки ноты, или сделать другой метод где Played Будет = false;
            if (note == 0) return;
            if (note == 1) { Played = false; return; }
            Note = note;
            Amplitude = 1;
            n = 0;
            OrT = 0;
            OrN = 0;
            OrnamentGet(0);
            Played = true;
            Mode = Modes.Attack;
            ModeN = 0;
        }

        /// <summary>
        /// Остановка генератора
        /// </summary>
        public void Stop()
        {
            Played = false;
        }
    }
}
