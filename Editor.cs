using System;
using System.IO;
using System.Windows.Forms;

class Editor
{
    //Константы программы
    public const string ProgramName = "Retro Tracker";
    public const string ProgramVersion = "1.0 - 5 июля 2015 года";
    public const string ProgramAutor = "Гордеев Сергей";
    public const string FileUnnamed = "Безымянный";
    public const string FileType = "Ретро трек (*.rtr)|*.rtr|Все файлы (*.*)|*.*";
    public const string Site = "www.sg-software.ru";
    public const string SiteFull = "http://www.sg-software.ru";
    static string ParametersFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "SG\\Retro Tracker"; //Папка с конфигурацией программы
    static string ParametersFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "SG\\Retro Tracker\\config.cfg"; //Файл конфигурации программы
    //Инициализация параметров программы
    public static void init()
    {
        try
        {
            //Пробуем загрузить настройки, если они были сохранены
            BinaryReader file = new BinaryReader(new FileStream(ParametersFile, FileMode.Open));
            WindowsPosirion.X = file.ReadInt32();
            WindowsPosirion.Y = file.ReadInt32();
            WindowsPosirion.Width = file.ReadInt32();
            WindowsPosirion.Heidht = file.ReadInt32();
            WindowsPosirion.Max = file.ReadBoolean();
            file.Close();
        }
        catch { }
    }
    //Сохранение параметров программы
    public static void saveconfig()
    {
        try
        {
            Directory.CreateDirectory(ParametersFolder);
            BinaryWriter file = new BinaryWriter(new FileStream(ParametersFile, FileMode.Create));
            file.Write(WindowsPosirion.X);
            file.Write(WindowsPosirion.Y);
            file.Write(WindowsPosirion.Width);
            file.Write(WindowsPosirion.Heidht);
            file.Write(WindowsPosirion.Max);
            file.Close();
        }
        catch { }
    }
    //Злобное сообщение об ошибке
    public static void Error(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    //Не злобное сообщение
    public static void Message(string message)
    {
        MessageBox.Show(message, ProgramName);
    }
}

public class WindowsPosirion
{
    static public int X = 100;
    static public int Y = 100;
    static public int Width = 800;
    static public int Heidht = 800;
    static public bool Max = false;
}