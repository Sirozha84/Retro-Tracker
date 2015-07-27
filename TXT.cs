using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RetroTracker
{
    class TXT
    {
        const byte Columns = 80;
        const byte Rows = 25;
        const byte SymbolWidth = 16;
        const byte SymbolHeight = 28;
        public static Bitmap Screen = new Bitmap(SymbolWidth * Columns, SymbolHeight * Rows); //Это и будет наш экран
        static Graphics grap = Graphics.FromImage(Screen);
        static SolidBrush[] DosColor = { new SolidBrush(Color.FromArgb(0, 0, 0)),
                                         new SolidBrush(Color.FromArgb(0, 0, 170)),
                                         new SolidBrush(Color.FromArgb(0, 170, 0)),
                                         new SolidBrush(Color.FromArgb(0, 170, 170)),
                                         new SolidBrush(Color.FromArgb(170, 0, 0)),
                                         new SolidBrush(Color.FromArgb(170, 0, 170)),
                                         new SolidBrush(Color.FromArgb(170, 170, 0)),
                                         new SolidBrush(Color.FromArgb(170, 170, 170)),
                                         new SolidBrush(Color.FromArgb(85, 85, 85)),
                                         new SolidBrush(Color.FromArgb(85, 85, 255)),
                                         new SolidBrush(Color.FromArgb(85, 255, 85)),
                                         new SolidBrush(Color.FromArgb(85, 255, 255)),
                                         new SolidBrush(Color.FromArgb(255, 85, 85)),
                                         new SolidBrush(Color.FromArgb(255, 85, 255)),
                                         new SolidBrush(Color.FromArgb(255, 255, 85)),
                                         new SolidBrush(Color.FromArgb(255, 255, 255))};
        static char[,] MemoryS = new char[Columns, Rows]; //Матрица символов
        static byte[,] MemoryC = new byte[Columns, Rows]; //Матрица цветов
        static byte[,] MemoryB = new byte[Columns, Rows]; //Матрица бэкграундов
        static byte LocateX = 0;
        static byte LocateY = 0;
        static byte CorrentColor = 7;
        static byte CorrentBackground = 0;

        //Перерисовка всего поля
        static void Draw() 
        {
            for (byte i = 0; i < Columns; i++)
                for (byte j = 0; j < Rows; j++)
                    Draw(i, j);
        }
        
        //Перерисовка конкретного символа   
        static void Draw(int x, int y) 
        {
            grap.FillRectangle(DosColor[MemoryB[x, y]], x * SymbolWidth, y * SymbolHeight, SymbolWidth, SymbolHeight);
            if ((MemoryS[x, y]) != ' ')
                grap.DrawString((MemoryS[x, y]).ToString(), new Font("Consolas", 16), DosColor[MemoryC[x, y]], x * SymbolWidth - 1, y * SymbolHeight + 1);
        }
        //Скролл текста вверх
        static void Scroll()
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows - 1; j++)
                {
                    MemoryS[i, j] = MemoryS[i, j + 1];
                    MemoryC[i, j] = MemoryC[i, j + 1];
                    MemoryB[i, j] = MemoryB[i, j + 1];
                }
                MemoryS[i, Rows - 1] = ' ';
                MemoryC[i, Rows - 1] = CorrentColor;
                MemoryB[i, Rows - 1] = CorrentBackground;
            }
            Draw();
        }

        //Очистка экрана
        public static void Clear()
        {
            Clear(0, 0, Columns, Rows);
        }

        //Очистка заданного региона
        public static void Clear(int x, int y, int width, int height)
        {
            for (int i = x; i < x + width; i++)
                for (int j = y; j < y + height; j++)
                {
                    MemoryS[i, j] = ' ';
                    MemoryC[i, j] = CorrentColor;
                    MemoryB[i, j] = CorrentBackground;
                    Draw(i, j);
                }
        }

        //Перевод коретки
        public static void EOS()
        {
            LocateX = 0;
            LocateY++;
            if (LocateY >= Rows)
            {
                LocateY = Rows - 1;
                Scroll();
            }
        }

        //Печать строки
        public static void Write(string String)
        {
            if (LocateX > Columns) LocateX = 0;
            if (LocateY > Rows) LocateY = 0;
            foreach (char ch in String)
            {
                MemoryS[LocateX, LocateY] = ch;
                MemoryC[LocateX, LocateY] = CorrentColor;
                MemoryB[LocateX, LocateY] = CorrentBackground;
                Draw(LocateX, LocateY);
                LocateX++;
                if (LocateX >= Columns) { EOS(); }
            }        
        }

        //Печать строки с переводом коретки
        public static void WriteLine(string String)
        {
            Write(String);
            if (LocateX > 0) EOS();
        }

        //Установка цвета
        public static void ColorSet(byte color)
        {
            CorrentColor = color;
        }

        //Установка бэкграунда
        public static void BackgroundSet(byte back)
        {
            CorrentBackground = back;
        }

        //Установка всех цветов
        public static void Colors(byte color, byte background)
        {
            CorrentColor = color;
            CorrentBackground = background;
        }

        //Установка позиции
        public static void Locate(int x, int y)
        {
            if (x >= Columns) x = 0;
            if (y >= Rows) y = 0;
            LocateX = (byte)x;
            LocateY = (byte)y;
        }

        //Печать как в спектруме, всё и сразу
        public static void PrintAT(int x, int y, string String)
        {
            Locate(x, y);
            Write(String);
        }

        //Печать как в спектруме, но без указания цветов
        public static void PrintAT(int x, int y, byte color, byte background, string String)
        {
            Locate(x, y);
            ColorSet(color);
            BackgroundSet(background);
            Write(String);
        }
    }
}
