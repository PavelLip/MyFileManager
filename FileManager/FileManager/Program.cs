using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    class Program
    {
        private static string[] lsc =
        {
            "cd",
            "select",
            "lsc",
            "exit"

        };
        private static List<string> command = new List<string>()
        {
            "cd",
            "select",
            "delete",
            "copy",
            "exit",
            "lsc"
        };

        //public static string path = "C:\u005c";
        public static int numberOnFilesPerPage = Console.WindowHeight - 5;
        public static int publicPageDir = 1;
        public static string selectFileonDIr;
        public static char symDelimiter;
        public static int minHeightWinConsole;
        public static int minWidthWinConsole;
        public static string path;

        static void Main(string[] args)
        {

            (path, symDelimiter, minHeightWinConsole, minWidthWinConsole) = Config.ReadAllSettings();
            Field.UpdateWindow();
            DataOutput.ShowFiles(path);
            bool exit = true;
            while (exit)
            {
                string commandUser = Console.ReadLine();
                string command = DefiningCommands(commandUser);
                switch (command)
                {

                    case "lsc": //показать доступные команды
                        Field.UpdateWindow();
                        Console.SetCursorPosition(0, 0);
                        for (int i = 0; i < lsc.Length; i++)
                        {
                            Console.WriteLine(lsc[i]);
                        }
                        Field.AddPathForInfoNoCommand(Program.path);
                        break;

                    case "cd": //переход в дирректорию по полному пути (исправить)
                        try
                        {
                            GoToDirectory(commandUser, "cd");
                            Field.UpdateWindow();
                            DataOutput.ShowFiles(path);
                        }
                        catch (Exception)
                        {
                            Field.UpdateWindow();
                            path = Select.PathFile(path) + "\\";
                            DataOutput.ShowFiles(path);
                            Field.NoFolder();
                        }
                        break;

                    case "select": // выбор файла или папки
                        if (File.Exists(Select.FileNameFull(command, commandUser)))
                        {
                            Field.UpdateWindow();
                            selectFileonDIr = Select.DirName("select ", commandUser);
                            GoToDirectory(commandUser, "select");
                            Field.AddPathForInfoFile(Select.PathFile(path), selectFileonDIr);
                            DataOutput.NextOrPreviousPage(DataOutput.Output(Select.PathFile(path)), publicPageDir);
                            CopyFile.CopyOrDeleteFile();
                        }
                        else if (Directory.Exists(Select.FileNameFull(command, commandUser)))
                        {
                            Field.UpdateWindow();
                            selectFileonDIr = Select.DirName("select ", commandUser);
                            GoToDirectory(commandUser, "select");
                            Field.AddPathForInfo(path, selectFileonDIr);
                            DataOutput.NextOrPreviousPage(DataOutput.Output(path), publicPageDir);
                            CopyFile.CopyOrDeleteDir();
                        }
                        else
                        {
                            Field.UpdateWindow();
                            DataOutput.ShowFiles(path);
                            Field.NoFolderAndFile();
                        }
                        break;

                    case "exit": //выход из программы
                        exit = false;
                        Config.AddPath();
                        break;

                    default:
                        Field.UpdateWindow();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Наберите команду - lsc что бы увидить список доступных команд");
                        Field.AddPathForInfoNoCommand(Program.path);
                        break;
                }
            }
        }



        public static string DefiningCommands(string strUser)
        {
            for (int i = 0; i < command.Count; i++)
            {
                if (strUser.IndexOf(command[i]) == 0)
                {
                    return command[i];
                }
            }
            return null;
        }
        //обновление пути папки в которой сейчас находится пользователь
        public static void GoToDirectory(string pathUser, string command)
        {

            path = null;
            for (int i = command.Length; i < pathUser.Length; i++)
            {
                path += pathUser[i];
            }
        }
    }
}
