using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace RetroTracker
{
    static class Audio
    {
        public const int MaxChannels = 32;      //Максимальное количество каналов
        public const int SampleRate = 96000;    //Частота дискретизации
        public static WaveOut waveOut;
        public static Player player;
        public static int ChanCount;                //Количество каналов

        /// <summary>
        /// Первоначальная инициализация звуковой системы
        /// </summary>
        public static void InitAudio()
        {
            player = new Player();
            player.SetWaveFormat(SampleRate, 1);
            waveOut = new WaveOut();
            waveOut.DesiredLatency = 200; // длина буфера /2=50 миллисекунд
            waveOut.Init(player);
        }
        /// <summary>
        /// Инициализация каналов синтезатора
        /// </summary>
        public static void InitAudio(Project tr)
        {
            ChanCount = tr.ChannelCount;
            for (int i = 0; i < MaxChannels; i++) player.sinthesizer[i] = new Sinthesizer(tr.Channels[i]);
            waveOut.Play();
        }

        /// <summary>
        /// Воспроизведение ноты в треке
        /// </summary>
        /// <param name="tr">Ссылка на проект</param>
        /// <param name="pn">Номер паттерна</param>
        /// <param name="nn">Номер ноты</param>
        public static void PlayTrack(Project tr, int pn, int nn)
        {
            for (int i = 0; i < MaxChannels; i++)
                player.sinthesizer[i].Play(tr.Patterns[tr.Track[pn]].Note[i, nn]);
        }
    }

    abstract class WaveProvider32 : IWaveProvider
    {
        private WaveFormat waveFormat;

        public WaveProvider32() : this(44100, 1) { }

        public WaveProvider32(int sampleRate, int channels) { SetWaveFormat(sampleRate, channels); }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int samplesRequired = count / 4;
            int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
            return samplesRead * 4;
        }

        public abstract int Read(float[] buffer, int offset, int sampleCount);

        public WaveFormat WaveFormat
        {
            get { return waveFormat; }
        }
    }

    class Player : WaveProvider32
    {
        public enum Modes { Stop, PlayTrack, PlayPattern };
        public Sinthesizer[] sinthesizer = new Sinthesizer[Audio.MaxChannels];
        //Переменные для плеера
        Project Track;
        public Modes Mode = Modes.Stop;
        public int PatternNum;  //Текущий номер паттерна
        public int NoteNum;     //Текущая нота
        int NoteTimeLen;    //Время играния ноты
        int NoteTime;       //Текущее время играния

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            for (int i = 0; i < sampleCount; i++)
            {
                //Если включен плеер, смотрим, не включилась ли следующая нота (ну и прочие подсчёты)
                if (Mode != Modes.Stop)
                {
                    if (NoteTime == 0)
                    {
                        for (int j = 0; j < Audio.ChanCount; j++)
                            sinthesizer[j].Play(Track.Patterns[Track.Track[PatternNum]].Note[j, NoteNum]);
                    }
                    NoteTime++;
                    if (NoteTime >= NoteTimeLen)
                    {
                        //Следующая нота
                        CalculateTrackNote();
                        NoteTime = 0;
                    }
                }
                //Заполняем буфер микшируя все каналы
                buffer[i + offset] = 0;
                for (int j = 0; j < Audio.ChanCount; j++) buffer[i + offset] += sinthesizer[j].Generator();
            }
            return sampleCount;
        }

        /// <summary>
        /// Высчитывание следующей ноты с учётом режима воспроизведения и режима повторов трека
        /// </summary>
        void CalculateTrackNote()
        {
            NoteNum++;
            if (NoteNum >= Track.Patterns[Track.Track[PatternNum]].Len)
            {
                NoteNum = 0;
                if (Mode == Modes.PlayPattern) return; //Если играем только паттерн
                PatternNum++;
                if (PatternNum >= Track.TrackLen) Mode = Modes.Stop; //Конец трека, но в последствии может быть зацыкливание
            }
        }

        /// <summary>
        /// Проигрывание трека
        /// </summary>
        /// <param name="track">Ссылка на текущий проект</param>
        /// <param name="mode">Режим проигрывания</param>
        /// /// <param name="pattern">Номер первого паттерна</param>
        /// /// <param name="num">Номер первой ноты</param>
        public void Play(Project track, Modes mode, int pattern, int num)
        {
            Track = track;
            Mode = mode;
            PatternNum = pattern;
            NoteNum = num;
            NoteTimeLen = Audio.SampleRate * 60 / Track.Tempo / Track.TaktLen; //Расчитываем длину играния ноты
            NoteTime = 0;
        }

        public void Stop()
        {
            Mode = Modes.Stop;
        }
    }
}
