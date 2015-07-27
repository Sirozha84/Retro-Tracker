namespace RetroTracker
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menunew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuopen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menusave = new System.Windows.Forms.ToolStripMenuItem();
            this.menusaveas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuexit = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuundo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuredo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menucut = new System.Windows.Forms.ToolStripMenuItem();
            this.menucopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menupaste = new System.Windows.Forms.ToolStripMenuItem();
            this.трекToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеДорожкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.синтезаторToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.микшерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьКаналToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьКаналToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проигрываниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlayTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlayTrackStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlayPattern = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlayPatternStart = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuhelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuabout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolnew = new System.Windows.Forms.ToolStripButton();
            this.toolopen = new System.Windows.Forms.ToolStripButton();
            this.toolsave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolcut = new System.Windows.Forms.ToolStripButton();
            this.toolcopy = new System.Windows.Forms.ToolStripButton();
            this.toolpaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolundo = new System.Windows.Forms.ToolStripButton();
            this.toolredo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerFlash = new System.Windows.Forms.Timer(this.components);
            this.timerPlayer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem,
            this.трекToolStripMenuItem,
            this.синтезаторToolStripMenuItem,
            this.проигрываниеToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1279, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menunew,
            this.menuopen,
            this.toolStripSeparator2,
            this.menusave,
            this.menusaveas,
            this.toolStripSeparator1,
            this.menuexit});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // menunew
            // 
            this.menunew.Image = global::RetroTracker.Properties.Resources.Новый;
            this.menunew.Name = "menunew";
            this.menunew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menunew.Size = new System.Drawing.Size(173, 22);
            this.menunew.Text = "Новый";
            this.menunew.Click += new System.EventHandler(this.menunew_Click);
            // 
            // menuopen
            // 
            this.menuopen.Image = global::RetroTracker.Properties.Resources.Открыть;
            this.menuopen.Name = "menuopen";
            this.menuopen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuopen.Size = new System.Drawing.Size(173, 22);
            this.menuopen.Text = "Открыть...";
            this.menuopen.Click += new System.EventHandler(this.menuopen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // menusave
            // 
            this.menusave.Image = global::RetroTracker.Properties.Resources.Сохранить;
            this.menusave.Name = "menusave";
            this.menusave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menusave.Size = new System.Drawing.Size(173, 22);
            this.menusave.Text = "Сохранить";
            this.menusave.Click += new System.EventHandler(this.menusave_Click);
            // 
            // menusaveas
            // 
            this.menusaveas.Name = "menusaveas";
            this.menusaveas.Size = new System.Drawing.Size(173, 22);
            this.menusaveas.Text = "Сохранить как...";
            this.menusaveas.Click += new System.EventHandler(this.menusaveas_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // menuexit
            // 
            this.menuexit.Name = "menuexit";
            this.menuexit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuexit.Size = new System.Drawing.Size(173, 22);
            this.menuexit.Text = "Выход";
            this.menuexit.Click += new System.EventHandler(this.menuexit_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuundo,
            this.menuredo,
            this.toolStripMenuItem1,
            this.menucut,
            this.menucopy,
            this.menupaste});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // menuundo
            // 
            this.menuundo.Image = global::RetroTracker.Properties.Resources.Отменить;
            this.menuundo.Name = "menuundo";
            this.menuundo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuundo.Size = new System.Drawing.Size(181, 22);
            this.menuundo.Text = "Отменить";
            this.menuundo.Visible = false;
            this.menuundo.Click += new System.EventHandler(this.menuundo_Click);
            // 
            // menuredo
            // 
            this.menuredo.Image = global::RetroTracker.Properties.Resources.Повторить;
            this.menuredo.Name = "menuredo";
            this.menuredo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuredo.Size = new System.Drawing.Size(181, 22);
            this.menuredo.Text = "Повторить";
            this.menuredo.Visible = false;
            this.menuredo.Click += new System.EventHandler(this.menuredo_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
            this.toolStripMenuItem1.Visible = false;
            // 
            // menucut
            // 
            this.menucut.Image = global::RetroTracker.Properties.Resources.Вырезать;
            this.menucut.Name = "menucut";
            this.menucut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menucut.Size = new System.Drawing.Size(181, 22);
            this.menucut.Text = "Вырезать";
            this.menucut.Click += new System.EventHandler(this.menucut_Click);
            // 
            // menucopy
            // 
            this.menucopy.Image = global::RetroTracker.Properties.Resources.Копировать;
            this.menucopy.Name = "menucopy";
            this.menucopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menucopy.Size = new System.Drawing.Size(181, 22);
            this.menucopy.Text = "Копировать";
            this.menucopy.Click += new System.EventHandler(this.menucopy_Click);
            // 
            // menupaste
            // 
            this.menupaste.Image = global::RetroTracker.Properties.Resources.Вставить;
            this.menupaste.Name = "menupaste";
            this.menupaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menupaste.Size = new System.Drawing.Size(181, 22);
            this.menupaste.Text = "Вставить";
            this.menupaste.Click += new System.EventHandler(this.menupaste_Click);
            // 
            // трекToolStripMenuItem
            // 
            this.трекToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактированиеДорожкиToolStripMenuItem,
            this.параметрыToolStripMenuItem});
            this.трекToolStripMenuItem.Name = "трекToolStripMenuItem";
            this.трекToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.трекToolStripMenuItem.Text = "Трек";
            // 
            // редактированиеДорожкиToolStripMenuItem
            // 
            this.редактированиеДорожкиToolStripMenuItem.Name = "редактированиеДорожкиToolStripMenuItem";
            this.редактированиеДорожкиToolStripMenuItem.ShortcutKeyDisplayString = "Tab";
            this.редактированиеДорожкиToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.редактированиеДорожкиToolStripMenuItem.Text = "Трек/Паттерн";
            this.редактированиеДорожкиToolStripMenuItem.Click += new System.EventHandler(this.редактированиеДорожкиToolStripMenuItem_Click);
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            this.параметрыToolStripMenuItem.Click += new System.EventHandler(this.параметрыToolStripMenuItem_Click);
            // 
            // синтезаторToolStripMenuItem
            // 
            this.синтезаторToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкаToolStripMenuItem,
            this.микшерToolStripMenuItem,
            this.добавитьКаналToolStripMenuItem,
            this.удалитьКаналToolStripMenuItem});
            this.синтезаторToolStripMenuItem.Name = "синтезаторToolStripMenuItem";
            this.синтезаторToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.синтезаторToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.синтезаторToolStripMenuItem.Text = "Канал";
            // 
            // настройкаToolStripMenuItem
            // 
            this.настройкаToolStripMenuItem.Name = "настройкаToolStripMenuItem";
            this.настройкаToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.настройкаToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.настройкаToolStripMenuItem.Text = "Настройка синтезатора";
            this.настройкаToolStripMenuItem.Click += new System.EventHandler(this.настройкаToolStripMenuItem_Click);
            // 
            // микшерToolStripMenuItem
            // 
            this.микшерToolStripMenuItem.Name = "микшерToolStripMenuItem";
            this.микшерToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.микшерToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.микшерToolStripMenuItem.Text = "Микшер";
            this.микшерToolStripMenuItem.Click += new System.EventHandler(this.микшерToolStripMenuItem_Click);
            // 
            // добавитьКаналToolStripMenuItem
            // 
            this.добавитьКаналToolStripMenuItem.Name = "добавитьКаналToolStripMenuItem";
            this.добавитьКаналToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.добавитьКаналToolStripMenuItem.Text = "Добавить канал";
            this.добавитьКаналToolStripMenuItem.Click += new System.EventHandler(this.добавитьКаналToolStripMenuItem_Click);
            // 
            // удалитьКаналToolStripMenuItem
            // 
            this.удалитьКаналToolStripMenuItem.Name = "удалитьКаналToolStripMenuItem";
            this.удалитьКаналToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.удалитьКаналToolStripMenuItem.Text = "Удалить канал";
            this.удалитьКаналToolStripMenuItem.Click += new System.EventHandler(this.удалитьКаналToolStripMenuItem_Click);
            // 
            // проигрываниеToolStripMenuItem
            // 
            this.проигрываниеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStop,
            this.menuPlayTrack,
            this.menuPlayTrackStart,
            this.menuPlayPattern,
            this.menuPlayPatternStart});
            this.проигрываниеToolStripMenuItem.Name = "проигрываниеToolStripMenuItem";
            this.проигрываниеToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.проигрываниеToolStripMenuItem.Text = "Проигрывание";
            // 
            // menuStop
            // 
            this.menuStop.Image = global::RetroTracker.Properties.Resources.Stop;
            this.menuStop.Name = "menuStop";
            this.menuStop.ShortcutKeyDisplayString = "Любая клавиша";
            this.menuStop.Size = new System.Drawing.Size(225, 22);
            this.menuStop.Text = "Стоп";
            this.menuStop.Click += new System.EventHandler(this.menuStop_Click);
            // 
            // menuPlayTrack
            // 
            this.menuPlayTrack.Image = global::RetroTracker.Properties.Resources.PlayTrack;
            this.menuPlayTrack.Name = "menuPlayTrack";
            this.menuPlayTrack.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuPlayTrack.Size = new System.Drawing.Size(225, 22);
            this.menuPlayTrack.Text = "Играть трек";
            this.menuPlayTrack.Click += new System.EventHandler(this.menuPlayTrack_Click);
            // 
            // menuPlayTrackStart
            // 
            this.menuPlayTrackStart.Image = global::RetroTracker.Properties.Resources.PlayTrackStart;
            this.menuPlayTrackStart.Name = "menuPlayTrackStart";
            this.menuPlayTrackStart.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.menuPlayTrackStart.Size = new System.Drawing.Size(225, 22);
            this.menuPlayTrackStart.Text = "Играть трек сначала";
            this.menuPlayTrackStart.Click += new System.EventHandler(this.menuPlayTrackStart_Click);
            // 
            // menuPlayPattern
            // 
            this.menuPlayPattern.Image = global::RetroTracker.Properties.Resources.PlayPat;
            this.menuPlayPattern.Name = "menuPlayPattern";
            this.menuPlayPattern.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.menuPlayPattern.Size = new System.Drawing.Size(225, 22);
            this.menuPlayPattern.Text = "Играть паттерн";
            this.menuPlayPattern.Click += new System.EventHandler(this.menuPlayPattern_Click);
            // 
            // menuPlayPatternStart
            // 
            this.menuPlayPatternStart.Image = global::RetroTracker.Properties.Resources.PlayPatStart;
            this.menuPlayPatternStart.Name = "menuPlayPatternStart";
            this.menuPlayPatternStart.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.menuPlayPatternStart.Size = new System.Drawing.Size(225, 22);
            this.menuPlayPatternStart.Text = "Играть паттерн сначала";
            this.menuPlayPatternStart.Click += new System.EventHandler(this.menuPlayPatternStart_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuhelp,
            this.toolStripSeparator4,
            this.menuabout});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // menuhelp
            // 
            this.menuhelp.Image = global::RetroTracker.Properties.Resources.Справка;
            this.menuhelp.Name = "menuhelp";
            this.menuhelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuhelp.Size = new System.Drawing.Size(198, 22);
            this.menuhelp.Text = "Просмотр справки";
            this.menuhelp.Click += new System.EventHandler(this.menuhelp_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(195, 6);
            // 
            // menuabout
            // 
            this.menuabout.Name = "menuabout";
            this.menuabout.Size = new System.Drawing.Size(198, 22);
            this.menuabout.Text = "О программе";
            this.menuabout.Click += new System.EventHandler(this.menuabout_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolnew,
            this.toolopen,
            this.toolsave,
            this.toolStripSeparator3,
            this.toolcut,
            this.toolcopy,
            this.toolpaste,
            this.toolStripSeparator5,
            this.toolundo,
            this.toolredo,
            this.toolStripSeparator6,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1279, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolnew
            // 
            this.toolnew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolnew.Image = global::RetroTracker.Properties.Resources.Новый;
            this.toolnew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolnew.Name = "toolnew";
            this.toolnew.Size = new System.Drawing.Size(23, 22);
            this.toolnew.Text = "Новый (Ctrl + N)";
            this.toolnew.Click += new System.EventHandler(this.toolnew_Click);
            // 
            // toolopen
            // 
            this.toolopen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolopen.Image = global::RetroTracker.Properties.Resources.Открыть;
            this.toolopen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolopen.Name = "toolopen";
            this.toolopen.Size = new System.Drawing.Size(23, 22);
            this.toolopen.Text = "Открыть (Ctrl + O)";
            this.toolopen.Click += new System.EventHandler(this.toolopen_Click);
            // 
            // toolsave
            // 
            this.toolsave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolsave.Image = global::RetroTracker.Properties.Resources.Сохранить;
            this.toolsave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsave.Name = "toolsave";
            this.toolsave.Size = new System.Drawing.Size(23, 22);
            this.toolsave.Text = "Сохранить (Ctrl + S)";
            this.toolsave.Click += new System.EventHandler(this.toolsave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolcut
            // 
            this.toolcut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolcut.Image = global::RetroTracker.Properties.Resources.Вырезать;
            this.toolcut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolcut.Name = "toolcut";
            this.toolcut.Size = new System.Drawing.Size(23, 22);
            this.toolcut.Text = "Вырезать (Ctrl + X)";
            this.toolcut.Click += new System.EventHandler(this.toolcut_Click);
            // 
            // toolcopy
            // 
            this.toolcopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolcopy.Image = global::RetroTracker.Properties.Resources.Копировать;
            this.toolcopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolcopy.Name = "toolcopy";
            this.toolcopy.Size = new System.Drawing.Size(23, 22);
            this.toolcopy.Text = "Копировать (Ctrl + C)";
            this.toolcopy.Click += new System.EventHandler(this.toolcopy_Click);
            // 
            // toolpaste
            // 
            this.toolpaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolpaste.Image = global::RetroTracker.Properties.Resources.Вставить;
            this.toolpaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolpaste.Name = "toolpaste";
            this.toolpaste.Size = new System.Drawing.Size(23, 22);
            this.toolpaste.Text = "Вставить (Ctrl + V)";
            this.toolpaste.Click += new System.EventHandler(this.toolpaste_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolundo
            // 
            this.toolundo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolundo.Image = global::RetroTracker.Properties.Resources.Отменить;
            this.toolundo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolundo.Name = "toolundo";
            this.toolundo.Size = new System.Drawing.Size(23, 22);
            this.toolundo.Text = "Отменить (Ctrl+Z)";
            this.toolundo.Visible = false;
            this.toolundo.Click += new System.EventHandler(this.toolundo_Click);
            // 
            // toolredo
            // 
            this.toolredo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolredo.Image = global::RetroTracker.Properties.Resources.Повторить;
            this.toolredo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolredo.Name = "toolredo";
            this.toolredo.Size = new System.Drawing.Size(23, 22);
            this.toolredo.Text = "Повторить (Ctrl+Y)";
            this.toolredo.Visible = false;
            this.toolredo.Click += new System.EventHandler(this.toolredo_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator6.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::RetroTracker.Properties.Resources.Stop;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Стоп (Любая клавиша)";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::RetroTracker.Properties.Resources.PlayTrack;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Играть трек (F5)";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::RetroTracker.Properties.Resources.PlayTrackStart;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "Играть трек сначала (F6)";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::RetroTracker.Properties.Resources.PlayPat;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "Играть паттерн (F7)";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::RetroTracker.Properties.Resources.PlayPatStart;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "Играть паттерн сначала (F8)";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 751);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1279, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.IsLink = true;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 19);
            this.toolStripStatusLabel1.Text = "Сайт автора";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1279, 700);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // timerFlash
            // 
            this.timerFlash.Interval = 200;
            this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick);
            // 
            // timerPlayer
            // 
            this.timerPlayer.Enabled = true;
            this.timerPlayer.Tick += new System.EventHandler(this.timerPlayer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 775);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormMain_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menunew;
        private System.Windows.Forms.ToolStripMenuItem menuopen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menusave;
        private System.Windows.Forms.ToolStripMenuItem menusaveas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuexit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolnew;
        private System.Windows.Forms.ToolStripButton toolopen;
        private System.Windows.Forms.ToolStripButton toolsave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuhelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuabout;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menucut;
        private System.Windows.Forms.ToolStripMenuItem menucopy;
        private System.Windows.Forms.ToolStripMenuItem menupaste;
        private System.Windows.Forms.ToolStripButton toolcut;
        private System.Windows.Forms.ToolStripButton toolcopy;
        private System.Windows.Forms.ToolStripButton toolpaste;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem menuundo;
        private System.Windows.Forms.ToolStripMenuItem menuredo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolundo;
        private System.Windows.Forms.ToolStripButton toolredo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem проигрываниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuStop;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem menuPlayTrack;
        private System.Windows.Forms.ToolStripMenuItem menuPlayTrackStart;
        private System.Windows.Forms.ToolStripMenuItem menuPlayPattern;
        private System.Windows.Forms.ToolStripMenuItem menuPlayPatternStart;
        private System.Windows.Forms.Timer timerFlash;
        private System.Windows.Forms.Timer timerPlayer;
        private System.Windows.Forms.ToolStripMenuItem трекToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem синтезаторToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem микшерToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеДорожкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьКаналToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьКаналToolStripMenuItem;
    }
}

