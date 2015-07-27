using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroTracker
{
    class Project
    {
        public static string FileName;
        public static bool Changed;
        //Константы
        public const int MaxPatternsCound = 100; //Максимальное количество паттернов
        public const int MaxTrackLenght = 1000; //Максимальная длина трека
        //Данные проекта (теги)
        public string SongName;
        public string AlbumName;
        public string AutorName;
        //Сам проект
        public int Tempo; //Темп
        public int PatternLen;//Длина паттерна по умолчанию
        public int[] Track; //Сет патернов - то есть сам трек
        public int TrackLen; //Количество паттернов в треке
        public int ChannelCount;    //Количество каналов в проекте
        public int TaktLen;
        public List<Pattern> Patterns = new List<Pattern>(); //Собственно, главное хранилище проекта, коллекция всех паттернов
        public List<Channel> Channels = new List<Channel>(); //Каналы проекта

        //Копирование объекта для создание истории
        /*public Project Copy()
        {
            Project other = (Project)this.MemberwiseClone();
            other.id
        }*/

        //Новый проект
        public void NewProject()
        {
            FileName = Editor.FileUnnamed;
            Changed = false;

            //Создание нового документа (вначале теги и общие настройки по умолчанию)
            SongName = "Новый трек";        
            AlbumName = "Новый альбом";
            AutorName = "Неизвестный автор";
            Tempo = 100;                            //Темп по умолчанию
            PatternLen = 64;                        //Длина паттерна по умолчанию
            TaktLen = 4;                            //Длина такта
            ChannelCount = 4;
            //Сам документ
            Track = new int[MaxTrackLenght];                 //Сбрасываем паттернсет
            TrackLen = 1;                           //Устанавливаем начальную длину трека
            Patterns.Clear();
            Patterns.Add(new Pattern(PatternLen));  //Создаём первый паттерн в коллекции
            Track[0] = 0;                           //Указываем первый паттерн в паттернсете равный первый в коллекции
            Channels.Clear();                       //Добавляем несколько каналов
            for (int i = 0; i < Audio.MaxChannels; i++)            
                Channels.Add(new Channel());
            Audio.InitAudio(this);

            TrackLen = 1;

            /*Patterns[0] = new Pattern(4);
            Patterns.Add(new Pattern(6));
            Patterns.Add(new Pattern(8));
            Patterns.Add(new Pattern(10));
            Track[1] = 3;
            Track[2] = 2;
            Track[3] = 1;*/
        }
        //Сохранение проекта
        public bool Save()
        {
            try
            {
                System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(FileName, System.IO.FileMode.Create));
                //Версия формата файла
                file.Write("RTR");
                file.Write(0);
                //Информация о треке
                file.Write(SongName);
                file.Write(AlbumName);
                file.Write(AutorName);
                file.Write(Tempo);
                file.Write(PatternLen);
                file.Write(TaktLen);
                //Информация о дорожках
                file.Write(ChannelCount);
                //foreach(Channel ch in Channels)
                for (int i = 0; i < ChannelCount; i++)
                {
                    file.Write(Channels[i].Volume);
                    file.Write(Channels[i].Atack);
                    file.Write(Channels[i].Hold);
                    file.Write(Channels[i].FadeOut);
                    file.Write(Channels[i].Len);
                    file.Write(Channels[i].Repeat);
                    for (int j = 0; j < Channels[i].Len; j++)
                    {
                        file.Write(Channels[i].ornament[j].Wave);
                        file.Write(Channels[i].ornament[j].Volume);
                        file.Write(Channels[i].ornament[j].Note);
                        file.Write(Channels[i].ornament[j].Frequency);
                    }
                }
                //Трек
                file.Write(TrackLen);
                for (int i = 0; i < TrackLen; i++) file.Write(Track[i]);
                //Паттерны
                file.Write(Patterns.Count);
                foreach (Pattern pat in Patterns)
                {
                    file.Write(pat.Len);
                    for (int i = 0; i < pat.Len; i++)
                        for (int j = 0; j < ChannelCount; j++)
                            file.Write(pat.Note[j, i]);
                }
                file.Close();
                //System.Windows.Forms.MessageBox.Show("Проект сохранён в файл " + FileName);
                return true;
            }
            catch
            {
                Editor.Error("Произошла ошибка во время сохранения файла. Файл не сохранён.");
                return false;
            }
        }
        //Загрузка проекта
        public bool Open()
        {
            try
            {
                System.IO.BinaryReader file = new System.IO.BinaryReader(new System.IO.FileStream(FileName, System.IO.FileMode.Open));
                if (file.ReadString() !="RTR")
                {
                    System.Windows.Forms.MessageBox.Show("Файл " + FileName +" не поддерживается");
                    file.Close();
                    return false;
                }
                int Version = file.ReadInt32();
                if (Version == 0)
                {
                    //Информация о теке
                    SongName = file.ReadString();
                    AlbumName = file.ReadString();
                    AutorName = file.ReadString();
                    Tempo = file.ReadInt32();
                    PatternLen = file.ReadInt32();
                    TaktLen = file.ReadInt32();
                    //Информация о дорожках
                    ChannelCount = file.ReadInt32();
                    for (int i = 0; i < ChannelCount; i++)
                    {
                        Channels[i] = new Channel();
                        Channels[i].Volume = file.ReadInt32();
                        Channels[i].Atack = file.ReadInt32();
                        Channels[i].Hold = file.ReadInt32();
                        Channels[i].FadeOut = file.ReadInt32();
                        Channels[i].Len = file.ReadInt32();
                        Channels[i].Repeat = file.ReadInt32();
                        for (int j = 0; j < Channels[i].Len; j++)
                        {
                            Channels[i].ornament[j].Wave = file.ReadInt32();
                            Channels[i].ornament[j].Volume = file.ReadInt32();
                            Channels[i].ornament[j].Note = file.ReadInt32();
                            Channels[i].ornament[j].Frequency = file.ReadInt32();
                        }
                    }
                    //Трек
                    Track = new int[65536];
                    TrackLen = file.ReadInt32();
                    for (int i = 0; i < TrackLen; i++) Track[i] = file.ReadInt32();
                    //Паттерны
                    Patterns.Clear();
                    int patcount = file.ReadInt32();
                    for (int p = 0; p < patcount; p++)
                    {
                        //Patterns.Add(new Pattern(file.ReadByte()));
                        Pattern pat = new Pattern(file.ReadInt32());
                        for (int i = 0; i < pat.Len; i++)
                            for (int j = 0; j < ChannelCount; j++)
                                pat.Note[j, i] = file.ReadByte();
                        Patterns.Add(pat);
                    }
                    file.Close();
                    Audio.InitAudio(this);
                    return true;
                }
                System.Windows.Forms.MessageBox.Show("Файл " + FileName + " имеет более новый формат и в это версии программы не поддерживается.");
                file.Close();
                return false;
                //System.Windows.Forms.MessageBox.Show("Проект открыт из файла " + FileName);
            }
            catch
            {
                Editor.Error("Произошла ошибка при открытии файла.");
                return false;
            }
        }
    }
}
