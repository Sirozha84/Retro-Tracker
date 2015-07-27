using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RetroTracker
{
    public partial class FormMain : Form
    {
        Project CurrentProject = new Project();
        List<Project> History = new List<Project>();
        int HistoryNumber;
        bool ProgramTextChange = true;
        System.Diagnostics.Process Help = new System.Diagnostics.Process();

        public FormMain()
        {
            Audio.InitAudio();
            InitializeComponent();
            Editor.init();
            Left = WindowsPosirion.X;
            Top = WindowsPosirion.Y;
            //Width = WindowsPosirion.Width;
            //Height = WindowsPosirion.Heidht;
            if (WindowsPosirion.Max) WindowState = FormWindowState.Maximized; else WindowState = FormWindowState.Normal;
            menunew_Click(null, null);
            SetFormText();
        }
        
        /// <summary>
        /// Создание нового файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menunew_Click(object sender, EventArgs e)
        {
            menuStop_Click(null, null);
            if (!SaveQuestion()) return;
            CurrentProject.NewProject();
            EditMode = EditModes.Edit;
            ResetKursors();
            DrawDocument();
            HistoryReset();
        }

        /// <summary>
        /// Открытие файла
        /// </summary>
        void FileOpen()
        {
            menuStop_Click(null, null);
            if (!SaveQuestion()) return;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = Editor.FileType;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            Project.FileName = openFileDialog1.FileName;
            if (!CurrentProject.Open()) return;
            EditMode = EditModes.Edit;
            ResetKursors();
            DrawDocument();
            HistoryReset();
            //Audio.InitAudio(CurrentProject);
        }

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <returns></returns>
        bool FileSave()
        {
            menuStop_Click(null, null);
            if (Project.FileName == Editor.FileUnnamed && !FileSaveAs()) return false;
            if (!CurrentProject.Save()) return false;
            Project.Changed = false;
            SetFormText();
            return true;
        }
        
        /// <summary>
        /// Сохранение файла как
        /// </summary>
        /// <returns></returns>
        bool FileSaveAs()
        {
            menuStop_Click(null, null);
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = Editor.FileType;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Project.FileName = saveFileDialog1.FileName;
                Project.Changed = false;
                SetFormText();
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuundo_Click(object sender, EventArgs e)
        {
            if (HistoryNumber < 2) return;
            HistoryNumber--;
            //CurrentProject = History[HistoryNumber - 1].Copy();
            DrawDocument();
            Project.Changed = true;
            SetFormText();
        }
        
        /// <summary>
        /// Возврат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuredo_Click(object sender, EventArgs e)
        {
            if (HistoryNumber == History.Count) return;
            HistoryNumber++;
            //CurrentProject = History[HistoryNumber - 1].Copy();
            DrawDocument();
            Project.Changed = true;
            SetFormText();
        }
        
        /// <summary>
        /// Вызов справки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuhelp_Click(object sender, EventArgs e)
        {
            //try { HelpClose(); Help.StartInfo.FileName = "help.chm"; Help.Start(); }
            //catch { Editor.Error("Файл справки не найден."); } 
            MessageBox.Show("Режим редактирование паттерна\n" +
                "Z,S,X,D,C..... - нижняя клавиатура\n" +
                "Q,2,W,3,E..... - верхняя клавиатура (октава + 1)\n" +
                "1,2,3,4,5,6 на цифровой клавиатуре - выбор октавы\n" +
                "Пробел - автошаг (если больше 0)\n" +
                "\"/\", \"*\" - измениние автошага \n" +
                "Enter - Прослушивание текущей позиции\n" +
                "\"+\", \"-\" - изменение темпа\n" +
                "Ctrl+\"+\", Ctrl+\"-\" - изменение громкости канала\n" +
                "Ctrl+Пробел - вкючить/выключить Mute\n" +
                "Ctrl+Alt+S - включить Solo\n" +
                "Ctrl+Enter - включить все каналы\n" +
                "~ - вставка остановки (заглушает играющую на этом канале ноту)\n" +
                //"Delete - удаление ноты\n" +
                "\n" +
                "Режим редактирования трека\n" +
                "Ctrl+Влево/Ctrl+Вправо\n - перемещать паттерн" +
                "N - создать новый паттерн\n" +
                "D - дублировать паттерн (создать его повтор)\n" +
                "С - клонировать паттерн (создать копию для изменения)\n" +
                //"Delete - удаление паттерна (не полное, при создании нового с этим же номером восстанавливает удалённый," +
                //"но не при клонировании на этот номер)",
                "\n" +
                "Режим микшера\n" +
                "\"+\", \"-\" - изменение громкости канала\n" +
                "Пробел - вкючить/выключить Mute\n" +
                "S - включить Solo\n" +
                "Enter - включить все каналы\n",
                "Используемые горячие клавиши", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// Закрытие программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveQuestion()) e.Cancel = true;
            if (WindowState == FormWindowState.Maximized) WindowsPosirion.Max = true;
            else WindowsPosirion.Max = false;
            Editor.saveconfig();
            HelpClose();
        }
        
        /// <summary>
        /// Рисование документа
        /// </summary>
        void DrawDocument()
        {
            ProgramTextChange = true;
            SetFormText();
            DrawAll();
        }

        /// <summary>
        /// Рисование имени файла и программы
        /// </summary>
        void SetFormText()
        {
            string star = ""; 
            if (Project.Changed) star = "*";
            //Text = System.IO.Path.GetFileNameWithoutExtension(Project.FileName) + star + " - " + Editor.ProgramName;
            Text = CurrentProject.SongName + star + " - " + Editor.ProgramName;
        }

        /// <summary>
        /// Регистрация изменений
        /// </summary>
        void HistoryChange() 
        { 
            Project.Changed = true; 
            SetFormText();
            //Создание отмены (что-то пока не хочется её делать.. либо потом, либо вообще забить)
            while (HistoryNumber < History.Count) History.RemoveAt(History.Count - 1);
            //History.Add(CurrentProject.Copy());
            HistoryNumber++;
        }

        /// <summary>
        /// Сброс истории
        /// </summary>
        void HistoryReset()
        {
            History.Clear();
            HistoryNumber = 0;
            Project.Changed = false;
            SetFormText();
        }

        /// <summary>
        /// регистрация изменений окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            WindowsPosirion.X = Left;
            WindowsPosirion.Y = Top;
            WindowsPosirion.Width = Width;
            WindowsPosirion.Heidht = Height;
            WindowsPosirion.Max = false;
        }

        /// <summary>
        /// Вопрос перед уничтожением проекта
        /// </summary>
        /// <returns></returns>
        public bool SaveQuestion()
        {
            if (!Project.Changed) return true;
            switch (MessageBox.Show("Сохранить изменения в файле \"" + System.IO.Path.GetFileNameWithoutExtension(Project.FileName) + "\"?", "Файл изменён",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes: return FileSave();
                case DialogResult.No: return true;
                case DialogResult.Cancel: return false;
            }
            return false;
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = Editor.Site;
            DrawAll();
        }

        private void timerFlash_Tick(object sender, EventArgs e)
        {
            Flash = 0;
            DrawPanel();
            Refresh();
            timerFlash.Enabled = false;
        }

        //Переход к параметрам трека
        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStop_Click(null, null);
            EditMode = EditModes.Properties;
            DrawAll();
            //if (EditMode != EditModes.Edit & EditMode != EditModes.Select) return;
            //Готовим контролы для диалогового окна
            TControls = new TControl[7];
            TControls[0] = new TControl(34, 8, CurrentProject.SongName, 20);
            TControls[1] = new TControl(34, 10, CurrentProject.AlbumName, 20);
            TControls[2] = new TControl(34, 12, CurrentProject.AutorName, 20);
            TControls[3] = new TControl(40, 14, CurrentProject.PatternLen, 3, 4, Pattern.MaxPatternLen);
            TControls[4] = new TControl(40, 16, CurrentProject.TaktLen, 3, 2, 16);
            TControls[5] = new TControl(31, 18, "   OK   ");
            TControls[6] = new TControl(41, 18, " Отмена ");
            K = 0;
            DrawProperties();
            RefreshScreen();
        }

        //Переход к настройке синтезатора
        private void настройкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStop_Click(null, null);
            EditMode = EditModes.SinSetup;
            DrawAll();
            //if (EditMode != EditModes.Edit & EditMode != EditModes.Select) return;
            TControls = new TControl[10];
            TControls[0] = new TControl(23, 3, PKursorX, 3, 0, CurrentProject.ChannelCount - 1);
            TControls[1] = new TControl(46, 3, PKursorX, 3, 0, CurrentProject.ChannelCount - 1);
            TControls[2] = new TControl(51, 3, " Копировать ");
            TControls[3] = new TControl(27, 5, CurrentProject.Channels[PKursorX].Atack, 3, 0, 999);
            TControls[4] = new TControl(43, 5, CurrentProject.Channels[PKursorX].Hold, 3, 0, 999);
            TControls[5] = new TControl(59, 5, CurrentProject.Channels[PKursorX].FadeOut, 3, 0, 999);
            TControls[6] = new TControl(36, 9, CurrentProject.Channels[PKursorX].Len, 2, 1, 64);
            TControls[7] = new TControl(48, 9, CurrentProject.Channels[PKursorX].Repeat, 2, 0, 63);
            TControls[8] = new TControl(26, 12, CurrentProject.Channels[PKursorX]);
            TControls[9] = new TControl(53, 23, " Закрыть ");
            K = 0;
            DrawSinSetup();
            RefreshScreen();
        }

        //Переход в микшер
        private void микшерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditMode = EditModes.Mixer;
            DrawAll();
            DrawMixer();
        }

        //Переход в редактирование трека
        private void редактированиеДорожкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditMode == EditModes.Track)
            {
                EditMode = EditModes.Edit;
                DrawAll();
                return;
            }
            if (EditMode == EditModes.Edit)
            {
                EditMode = EditModes.Track;
                DrawAll();
                return;
            }
        }
        //Добавление канала
        private void добавитьКаналToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProject.ChannelCount < Audio.MaxChannels)
            {
                CurrentProject.ChannelCount++;
                Audio.ChanCount = CurrentProject.ChannelCount;
                DrawAll();
                HistoryChange();
            }
        }

        //Удаление канала
        private void удалитьКаналToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentProject.ChannelCount > 1)
            {
                CurrentProject.ChannelCount--;
                Audio.ChanCount = CurrentProject.ChannelCount;
                if (PKursorX > CurrentProject.ChannelCount - 1) PKursorX = CurrentProject.ChannelCount - 1;
                DrawAll();
                HistoryChange();
            }
        }

        //Прочие мелочи
        void RefreshScreen() { pictureBox1.Image = TXT.Screen; }
        void HelpClose() { try { Help.Kill(); } catch { } }
        private void menusave_Click(object sender, EventArgs e) { FileSave(); }
        private void menusaveas_Click(object sender, EventArgs e) { if (FileSaveAs()) FileSave(); }
        private void menuexit_Click(object sender, EventArgs e) { this.Close(); }
        private void menuabout_Click(object sender, EventArgs e) { FormAbout form = new FormAbout(); form.ShowDialog(); }
        private void toolnew_Click(object sender, EventArgs e) { menunew_Click(null, null); }
        private void toolopen_Click(object sender, EventArgs e) { menuopen_Click(null, null); }
        private void toolsave_Click(object sender, EventArgs e) { menusave_Click(null, null); }
        private void toolcut_Click(object sender, EventArgs e) { menucut_Click(null, null); }
        private void toolcopy_Click(object sender, EventArgs e) { menucopy_Click(null, null); }
        private void toolpaste_Click(object sender, EventArgs e) { menupaste_Click(null, null); }
        private void toolundo_Click(object sender, EventArgs e) { menuundo_Click(null, null); }
        private void toolredo_Click(object sender, EventArgs e) { menuredo_Click(null, null); }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start(Editor.SiteFull); }
        private void menuopen_Click(object sender, EventArgs e) { FileOpen(); }
        private void toolStripButton1_Click(object sender, EventArgs e) { menuStop_Click(null, null); }
        private void toolStripButton2_Click(object sender, EventArgs e) { menuPlayTrack_Click(null, null); }
        private void toolStripButton3_Click(object sender, EventArgs e) { menuPlayTrackStart_Click(null, null); }
        private void toolStripButton4_Click(object sender, EventArgs e) { menuPlayPattern_Click(null, null); }
        private void toolStripButton5_Click(object sender, EventArgs e) { menuPlayPatternStart_Click(null, null); }
    }
}