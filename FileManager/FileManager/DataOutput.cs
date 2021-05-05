using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class DataOutput
    {
        public static string DefaultUserPath()
        {
            string path = "C\u005c";
            return path;
        }

        // все файлы и папки в одном массиве
        public static string[] Output(string path)
        {
            string[] directory = Directory.GetDirectories(@path);
            string[] files = Directory.GetFiles(@path);
            int count = directory.Length + files.Length;
            string[] str = new string[count];
            for (int i = 0; i < directory.Length; i++)
            {
                str[i] = directory[i].Substring(path.Length, directory[i].Length - path.Length); ;
            }
            for (int i = 0; i < files.Length; i++)
            {
                str[i + directory.Length] = files[i].Substring(path.Length, files[i].Length - path.Length); ;
            }
            return str;
        }
        //постраничный вывод данных выбранной папки
        public static void ShowFiles(string path)
        {
            Field.AddPathForInfo(path);
            Console.SetCursorPosition(0, 0);
            string[] data = DataOutput.Output(path);
            if (data.Length <= Program.numberOnFilesPerPage)
            {
                NextOrPreviousPage(data, 1);
            }
            else
            {
                NextOrPreviousPage(data, 1);
                Field.AddPathForInfo(path, 1, CountPage(data));
                WorkFromDir(path, data);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
        //выбор папки и постраничный вывод на экран
        private static void WorkFromDir(string path, string[] data)
        {
            bool flag = true;
            Program.publicPageDir = 1;
            int countPage = CountPage(data);
            while (flag)
            {
                //добавить проверку что введено клавиша или текст
                var x = Console.ReadKey();
                if (x.Key == ConsoleKey.RightArrow)
                {
                    if (Program.publicPageDir < countPage)
                    {
                        Program.publicPageDir += 1;
                        AddDataConsole(Program.publicPageDir);
                    }
                    else
                    {
                        AddDataConsole(Program.publicPageDir);
                    }
                }
                else if (x.Key == ConsoleKey.LeftArrow)
                {
                    if (Program.publicPageDir > 1)
                    {
                        Program.publicPageDir -= 1;
                        AddDataConsole(Program.publicPageDir);
                    }
                    else
                    {
                        AddDataConsole(Program.publicPageDir);
                    }
                }
                else if (x.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Field.UpdateWindow();
                    NextOrPreviousPage(data, Program.publicPageDir);
                    Field.AddPathForInfo(path);
                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    flag = false;
                }
            }
            void AddDataConsole(int page2)
            {
                Console.Clear();
                Field.UpdateWindow();
                NextOrPreviousPage(data, page2);
                Field.AddPathForInfo(path, page2, CountPage(data));
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
            }
        }
        //кол-во страниц в выбранной папке
        private static int CountPage(string[] str)
        {
            int countDirAndFiles = str.Length;
            int countPage;
            if (countDirAndFiles / Program.numberOnFilesPerPage == 0)
            {
                countPage = countDirAndFiles / Program.numberOnFilesPerPage;
            }
            else
            {
                countPage = countDirAndFiles / Program.numberOnFilesPerPage + 1;
            }
            return countPage;
        }
        //вывод данных введенной страницы
        public static void NextOrPreviousPage(string[] str, int page)
        {
            Console.SetCursorPosition(0, 0);
            int countPage = CountPage(str);
            if (str.Length > Program.numberOnFilesPerPage)
            {
                int finishPosition = Program.numberOnFilesPerPage * page;
                if (str.Length - Program.numberOnFilesPerPage * page < 0)
                {
                    finishPosition = str.Length;
                }
                for (int i = Program.numberOnFilesPerPage * (page - 1); i < finishPosition; i++)
                {
                    Console.WriteLine(str[i]);
                }
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    Console.WriteLine(str[i]);
                }
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 1);

        }



    }
}
