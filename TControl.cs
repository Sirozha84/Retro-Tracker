using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroTracker
{
    class TControl
    {
        public enum Types { Button, String, Number, Table }
        public enum Keys { Up, Down, Left, Right, Add, Sub, Del }
        int X;              //Расположение контрола
        int Y;
        public Types Type;  //Тип контрола
        string String;      //Выводимый текст
        int Len;
        int Min;            //Границы для чисел
        public int Max;
        int Kx;             //Курсор в таблице
        int Ky;
        int Shift;          //Сдвиг списка, если не влазит
        Channel Ch;         //Текущий канал

        /// <summary>
        /// Создание контрола - кнопки
        /// </summary>
        /// <param name="x">X-Координата</param>
        /// <param name="y">Y-Координата</param>
        /// <param name="text">Надпись на кнопке</param>
        public TControl(int x, int y, String text)
        {
            Type = Types.Button;
            X = x;
            Y = y;
            String = text;
        }

        /// <summary>
        /// Создание контрола - текстовой строки
        /// </summary>
        /// <param name="x">X-Координата</param>
        /// <param name="y">Y-Координата</param>
        /// <param name="text">Первоначальный текст</param>
        /// <param name="lenght">Длина строки</param>
        public TControl(int x, int y, String text, int lenght)
        {
            Type = Types.String;
            X = x;
            Y = y;
            String = text;
            Len = lenght;
        }

        /// <summary>
        /// Создание контрола - строки для ввода числа
        /// </summary>
        /// <param name="x">X-Координата</param>
        /// <param name="y">Y-Координата</param>
        /// <param name="text">Первоначальный текст</param>
        /// <param name="lenght">Длина строки</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        public TControl(int x, int y, int value, int lenght, int min, int max)
        {
            Type = Types.Number;
            X = x;
            Y = y;
            String = value.ToString();
            Len = lenght;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Создание контрола - таблицы орнамента
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="chan"></param>
        public TControl(int x, int y, Channel ch)
        {
            Type = Types.Table;
            X = x;
            Y = y;
            Kx = 0;
            Ky = 0;
            Shift = 0;
            Ch = ch;
        }

        /// <summary>
        /// Рисование контрола
        /// </summary>
        public void Draw(bool Active)
        {
            switch (Type)
            {
                case Types.Button:
                    if (Active) TXT.Colors(0, 15); else TXT.Colors(0, 11);
                    TXT.PrintAT(X, Y, String);
                    break;
                case Types.String:
                case Types.Number:
                    TXT.Colors(15, 0);
                    TXT.Clear(X, Y, Len + 1, 1);
                    TXT.PrintAT(X, Y, String);
                    if (Active) { TXT.BackgroundSet(15); TXT.Write(" "); }
                    break;
                case Types.Table:
                    for (int i = 0; i < 10; i++)
                    {
                        int s = i + Shift;
                        if (s < Ch.Len)
                        {
                            byte b = 0; if (s >= Ch.Repeat) b = 1;
                            TXT.Colors(15, b); TXT.Clear(X + 3, Y + i, 23, 1);
                            TXT.PrintAT(X, Y + i, 0, 3, (s).ToString("0 "));
                            TXT.Colors(15, b); if (Kx == 0 & Ky == s) if (Active) TXT.Colors(0, 15); else TXT.Colors(0, 8);
                            TXT.PrintAT(X + 3, Y + i, Ch.ornament[s].Wave.ToString());
                            TXT.Colors(15, b); if (Kx == 1 & Ky == s) if (Active) TXT.Colors(0, 15); else TXT.Colors(0, 8);
                            TXT.PrintAT(X + 9, Y + i, Ch.ornament[s].Volume.ToString());
                            TXT.Colors(15, b); if (Kx == 2 & Ky == s) if (Active) TXT.Colors(0, 15); else TXT.Colors(0, 8);
                            TXT.PrintAT(X + 15, Y + i, Ch.ornament[s].Note.ToString());
                            TXT.Colors(15, b); if (Kx == 3 & Ky == s) if (Active) TXT.Colors(0, 15); else TXT.Colors(0, 8);
                            TXT.PrintAT(X + 21, Y + i, Ch.ornament[s].Frequency.ToString());
                        }
                        else
                        {
                            TXT.BackgroundSet(3);
                            TXT.Clear(X, Y + i, 26, 1);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Удаление из строки последнего символа
        /// </summary>
        public void DeleteLastChar()
        {
            if ((Type == Types.String | Type == Types.Number) && String.Length > 0)
                String = String.Remove(String.Length - 1);
        }

        /// <summary>
        /// Добавление к строке символа
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public void AddChar(char ch)
        {
            if ((Type == Types.String | Type == Types.Number) && String.Length < Len)
            {
                if (ch < 32) return;
                if (Type == Types.Number && (ch < 48 | ch > 57)) return; //Если вводится только цифра, остальное сбрасываем
                String += ch;
            }
        }

        /// <summary>
        /// Возвращает редактируемую строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Type == Types.String) return String;
            return "Error";
        }

        /// <summary>
        /// Коррекция вводимого числа с учётом разрешённых границ. И возвращает число.
        /// </summary>
        public int ToInt32()
        {
            if (Type != Types.Number) return 0;
            int num = 0;
            if (String.Length > 0) num = Convert.ToInt32(String);
            if (num < Min) num = Min;
            if (num > Max) num = Max;
            String = num.ToString();
            return num;
        }

        /// <summary>
        /// Увеличение числа
        /// </summary>
        public void Add()
        {
            if (Type == Types.Table) TabControl(Keys.Add);
            if (Type != Types.Number) return;
            String = (ToInt32() + 1).ToString();
            ToInt32();
        }

        /// <summary>
        /// Уменьшение числа
        /// </summary>
        public void Sub()
        {
            if (Type == Types.Table) TabControl(Keys.Sub);
            if (Type != Types.Number) return;
            String = (ToInt32() - 1).ToString();
            ToInt32();
        }

        /// <summary>
        /// Управление таблицей, возвращает true, если курсор "вылез наружу",
        /// и надо передать управление другому контролу
        /// </summary>
        /// <param name="key">Клавиша управления</param>
        /// <returns></returns>
        public bool TabControl(Keys key)
        {
            if (key == Keys.Left) if (Kx > 0) Kx--; else return true;
            if (key == Keys.Right) if (Kx < 3) Kx++; else return true;
            if (key == Keys.Up) if (Ky > 0) Ky--;// else return true;
            if (key == Keys.Down) if (Ky < Ch.Len - 1) Ky++;// else return true;
            if (Shift > Ky) Shift = Ky;
            if (Shift < Ky - 9) Shift = Ky - 9;
            if (key == Keys.Add)
            {
                if (Kx == 0 && Ch.ornament[Ky].Wave < Sinthesizer.NumberOfTypesOfWaves) Ch.ornament[Ky].Wave++;
                if (Kx == 1 && Ch.ornament[Ky].Volume < Channel.VolumeMax) Ch.ornament[Ky].Volume += Channel.VolumeStep;
                if (Kx == 2 && Ch.ornament[Ky].Note < Channel.NoteMax) Ch.ornament[Ky].Note++;
                if (Kx == 3 && Ch.ornament[Ky].Frequency < Channel.FreqMax) Ch.ornament[Ky].Frequency += Channel.FreqStep;
            }
            if (key == Keys.Sub)
            {
                if (Kx == 0 && Ch.ornament[Ky].Wave > 0) Ch.ornament[Ky].Wave--;
                if (Kx == 1 && Ch.ornament[Ky].Volume > Channel.VolumeMin) Ch.ornament[Ky].Volume -= Channel.VolumeStep;
                if (Kx == 2 && Ch.ornament[Ky].Note > Channel.NoteMin) Ch.ornament[Ky].Note--;
                if (Kx == 3 && Ch.ornament[Ky].Frequency > Channel.FreqMin) Ch.ornament[Ky].Frequency -= Channel.FreqStep;
            }
            if (key == Keys.Del)
            {
                if (Kx == 0) Ch.ornament[Ky].Wave = 0;
                if (Kx == 1) Ch.ornament[Ky].Volume = 0;
                if (Kx == 2) Ch.ornament[Ky].Note = 0;
                if (Kx == 3) Ch.ornament[Ky].Frequency = 0;
            }
            return false;
        }

        /// <summary>
        /// Изменение размера таблицы
        /// </summary>
        /// <param name="len">Новая длина</param>
        /// <param name="repeat">Новый индекс для повтора</param>
        public void TabResize(int len, int repeat)
        {
            if (Ch.Len != len) Ch.Len = len;
            if (Ch.Repeat != repeat) Ch.Repeat = repeat;
            if (Shift > Ch.Len - 10) Shift = Ch.Len - 10;
            if (Shift < 0) Shift = 0;
            if (Ky >= Ch.Len) Ky = Ch.Len - 1;
        }
    }
}
