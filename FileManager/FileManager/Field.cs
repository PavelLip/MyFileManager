using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FileManager
{
    [Serializable]
    class Field
    {
        // Разделительный символ и минимальные размеры окна консоли
        private static char symDelimiter = Program.symDelimiter;
        private static int minHeightWinConsole = Program.minHeightWinConsole;
        private static int minWidthWinConsole = Program.minWidthWinConsole;

        public static void UpdateWindow()
        {
            Console.Clear();
            Field.FieldConsole();
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
        public static void AddPathForInfo(string path)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(Select.PathName(path));
            Console.SetCursorPosition(21, Console.WindowHeight - 3);
            Console.WriteLine("Ожидание команды");
        }
        public static void AddPathForInfo(string path, string fileName)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(Select.PathName(path));
            Console.SetCursorPosition(21, Console.WindowHeight - 3);
            Console.WriteLine($"Выбрана дирриктория {fileName}");
            Console.SetCursorPosition(65, Console.WindowHeight - 3);
            Console.WriteLine($"Дата создания {File.GetCreationTime(path)}");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);

        }
        public static void AddPathForInfoFile(string path, string fileName)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(Select.PathName(path));
            Console.SetCursorPosition(21, Console.WindowHeight - 3);
            Console.WriteLine($"Выбран файл {fileName}");
            Console.SetCursorPosition(65, Console.WindowHeight - 3);
            Console.WriteLine($"Дата создания {File.GetCreationTime(path)}");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
        public static void NoFolder()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(Program.path);
            Console.SetCursorPosition(21, Console.WindowHeight - 3);
            Console.WriteLine("Такой папки не существует");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
        public static void NoFolderAndFile()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(Program.path);
            Console.SetCursorPosition(21, Console.WindowHeight - 3);
            Console.WriteLine("Такой папки/файла не существует");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
        public static void AddPathForInfoNoCommand(string path)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(Select.PathName(path));
            Console.SetCursorPosition(21, Console.WindowHeight - 3);
            Console.WriteLine("Введена неверная команда");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
        public static void AddPathForInfo(string path, int pageStart, int pageFinish)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(path);
            Console.SetCursorPosition(21, Console.WindowHeight - 3);
            Console.WriteLine($"Страница {pageStart} из {pageFinish}, для выхода нажмите ESC");
        }
        public static void FieldConsole()
        {
            int heightWinConsole, widthWinConsole;
            //Проверка размеров экрана, если они изменилтсь то, перерисовка полей относительно нового размера консоли.
            (heightWinConsole, widthWinConsole) = UpdateSizeWinConsole();
            //Отрисовка разделительных полей
            Console.SetWindowSize(widthWinConsole, heightWinConsole);
            Console.SetBufferSize(widthWinConsole, heightWinConsole);
            Program.numberOnFilesPerPage = Console.WindowHeight - 5;

            AddSymDelimiter(heightWinConsole, widthWinConsole, symDelimiter, 1);
            AddSymDelimiter(heightWinConsole, widthWinConsole, symDelimiter, 4);
        }
        private static (int, int) UpdateSizeWinConsole()
        {
            int heightWinConsole = Console.WindowHeight;
            int widthWinConsole = Console.WindowWidth;

            if (heightWinConsole > minHeightWinConsole)
            {
                heightWinConsole = Console.WindowHeight;
            }
            else
            {
                heightWinConsole = minHeightWinConsole;
            }

            if (widthWinConsole > minWidthWinConsole)
            {
                widthWinConsole = Console.WindowWidth;
            }
            else
            {
                widthWinConsole = minWidthWinConsole;
            }
            return (heightWinConsole, widthWinConsole);
        }

        private static void AddSymDelimiter(int heigh, int width, char sym, int pozitionDown)
        {
            for (int i = 0; i < width; i++)
            {
                //Console.SetCursorPosition(i,Convert.ToInt32(widthWinConsole * 0.88)-1);
                //Console.SetCursorPosition(i, Convert.ToInt32(widthWinConsole * 0.96)-1);
                Console.SetCursorPosition(i, heigh - pozitionDown);
                Console.Write(sym);
            }
        }

    }
}