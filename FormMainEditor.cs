using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroTracker
{
    public partial class FormMain : Form
    {
        #region Переменные
        const int ColumnWidth = 4;
        const int Columns = 76 / ColumnWidth;
        enum EditModes { Edit, Track, Mixer, Properties, PropertiesApply, SinSetup };
        //Всякие редакторские переменные
        int PKursorX;       //Курсор паттерна (канал)
        int PKursorY;       //Курсор паттерна (нота)
        int PShift;         //Сдвиг колонок каналов
        int TKursor;        //Курсор трека
        int TShift;         //Сдвиг полоски трека
        EditModes EditMode = EditModes.Edit;    //Режим редактирования
        bool Selected = false;                  //Выделено ли то-то
        int SelectX1;       //Границы выделения
        int SelectX2;
        int SelectY1;
        int SelectY2;
        byte Octave = 4;    //Текущая октава
        byte AutoStep = 0;  //Автошаг
        byte Flash = 0;     //Подсветка редактируемых параметров (тупо для красоты
        byte FlashParam;    //Какой именно параметр будет подсвечиваться
        //Переменные для окон настроек или вопросов
        int K = 0;          //Курсор (TAB-позиция)
        TControl[] TControls;
        #endregion

        #region Рисование трека и паттернов
        //Рисование панели с подсказками
        void DrawPanel()
        {
            if (Flash > 0)
            {
                timerFlash.Enabled = false;
                timerFlash.Interval = 100;
                timerFlash.Enabled = true;
            }
            TXT.Colors(0, 0);
            TXT.Clear(0, 24, 80, 1);
            switch (EditMode)
            {
                case EditModes.Edit:
                    TXT.Colors(0, 3);
                    TXT.Clear(0, 24, 80, 1);

                    TXT.Locate(0, 24);

                    TXT.Colors(0, 3); TXT.Write(" Темп ");
                    TXT.Colors(0, 11); if (Flash > 0 & FlashParam == 0) TXT.Colors(0, 14);
                    TXT.Write(" " + CurrentProject.Tempo.ToString("000") + " ");

                    TXT.Colors(0, 3); TXT.Write(" Октава ");
                    TXT.Colors(0, 11); if (Flash > 0 & FlashParam == 1) TXT.Colors(0, 14);
                    TXT.Write(" " + Octave.ToString() + " ");

                    TXT.Colors(0, 3); TXT.Write(" Автошаг ");
                    TXT.Colors(0, 11); if (Flash > 0 & FlashParam == 2) TXT.Colors(0, 14);
                    TXT.Write(" " + AutoStep.ToString() + " ");

                    TXT.Colors(0, 3); TXT.Write(" Гр. канала ");
                    TXT.Colors(0, 11); if (Flash > 0 & FlashParam == 3) TXT.Colors(0, 14);
                    TXT.Write(" " + CurrentProject.Channels[PKursorX].Volume.ToString("000") + " ");

                    TXT.Colors(0, 3); TXT.Write(" Активный ");
                    TXT.Colors(0, 11);if (Flash > 0 & FlashParam == 4) TXT.Colors(0, 14);
                    if (Audio.player.sinthesizer[PKursorX].Mute) TXT.Write(" Нет "); else TXT.Write(" Да  ");
                    break;
                case EditModes.Mixer:
                    TXT.Colors(0, 3);
                    TXT.Clear(0, 24, 80, 1);
                    TXT.PrintAT(0, 24, 15, 3, " Пробел");
                    TXT.Colors(0, 3); TXT.Write(" - Mute ");
                    TXT.Colors(15, 3); TXT.Write("S");
                    TXT.Colors(0, 3); TXT.Write(" - Соло ");
                    TXT.Colors(15, 3); TXT.Write("Enter");
                    TXT.Colors(0, 3); TXT.Write(" - включить всё ");
                    TXT.Colors(15, 3); TXT.Write("Esc");
                    TXT.Colors(0, 3); TXT.Write(" - стоп и выход");
                    break;
                case EditModes.Track:
                    TXT.Colors(0, 3);
                    TXT.Clear(0, 24, 80, 1);
                    TXT.PrintAT(0, 24, 15, 3, " Ctrl+стрелки");
                    TXT.Colors(0, 3); TXT.Write(" сдвиг ");
                    TXT.Colors(15, 3); TXT.Write("N");
                    TXT.Colors(0, 3); TXT.Write(" новый ");
                    TXT.Colors(15, 3); TXT.Write("D");
                    TXT.Colors(0, 3); TXT.Write(" дубл. ");
                    TXT.Colors(15, 3); TXT.Write("C");
                    TXT.Colors(0, 3); TXT.Write(" копия ");
                    TXT.Colors(15, 3); TXT.Write("Del");
                    TXT.Colors(0, 3); TXT.Write(" удалить ");
                    TXT.Colors(15, 3); TXT.Write("+/-");
                    TXT.Colors(0, 3); TXT.Write(" длина ");
                    TXT.Colors(0, 11); if (Flash > 0) TXT.Colors(0, 14);
                    TXT.Write(CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len.ToString(" 000 "));
                    break;
            }
        }

        //Рисование трека
        void DrawTrack()
        {
            if (TShift > TKursor) TShift = TKursor;
            if (TShift < TKursor - 19) TShift = TKursor - 19;
            TXT.Colors(15, 0);
            TXT.Clear(0, 0, 80, 3);
            for (int i = 0; i < 20; i++)
            {
                int t = i + TShift;
                if (t < CurrentProject.TrackLen)
                {
                    TXT.Colors(15,1);
                    if (t == TKursor) if (EditMode == EditModes.Track) TXT.Colors(0, 15); else TXT.Colors(15, 9); //Цвет курсора
                    int n = CurrentProject.Track[t];
                    TXT.Locate(i * 4, 0); TXT.Write("    ");
                    TXT.Locate(i * 4, 1); TXT.Write(" " + n.ToString("00") + " ");
                    TXT.Locate(i * 4, 2); TXT.Write("    ");
                }
            }
            //Полоска типа прокрутки
            byte dl = 78;
            byte left = 1;
            if (CurrentProject.TrackLen > 20)
            {
                dl = (byte)Math.Round(78 * (float)20 / CurrentProject.TrackLen);
                if (dl < 1) dl = 1;
                left = (byte)(Math.Round(78 * (float)TShift / CurrentProject.TrackLen) + 1);
            }
            TXT.PrintAT(0, 3, 15, 8, "<                                                                              >");
            TXT.Colors(7, 7);
            TXT.Clear(left, 3, dl, 1);
        }

        /// <summary>
        /// Вычисление правильного номера для отображения
        /// </summary>
        /// <param name="t">позиция в треке</param>
        /// <param name="n">абсолютный номер</param>
        /// <returns></returns>
        int CalculateNum(int t, int n)
        {
            //return n;
            if (n < 0)
            {
                if (t > 0)
                    return CalculateNum(t - 1, n + CurrentProject.Patterns[CurrentProject.Track[t - 1]].Len);
                return -1;
            }
            if (n >= CurrentProject.Patterns[CurrentProject.Track[t]].Len)
            {
                if (t < CurrentProject.TrackLen - 1)
                    return CalculateNum(t + 1, n - CurrentProject.Patterns[CurrentProject.Track[t]].Len);
                return -1;
            }
            return n;
        }

        /// <summary>
        /// Вычисление ноты в нужном знакоместе
        /// </summary>
        /// <param name="t">позиция в треке</param>
        /// <param name="n">абсолютный номер</param>
        /// <param name="k">канал</param>
        /// <returns></returns>
        string CalculateNote(int t, int n, int k)
        {
            //return "TTT";
            if (n < 0)
            {
                if (t > 0)
                    return CalculateNote(t - 1, n + CurrentProject.Patterns[CurrentProject.Track[t - 1]].Len, k);
                return "   ";
            }
            if (n >= CurrentProject.Patterns[CurrentProject.Track[t]].Len)
            {
                if (t < CurrentProject.TrackLen - 1)
                    return CalculateNote(t + 1, n - CurrentProject.Patterns[CurrentProject.Track[t]].Len, k);
                return "   ";
            }
            return Note(CurrentProject.Patterns[CurrentProject.Track[t]].Note[k, n]);
        }

        //Нота для отображения
        string Note(int i)
        {
            if (i == 1) return "~~~";
            if (i < 12) return "-  ";

            string s = (i / 12 - 1).ToString();
            switch ((i) % 12)
            {
                case 0: s += "C "; break;
                case 1: s += "C#"; break;
                case 2: s += "D "; break;
                case 3: s += "D#"; break;
                case 4: s += "E "; break;
                case 5: s += "F "; break;
                case 6: s += "F#"; break;
                case 7: s += "G "; break;
                case 8: s += "G#"; break;
                case 9: s += "A "; break;
                case 10: s += "A#"; break;
                case 11: s += "B "; break;
            }
            return s;
        }

        //Попадает ли число в регион между двумя другими
        bool InRegion(int x, int a, int b)
        {
            if (a <= b) return x >= a & x <= b;
            else return x >= b & x <= a;
        }

        //Рисование визуализации
        void DrawVisual()
        {
            for (int i = -1; i < Columns; i++)
            {
                int ch = i + PShift;    //Рисуемый канал
                int left = 3 + i * ColumnWidth;   //Колонка в которой рисуется канал
                if (i >= 0 & ch < CurrentProject.ChannelCount) //Шапка 
                {
                    TXT.Colors(0, 2);
                    if (Audio.player.sinthesizer[ch].Mute) TXT.Colors(0, 4);
                    TXT.PrintAT(left, 4, "К" + ch.ToString("00"));
                    TXT.BackgroundSet(0);
                    if (Audio.player.sinthesizer[ch].Played & !Audio.player.sinthesizer[ch].Mute)
                        TXT.BackgroundSet(VolumeToColor(Audio.player.sinthesizer[i].Volume));
                        TXT.Write(" ");
                }
            }
        }

        //Рисование паттерна
        void DrawPattern()
        {
            for (int i = -1; i < Columns; i++)
            {
                int ch = i + PShift;    //Рисуемый канал
                int left = 3 + i * ColumnWidth;   //Колонка в которой рисуется канал
                for (int j = -9; j <= 9; j++)
                {

                    int num = j + PKursorY;
                    //Устанавливаем цвет строки исходя из того корретный ли это паттерн, и строка ли это начала такта
                    if (num >= 0 & num < CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len)
                    {
                        TXT.Colors(7, 0); //Обычная нота
                        if (CurrentProject.TaktLen>0 && num % CurrentProject.TaktLen == 0) TXT.Colors(15, 0); //Начало такта
                        //А не выделена ли эта нота?
                        if (Selected & InRegion(num, SelectY1, SelectY2) & InRegion(ch,SelectX1, SelectX2)) TXT.Colors(15, 9);
                    }
                    else TXT.Colors(8, 0); //Всё остальное тёмно серое
                    //Рисуем номера строк
                    if (i < 0)
                    {
                        if (j == 0) TXT.BackgroundSet(8);
                        //num = CalculateNum(TKursor, num);
                        num = CalculateNum(TKursor, num);
                        if (num >= 0) TXT.PrintAT(0, j + 14, num.ToString("00 "));
                        else TXT.PrintAT(0, j + 14, "   ");
                    }
                    if (i >= 0 & ch < CurrentProject.ChannelCount) //Таблица
                    {
                        if (j == 0)
                        {
                            TXT.BackgroundSet(8);
                            //А не выделена ли эта нота?
                            if (Selected & InRegion(ch, SelectX1, SelectX2)) TXT.Colors(15, 9);
                            //А не курсор ли это
                            if (i == PKursorX - PShift & EditMode == EditModes.Edit) TXT.Colors(0, 15);
                        }
                        //TXT.PrintAT(left, j + 14, CalculateNote(TKursor, num, ch) + " ");
                        TXT.PrintAT(left, j + 14, CalculateNote(TKursor, num, ch) + " ");
                    }
                }
            }
        }

        //Превращение громкости в цвет для визуализации
        byte VolumeToColor(float v)
        {
            if (v > 0.9f) return 15;
            if (v > 0.7f) return 14;
            if (v > 0.5f) return 10;
            if (v > 0.3f) return 2;
            if (v > 0.1f) return 8;
            return 0;
        }

        //Рисовать весь экран
        void DrawAll()
        {
            TXT.BackgroundSet(0);
            TXT.Clear();
            DrawTrack();
            DrawVisual();
            DrawPattern();
            DrawPanel();
            RefreshScreen();
        }
        #endregion

        #region Рисование (Диалоговые окна)
        /// <summary>
        /// Рисование поля параметров трека
        /// </summary>
        void DrawProperties()
        {
            TXT.Colors(0, 3);
            TXT.Clear(22, 5, 36, 15);
            TXT.PrintAT(32, 6, "Параметры  трека");
            TXT.PrintAT(24, 8, "Название:");
            TXT.PrintAT(24, 10, "Альбом:");
            TXT.PrintAT(24, 12, "Автор:");
            TXT.PrintAT(24, 14, "Длина паттерна:     -+");
            TXT.PrintAT(24, 16, "Нот в такте:        -+");
            DrawControls();
        }

        /// <summary>
        /// Рисование вопроса о применении параметров
        /// </summary>
        void DrawPropertiesApply()
        {
            TXT.Colors(0, 11);
            TXT.Clear(17, 10, 46, 6);
            TXT.PrintAT(18, 11, "Длина по умолчанию для паттернов изменилась.");
            TXT.PrintAT(18, 12, "Изменить длину всех существующих паттернов?");
            DrawControls();
        }

        /// <summary>
        /// Рисование настройки синтезатора
        /// </summary>
        void DrawSinSetup()
        {
            TXT.Colors(0, 3);
            TXT.Clear(15, 0, 49, 25);
            TXT.PrintAT(29, 1, "Настройка  синтезатора");
            TXT.PrintAT(16, 3, "Канал:          Копировать в:");
            TXT.PrintAT(16, 5, "Выведение:      Удержание:      Затухание:");
            TXT.PrintAT(36, 7, "Орнамент");
            TXT.PrintAT(29, 9, "Длина:     Повтор:");
            TXT.PrintAT(29, 11, "Волна Ампл. Нота  Част.");
            TXT.PrintAT(17, 23, "~ - стоп (и в треке тоже)");
            DrawControls();
        }

        /// <summary>
        /// Рисование контролов
        /// </summary>
        void DrawControls()
        {
            for (int i = 0; i < TControls.Count(); i++) TControls[i].Draw(i == K);
        }

        /// <summary>
        /// Рисование микшера
        /// </summary>
        void DrawMixer()
        {
            for (int i = -1; i < Columns; i++)
            {
                int ch = i + PShift;    //Рисуемый канал
                int left = 3 + i * ColumnWidth;   //Колонка в которой рисуется канал
                if (i >= 0 & ch < CurrentProject.ChannelCount) //Шапка 
                {
                    if (PKursorX == ch) TXT.Colors(0, 15); else TXT.Colors(0, 3);
                    TXT.PrintAT(left, 14, CurrentProject.Channels[ch].Volume.ToString("000 "));
                }
            }
        }
        #endregion

        #region Управление с клавиатуры
        //Управление с клавиатуры
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (EditMode)
            {
                #region Редактирование паттерна
                case EditModes.Edit:
                    //Оставка плеера, если он запущен
                    if (Audio.player.Mode != Player.Modes.Stop) menuStop_Click(null, null);
                    //Простое перемещение по паттерну
                    if (e.KeyCode == Keys.Up & !e.Shift)
                    {
                        if (Selected) Selected = false;
                        if (PKursorY > 0) PKursorY--;
                        else
                            if (TKursor > 0)
                        {
                            TKursor--;
                            PKursorY = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len - 1;
                        }
                    }
                    if (e.KeyCode == Keys.Down & !e.Shift)
                    {
                        if (Selected) Selected = false;
                        if (PKursorY < CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len - 1) PKursorY++;
                        else if (TKursor < CurrentProject.TrackLen - 1) { TKursor++; PKursorY = 0; }
                    }
                    if (e.KeyCode == Keys.Left & !e.Shift)
                    {
                        if (Selected) Selected = false;
                        if (PKursorX > 0) { PKursorX--; if (PShift > PKursorX) PShift = PKursorX; }
                    }
                    if (e.KeyCode == Keys.Right & !e.Shift)
                    {
                        if (Selected) Selected = false;
                        if (PKursorX < CurrentProject.ChannelCount - 1)
                        {
                            PKursorX++;
                            if (PShift < PKursorX - Columns + 1) PShift = PKursorX - Columns + 1;
                            DrawPattern();
                        }
                    }
                    //Выделение
                    if (e.KeyCode == Keys.Up & e.Shift)
                    {
                        StartSelect();
                        if (PKursorY > 0) { PKursorY--; SelectY2 = PKursorY; }
                    }
                    if (e.KeyCode == Keys.Down & e.Shift)
                    {
                        StartSelect();
                        if (PKursorY < CurrentProject.Patterns[CurrentProject.Track[CurrentProject.Track[TKursor]]].Len - 1)
                        { PKursorY++; SelectY2 = PKursorY; }
                    }
                    if (e.KeyCode == Keys.Left & e.Shift)
                    {
                        StartSelect();
                        if (PKursorX > 0) { PKursorX--; SelectX2 = PKursorX; if (PShift > PKursorX) PShift = PKursorX; }
                    }
                    if (e.KeyCode == Keys.Right & e.Shift)
                    {
                        StartSelect();
                        if (PKursorX < CurrentProject.ChannelCount - 1)
                        {
                            PKursorX++;
                            SelectX2 = PKursorX;
                            if (PShift < PKursorX - Columns + 1) PShift = PKursorX - Columns + 1;
                            DrawPattern();
                        }
                    }
                    //Нота
                    int key = KeyToNote(e, true);
                    if (key > 0) //Проигрывание ноты
                    {
                        CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[PKursorX, PKursorY] = (byte)key;
                        Audio.PlayTrack(CurrentProject, TKursor, PKursorY);
                        PKursorY += AutoStep;
                        if (PKursorY >= CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len)
                            PKursorY -= CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Space & !e.Control) //Расстановка пробелоа
                    {
                        PKursorY += AutoStep;
                        if (PKursorY >= CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len)
                            PKursorY -= CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len;
                    }
                    //Проигрывание позиции
                    if (e.KeyCode == Keys.Enter & !e.Control) Audio.PlayTrack(CurrentProject, TKursor, PKursorY);
                    //Копипаст и прочее редактирование
                    if (e.KeyCode == Keys.Delete && e.Shift) menucut_Click(null, null);     //Shift + Del - вырезать
                    if (e.KeyCode == Keys.Insert && e.Control) menucopy_Click(null, null);  //Ctrl + Insert - копировать
                    if (e.KeyCode == Keys.Insert && e.Shift) menupaste_Click(null, null);   //Shift + Insert - вставить
                    if (e.KeyCode == Keys.Delete) Delete();                                 //Delete - удалить
                    //Регулирование параметров
                    if (e.KeyCode == Keys.NumPad1) { Octave = 1; Flash = 10; FlashParam = 1; }
                    if (e.KeyCode == Keys.NumPad2) { Octave = 2; Flash = 10; FlashParam = 1; }
                    if (e.KeyCode == Keys.NumPad3) { Octave = 3; Flash = 10; FlashParam = 1; }
                    if (e.KeyCode == Keys.NumPad4) { Octave = 4; Flash = 10; FlashParam = 1; }
                    if (e.KeyCode == Keys.NumPad5) { Octave = 5; Flash = 10; FlashParam = 1; }
                    if (e.KeyCode == Keys.NumPad6) { Octave = 6; Flash = 10; FlashParam = 1; }
                    if (e.KeyCode == Keys.Add & !e.Control)
                    {
                        CurrentProject.Tempo++;
                        if (CurrentProject.Tempo > 999) CurrentProject.Tempo = 999;
                        Flash = 10;
                        FlashParam = 0;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Subtract & !e.Control)
                    {
                        CurrentProject.Tempo--;
                        if (CurrentProject.Tempo < 1) CurrentProject.Tempo = 1;
                        Flash = 10;
                        FlashParam = 0;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Multiply)
                    {
                        AutoStep++;
                        if (AutoStep > 4) AutoStep = 4;
                        Flash = 10;
                        FlashParam = 2;
                    }
                    if (e.KeyCode == Keys.Divide)
                    {
                        AutoStep--;
                        if (AutoStep == 255) AutoStep = 0;
                        Flash = 10;
                        FlashParam = 2;
                    }
                    //Быстрые натсройки микшера
                    if (e.KeyCode == Keys.Add & e.Control)
                    {
                        if (CurrentProject.Channels[PKursorX].Volume < 100) CurrentProject.Channels[PKursorX].Volume += 5;
                        Flash = 10;
                        FlashParam = 3;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Subtract & e.Control)
                    {
                        if (CurrentProject.Channels[PKursorX].Volume > 0) CurrentProject.Channels[PKursorX].Volume -= 5;
                        Flash = 10;
                        FlashParam = 3;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Space & e.Control) { MuteTogle(PKursorX); Flash = 10; FlashParam = 4; DrawPanel(); }
                    if (e.KeyCode == Keys.Enter & e.Control) { OnAllChannels(); Flash = 10; FlashParam = 4; DrawPanel(); }
                    if (e.KeyCode == Keys.S & e.Control & e.Alt) { Solo(PKursorX); Flash = 10; FlashParam = 4; DrawPanel(); }
                    if (e.KeyCode == Keys.Tab) EditMode = EditModes.Track;
                    DrawTrack();
                    DrawPattern();
                    DrawPanel();
                    Refresh();
                    break;
                #endregion
                #region Редактирование трека
                case EditModes.Track:
                    if (Audio.player.Mode != Player.Modes.Stop) menuStop_Click(null, null);
                    //Простое перемещение по треку
                    if (e.KeyCode == Keys.Left & !e.Control & TKursor > 0) { TKursor--; PKursorY = 0; }
                    if (e.KeyCode == Keys.Right & !e.Control & TKursor < CurrentProject.TrackLen - 1) { TKursor++; PKursorY = 0; }
                    //Перемещение паттернов
                    if (e.KeyCode == Keys.Left & e.Control & TKursor > 0)
                    {
                        int temp = CurrentProject.Track[TKursor - 1];
                        CurrentProject.Track[TKursor - 1] = CurrentProject.Track[TKursor];
                        CurrentProject.Track[TKursor] = temp;
                        TKursor--;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Right & e.Control & TKursor < CurrentProject.TrackLen - 1)
                    {
                        int temp = CurrentProject.Track[TKursor + 1];
                        CurrentProject.Track[TKursor + 1] = CurrentProject.Track[TKursor];
                        CurrentProject.Track[TKursor] = temp;
                        TKursor++;
                        HistoryChange();
                    }
                    //Создание нового паттерна
                    if (e.KeyCode == Keys.N && PushPats())
                    {
                        //Находим минимальный доступный номер
                        int n = 0;
                        bool est;
                        do
                        {
                            est = false;
                            for (int i = 0; i < CurrentProject.TrackLen; i++)
                                if (CurrentProject.Track[i] == n) est = true;
                            if (est) n++;
                        } while (est);
                        //Надо ли создавать новый паттерн, или воскрешать удалённый
                        if (n >= CurrentProject.Patterns.Count)
                        {
                            if (n < Project.MaxPatternsCound)
                            {
                                CurrentProject.Patterns.Add(new Pattern(CurrentProject.PatternLen));
                                CurrentProject.Track[++TKursor] = n;
                                PKursorY = 0;
                                HistoryChange();
                            }
                        }
                        else
                        {
                            CurrentProject.Track[++TKursor] = n;
                            PKursorY = 0;
                            HistoryChange();
                        }
                    }
                    //Дублирование паттерна
                    if (e.KeyCode == Keys.D && PushPats())
                    {
                        CurrentProject.Track[++TKursor] = CurrentProject.Track[TKursor - 1];
                        HistoryChange();
                    }
                    //Клонирование паттерна
                    if (e.KeyCode == Keys.C && PushPats())
                    {
                        //Находим минимальный доступный номер
                        int n = 0;
                        bool est;
                        do
                        {
                            est = false;
                            for (int i = 0; i < CurrentProject.TrackLen; i++)
                                if (CurrentProject.Track[i] == n) est = true;
                            if (est) n++;
                        } while (est);
                        //Создаём новый паттерн и копируем в него предыдущий (ох.... тестировать будет мучительно)
                        if (n < CurrentProject.Patterns.Count)
                            CurrentProject.Patterns[n] = new Pattern(CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len);
                        else
                            CurrentProject.Patterns.Add(new Pattern(CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len));
                        CurrentProject.Track[TKursor + 1] = n;
                        CurrentProject.Patterns[n].Len = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len;
                        for (int i = 0; i < Pattern.MaxPatternLen; i++)
                            for (int j = 0; j < Audio.MaxChannels; j++)
                                CurrentProject.Patterns[n].Note[j, i] = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[j, i];
                        PKursorY = 0;
                        TKursor++;
                        HistoryChange();
                    }
                    //Удаление паттерна
                    if (e.KeyCode == Keys.Delete & CurrentProject.TrackLen > 1)
                    {
                        for (int i = TKursor; i < CurrentProject.TrackLen; i++)
                            CurrentProject.Track[i] = CurrentProject.Track[i + 1];
                        CurrentProject.TrackLen--;
                        if (TKursor > CurrentProject.TrackLen - 1) TKursor = CurrentProject.TrackLen - 1;
                        PKursorY = 0;
                        HistoryChange();
                    }
                    //Изменение размера паттерна
                    if (e.KeyCode == Keys.Add)
                    {
                        CurrentProject.Patterns[CurrentProject.Track[TKursor]].ResizeBy(1);
                        Flash = 10;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Subtract)
                    {
                        CurrentProject.Patterns[CurrentProject.Track[TKursor]].ResizeBy(-1);
                        Flash = 10;
                        if (PKursorY > CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len - 1)
                            PKursorY = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Len - 1;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Escape | e.KeyCode == Keys.Enter | e.KeyCode == Keys.Tab) EditMode = EditModes.Edit;
                    DrawTrack();
                    DrawPattern();
                    DrawPanel();
                    Refresh();
                    break;
                #endregion
                #region Параметры трека и настройки синтезатора и микшер
                case EditModes.Properties:
                    if (Audio.player.Mode != Player.Modes.Stop) menuStop_Click(null, null); //Оставка плеера, если он запущен
                    if (e.KeyCode == Keys.Left | e.KeyCode == Keys.Up | (e.KeyCode == Keys.Tab & e.Shift))
                    { TControls[K].ToInt32(); K--; if (K < 0) K = 6; }
                    if (e.KeyCode == Keys.Right | e.KeyCode == Keys.Down | (e.KeyCode == Keys.Tab & !e.Shift))
                    { TControls[K].ToInt32(); K++; if (K > 6) K = 0; }
                    //if (e.KeyCode == Keys.Tab & e.Shift) { K--; if (K < 0) K = TControls.Count() - 1; }
                    if (e.KeyCode == Keys.Back) TControls[K].DeleteLastChar();
                    if (e.KeyCode == Keys.Add) TControls[K].Add();
                    if (e.KeyCode == Keys.Subtract) TControls[K].Sub();
                    DrawControls();
                    RefreshScreen();
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (K == 6)
                        {
                            EditMode = EditModes.Edit;
                            DrawAll();
                        }
                        else
                        {
                            //Пользователь хочет применить изменения пропертисов
                            int oldPatternLen = CurrentProject.PatternLen;
                            CurrentProject.SongName = TControls[0].ToString();
                            CurrentProject.AlbumName = TControls[1].ToString();
                            CurrentProject.AutorName = TControls[2].ToString();
                            CurrentProject.PatternLen = Convert.ToInt32(TControls[3].ToInt32());
                            CurrentProject.TaktLen = Convert.ToInt32(TControls[4].ToInt32());
                            if (CurrentProject.PatternLen == oldPatternLen)
                            {
                                EditMode = EditModes.Edit;
                                DrawAll();
                                HistoryChange();
                            }
                            else
                            {
                                EditMode = EditModes.PropertiesApply;
                                TControls = new TControl[2];
                                TControls[0] = new TControl(25, 14, " Изменить ");
                                TControls[1] = new TControl(37, 14, " Только для новых ");
                                K = 0;
                                DrawPropertiesApply();
                            }
                        }
                    }
                    if (e.KeyCode == Keys.Escape) { EditMode = EditModes.Edit; DrawAll(); }
                    break;
                #endregion
                #region Вопрос об изменении размера имеющихся паттернов
                case EditModes.PropertiesApply:
                    if (e.KeyCode == Keys.Up | e.KeyCode == Keys.Down | e.KeyCode == Keys.Left | e.KeyCode == Keys.Right |
                        e.KeyCode == Keys.Tab)
                    { K = 1 - K; DrawControls(); RefreshScreen(); }
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (K == 0)
                        {
                            //Изменяем длину всех паттернов
                            foreach (Pattern pat in CurrentProject.Patterns) pat.Resize(CurrentProject.PatternLen);
                            if (PKursorY >= CurrentProject.PatternLen) PKursorY = CurrentProject.PatternLen - 1;
                            HistoryChange();
                        }
                        EditMode = EditModes.Edit;
                        DrawAll();
                    }
                    if (e.KeyCode == Keys.Escape) { EditMode = EditModes.Edit; DrawAll(); }
                    break;
                #endregion
                #region Настройка синтезатора
                case EditModes.SinSetup:
                    if (Audio.player.Mode != Player.Modes.Stop) menuStop_Click(null, null); //Оставка плеера, если он запущен
                    if (TControls[K].Type != TControl.Types.Table)
                    {
                        if (e.KeyCode == Keys.Left | e.KeyCode == Keys.Up) K--;
                        if (e.KeyCode == Keys.Right | e.KeyCode == Keys.Down) K++;
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Left && TControls[K].TabControl(TControl.Keys.Left)) K--;
                        if (e.KeyCode == Keys.Up && TControls[K].TabControl(TControl.Keys.Up)) K--;
                        if (e.KeyCode == Keys.Right && TControls[K].TabControl(TControl.Keys.Right)) K++;
                        if (e.KeyCode == Keys.Down && TControls[K].TabControl(TControl.Keys.Down)) K++;
                        if (e.KeyCode == Keys.Delete) { TControls[K].TabControl(TControl.Keys.Del); HistoryChange(); }
                    }
                    if (e.KeyCode == Keys.Back) { TControls[K].DeleteLastChar(); HistoryChange(); }
                    if (e.KeyCode == Keys.Add) { TControls[K].Add(); HistoryChange(); }
                    if (e.KeyCode == Keys.Subtract) { TControls[K].Sub(); HistoryChange(); }
                    if (e.KeyCode == Keys.Tab & !e.Shift) { K++; if (K > TControls.Count() - 1) K = 0; }
                    if (e.KeyCode == Keys.Tab & e.Shift) { K--; if (K < 0) K = TControls.Count() - 1; }
                    if (K < 0) K = TControls.Count() - 1;
                    if (K > TControls.Count() - 1) K = 0;
                    //Если выбран другой канал
                    if (PKursorX != TControls[0].ToInt32())
                    {
                        PKursorX = TControls[0].ToInt32();
                        настройкаToolStripMenuItem_Click(null, null);
                    }
                    //Что если длина или повтор орнамента изменились, меняем его контрол
                    TControls[7].Max = TControls[6].ToInt32();// - 1;
                    TControls[8].TabResize(TControls[6].ToInt32(), TControls[7].ToInt32());
                    DrawControls();
                    RefreshScreen();
                    //Поменялись длины атаки и затухания
                    CurrentProject.Channels[PKursorX].Atack = TControls[3].ToInt32();
                    CurrentProject.Channels[PKursorX].Hold = TControls[4].ToInt32();
                    CurrentProject.Channels[PKursorX].FadeOut = TControls[5].ToInt32();
                    //Если хотим просто воспроизвести ноту
                    int note = KeyToNote(e, false);
                    if (note > 0) Audio.player.sinthesizer[PKursorX].Play(note);
                    //Копируем канал в другой
                    if (e.KeyCode == Keys.Enter & K == 2)
                    {
                        CurrentProject.Channels[TControls[1].ToInt32()] =
                            (Channel)CurrentProject.Channels[TControls[0].ToInt32()].Copy();


                        TXT.PrintAT(51, 3, 0, 14, "Скопировано!");
                        Refresh();
                    }
                    //Выходим из настройки синтезатора
                    if (e.KeyCode == Keys.Escape | (e.KeyCode == Keys.Enter & K != 2)) { EditMode = EditModes.Edit; DrawAll(); }
                    break;
                #endregion
                #region Настройка микшера
                case EditModes.Mixer:
                    if (e.KeyCode == Keys.Left)
                    {
                        if (PKursorX > 0)
                        {
                            PKursorX--;
                            if (PShift > PKursorX) PShift = PKursorX;
                        }
                    }
                    if (e.KeyCode == Keys.Right)
                    {
                        if (PKursorX < CurrentProject.ChannelCount - 1)
                        {
                            PKursorX++;
                            if (PShift < PKursorX - Columns + 1) PShift = PKursorX - Columns + 1;
                        }
                    }
                    if (e.KeyCode == Keys.Add && CurrentProject.Channels[PKursorX].Volume < 100)
                    {
                        CurrentProject.Channels[PKursorX].Volume += 5;
                        HistoryChange();
                    }
                    if (e.KeyCode == Keys.Subtract && CurrentProject.Channels[PKursorX].Volume > 0)
                    {
                        CurrentProject.Channels[PKursorX].Volume -= 5;
                        HistoryChange();
                    }
                    //Переключатели Mute
                    if (e.KeyCode == Keys.Space) MuteTogle(PKursorX);
                    if (e.KeyCode == Keys.S) Solo(PKursorX);
                    if (e.KeyCode == Keys.Enter) OnAllChannels();
                    DrawAll();
                    DrawMixer();
                    Refresh();
                    if (e.KeyCode == Keys.Escape) { EditMode = EditModes.Edit; menuStop_Click(null, null); DrawAll(); }
                    break;
                #endregion
            
            }
        }
        #endregion

        #region Всякие разные операции над клавиатурой
        /// <summary>
        /// Включение/выключение Mute на заданный канал
        /// </summary>
        /// <param name="ch">Канал</param>
        void MuteTogle(int ch)
        {
            Audio.player.sinthesizer[ch].Mute = !Audio.player.sinthesizer[ch].Mute;
        }

        /// <summary>
        /// Включение Соло на заданный канал
        /// </summary>
        /// <param name="ch">Канал</param>
        void Solo(int ch)
        {
            for (int i = 0; i < CurrentProject.ChannelCount; i++)
                Audio.player.sinthesizer[i].Mute = true;
            Audio.player.sinthesizer[ch].Mute = false;
        }

        /// <summary>
        /// Включение всех каналов
        /// </summary>
        void OnAllChannels()
        {
            for (int i = 0; i < CurrentProject.ChannelCount; i++)
                Audio.player.sinthesizer[i].Mute = false;
        }

        /// <summary>
        /// Нажатие буквы
        /// </summary>
        void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (EditMode)
            {
                case EditModes.Properties:
                case EditModes.SinSetup:
                    if (TControls[K].Type != TControl.Types.Button)
                    {
                        TControls[K].AddChar(e.KeyChar);
                        TControls[K].Draw(true);
                        RefreshScreen();
                    }
                    break;
            }
        }

        /// <summary>
        /// Перевод нажатой клавиши в ноту
        /// </summary>
        /// <param name="e">Клавиша</param>
        /// <param name="WithDigits">Использовать ли цифровые клавиши?</param>
        /// <returns>Возвращается код ноты</returns>
        int KeyToNote(KeyEventArgs e, bool WithDigits)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            int n = (Octave + 1) * 12;
            switch (e.KeyCode)
            {
                case Keys.Z: return n;                                      //нижние клавиши
                case Keys.S: if (!e.Control) return n + 1; else return 0;
                case Keys.X: return n + 2;
                case Keys.D: return n + 3;
                case Keys.C: return n + 4;
                case Keys.V: return n + 5;
                case Keys.G: return n + 6;
                case Keys.B: return n + 7;
                case Keys.H: return n + 8;
                case Keys.N: if (!e.Control) return n + 9; else return 0;
                case Keys.J: return n + 10;
                case Keys.M: return n + 11;
                case Keys.Oemcomma: return n + 12;                          //Нижние клавиши +
                case Keys.L: return n + 13;
                case Keys.OemPeriod: return n + 14;
                case Keys.Oem1: return n + 15;
                case Keys.OemQuestion: return n + 16;
                case Keys.Q: return n + 12;                                 //Нижние клавиши
                case Keys.D2: if (WithDigits) return n + 13; else return -1;
                case Keys.W: return n + 14;
                case Keys.D3: if (WithDigits) return n + 15; else return -1;
                case Keys.E: return n + 16;
                case Keys.R: return n + 17;
                case Keys.D5: if (WithDigits) return n + 18; else return -1;
                case Keys.T: return n + 19;
                case Keys.D6: if (WithDigits) return n + 20; else return -1;
                case Keys.Y: return n + 21;
                case Keys.D7: if (WithDigits) return n + 22; else return -1;
                case Keys.U: return n + 23;
                case Keys.I: return n + 24;                                 //Верхние клавиши +
                case Keys.D9: if (WithDigits) return n + 25; else return -1;
                case Keys.O: return n + 26;
                case Keys.D0: if (WithDigits) return n + 27; else return -1;
                case Keys.P: return n + 28;
                case Keys.OemOpenBrackets: return n + 29;
                case Keys.Oemplus: return n + 30;
                case Keys.Oem6: return n + 31;
                case Keys.Oemtilde: return 1;
            }
            return -1;
        }
        #endregion

        #region Копипасты
        byte[,] Clipboard;
        //Вырезать
        private void menucut_Click(object sender, EventArgs e)
        {
            if (Selected)
            {
                SelectFix();
                Clipboard = new byte[SelectX2 - SelectX1 + 1, SelectY2 - SelectY1 + 1];
                for (int i = SelectX1; i <= SelectX2; i++)
                    for (int j = SelectY1; j <= SelectY2; j++)
                    {
                        Clipboard[i - SelectX1, j - SelectY1] = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[i, j];
                        CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[i, j] = 0;
                    }
            }
            else
            {
                Clipboard = new byte[1, 1];
                Clipboard[0, 0] = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[PKursorX, PKursorY];
                CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[PKursorX, PKursorY] = 0;
            }
            DrawPattern();
            pictureBox1.Image = TXT.Screen;
            HistoryChange();
        }
        //Копировать
        private void menucopy_Click(object sender, EventArgs e)
        {
            if (Selected)
            {
                SelectFix();
                Clipboard = new byte[SelectX2 - SelectX1 + 1, SelectY2 - SelectY1 + 1];
                for (int i = SelectX1; i <= SelectX2; i++)
                    for (int j = SelectY1; j <= SelectY2; j++)
                        Clipboard[i - SelectX1, j - SelectY1] = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[i, j];
            }
            else
            {
                Clipboard = new byte[1, 1];
                Clipboard[0, 0] = CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[PKursorX, PKursorY];
            }
        }
        //Вставить
        private void menupaste_Click(object sender, EventArgs e)
        {
            if (Selected) //Вот здесь надо сделать по умному, если выделение уже есть, вставлить в левый верхний угол
            {
                SelectFix();
                if (PKursorX > SelectX1) PKursorX = SelectX1;
                if (PKursorY > SelectY1) PKursorY = SelectY1;
            }
            if (Clipboard == null) return;
            for (int i = 0; i < Clipboard.GetLength(0); i++)
                for (int j = 0; j < Clipboard.GetLength(1); j++)
                    try
                    {
                        CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[PKursorX + i, PKursorY + j] = Clipboard[i, j];
                    }
                    catch { }
            Selected = false;
            EditMode = 0;
            DrawPattern();
            pictureBox1.Image = TXT.Screen;
            HistoryChange();
        }
        //Удалить
        private void Delete()
        {

            if (Selected)
            {
                SelectFix();
                for (int i = SelectX1; i <= SelectX2; i++)
                    for (int j = SelectY1; j <= SelectY2; j++)
                        CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[i, j] = 0;
            }
            else
                CurrentProject.Patterns[CurrentProject.Track[TKursor]].Note[PKursorX, PKursorY] = 0;
            DrawPattern();
            pictureBox1.Image = TXT.Screen;
            HistoryChange();
        }
        //Исправление выделения, чтоб небыло ошибок с циклами
        void SelectFix()
        {
            if (SelectX1 > SelectX2)
            {
                int a = SelectX1;
                SelectX1 = SelectX2;
                SelectX2 = a;
            }
            if (SelectY1 > SelectY2)
            {
                int a = SelectY1;
                SelectY1 = SelectY2;
                SelectY2 = a;
            }
        }
        #endregion

        #region Проигрыватель
        //Остановка плеера
        private void menuStop_Click(object sender, EventArgs e)
        {
            if (Audio.player == null) return;
            Audio.player.Stop();
            //timerPlayer.Enabled = false;
            //foreach (Sinthesizer sin in Audio.player.sinthesizer) sin.Stop();
            for (int i = 0; i < CurrentProject.ChannelCount; i++) Audio.player.sinthesizer[i].Stop();
        }

        //Играть трек
        private void menuPlayTrack_Click(object sender, EventArgs e)
        {
            if (!PlayerEnabled()) return;
            if (Audio.player.Mode != Player.Modes.Stop) { menuStop_Click(null, null); return; }
            Audio.player.Play(CurrentProject, Player.Modes.PlayTrack, TKursor, PKursorY);
            //timerPlayer.Enabled = true;
            Selected = false;
        }

        //Играть трек сначала
        private void menuPlayTrackStart_Click(object sender, EventArgs e)
        {
            if (!PlayerEnabled()) return;
            if (Audio.player.Mode != Player.Modes.Stop) { menuStop_Click(null, null); return; }
            Audio.player.Play(CurrentProject, Player.Modes.PlayTrack, 0, 0);
            //timerPlayer.Enabled = true;
            Selected = false;
        }

        //Играть паттерн
        private void menuPlayPattern_Click(object sender, EventArgs e)
        {
            if (!PlayerEnabled()) return;
            if (Audio.player.Mode != Player.Modes.Stop) { menuStop_Click(null, null); return; }
            Audio.player.Play(CurrentProject, Player.Modes.PlayPattern, TKursor, PKursorY);
            //timerPlayer.Enabled = true;
            Selected = false;
        }

        //Играть паттерн сначала
        private void menuPlayPatternStart_Click(object sender, EventArgs e)
        {
            if (!PlayerEnabled()) return;
            if (Audio.player.Mode != Player.Modes.Stop) { menuStop_Click(null, null); return; }
            Audio.player.Play(CurrentProject, Player.Modes.PlayPattern, TKursor, 0);
            //timerPlayer.Enabled = true;
            Selected = false;
        }

        /// <summary>
        /// Можно ли плееру в этот момент включиться?
        /// </summary>
        /// <returns></returns>
        bool PlayerEnabled()
        {
            if (EditMode != EditModes.Edit &
                EditMode != EditModes.Mixer &
                EditMode != EditModes.Track) return false;
            return true;
        }

        // Обновление визуализации
        private void timerPlayer_Tick(object sender, EventArgs e)
        {
            if (Audio.player.Mode != Player.Modes.Stop)
            {
                TKursor = Audio.player.PatternNum;
                PKursorY = Audio.player.NoteNum;
                DrawAll();
            }
            if (EditMode == EditModes.Edit) DrawVisual();
            if (EditMode == EditModes.Mixer) { DrawVisual(); DrawMixer(); }
            //DrawVisual();
            RefreshScreen();
        }
        #endregion

        /// <summary>
        /// Сброс позиций курсора при создании или открытии нового документа
        /// </summary>
        void ResetKursors()
        {
            PKursorX = 0;   //Курсор паттерна (канал)
            PKursorY = 0;   //Курсор паттерна (нота)
            PShift = 0;     //Сдвиг колонок каналов
            TKursor = 0;    //Курсор трека
            TShift = 0;     //Сдвиг полоски трека
        }

        /// <summary>
        /// Начало выделения
        /// </summary>
        void StartSelect()
        {
            if (!Selected)
            {
                Selected = true;
                SelectX1 = PKursorX;
                SelectY1 = PKursorY;
            }
            SelectX2 = PKursorX;
            SelectY2 = PKursorY;
        }

        /// <summary>
        /// Сдвиг паттернов в треке правей курсора для вставки нового
        /// </summary>
        /// <returns>Возвращает True, если операция прошла удачно</returns>
        bool PushPats()
        {
            if (CurrentProject.TrackLen >= Project.MaxTrackLenght) return false;
            CurrentProject.TrackLen++;
            for (int i = CurrentProject.TrackLen; i > TKursor + 1; i--)
                CurrentProject.Track[i] = CurrentProject.Track[i - 1];
            return true;
        }
    }
}
