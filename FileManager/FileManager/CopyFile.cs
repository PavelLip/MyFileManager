using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class CopyFile
    {
        private static string[] lsc =
        {
            "copy",
            "delete",
            "exit"
        };
        public static void CopyDirOnFiles(string pathOld, string pathNew)
        {
            AllDir(pathOld, pathNew);
            DirectoryInfo dirInfo = new DirectoryInfo(pathOld);
            foreach (FileInfo file in dirInfo.GetFiles("*.*"))
            {
                File.Copy(file.FullName, pathNew + "\\" + file.Name, true);
            }
        }

        private static void AllDir(string path, string path2)
        {
            string[] dir = Directory.GetDirectories(path);
            //string[] files = Directory.GetFiles(path);
            string[] dir2 = null;
            for (int i = 0; i < dir.GetLength(0); i++)
            {

                Directory.CreateDirectory(newDir(dir[i], path2));
                dir2 = Directory.GetDirectories(dir[i]);
                string[] files = Directory.GetFiles(dir[i]);
                for (int j = 0; j < files.Length; j++)
                {
                    File.Copy(files[j], NewPathFile(path, path2, files[j]), true);
                }
                if (dir2.Any())
                {
                    AllDir(dir[i], newDir(dir[i], path2));
                }
            }
        }

        private static string NewPathFile(string oldPath, string newPath, string pathfile)
        {
            string newStr = newPath + "\\" + pathfile.Substring(oldPath.Length);
            return newStr;
        }

        public static void CopyOrDeleteDir()
        {
            bool flag = true;
            while (flag)
            {
                string commandUser = Console.ReadLine();
                string command = Program.DefiningCommands(commandUser);
                switch (command)
                {
                    case "delete":
                        Directory.Delete(Program.path, true);
                        Program.selectFileonDIr = Select.DirName("delete", commandUser);
                        Field.UpdateWindow();
                        Field.AddPathForInfo(Select.PathFile(Program.path) + "\\", Program.selectFileonDIr);
                        DataOutput.NextOrPreviousPage(DataOutput.Output(Select.PathFile(Program.path) + "\\"), Program.publicPageDir);
                        break;

                    case "copy":
                        Program.selectFileonDIr = Select.DirName("copy ", commandUser);
                        Directory.CreateDirectory(Program.selectFileonDIr);
                        CopyDirOnFiles(Program.path, Program.selectFileonDIr);
                        Field.UpdateWindow();
                        Field.AddPathForInfo(Select.PathFile(Program.path), Program.selectFileonDIr);
                        DataOutput.NextOrPreviousPage(DataOutput.Output(Program.path), Program.publicPageDir);
                        break;

                    case "exit":
                        Field.UpdateWindow();
                        Field.AddPathForInfo(Select.PathFile(Program.path), Program.selectFileonDIr);
                        DataOutput.NextOrPreviousPage(DataOutput.Output(Program.path), Program.publicPageDir);
                        flag = false;
                        break;

                    case "lsc":
                        Field.UpdateWindow();
                        Console.SetCursorPosition(0, 0);
                        for (int i = 0; i < lsc.Length; i++)
                        {
                            Console.WriteLine(lsc[i]);
                        }
                        Field.AddPathForInfoNoCommand(Program.path);
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


        public static void CopyOrDeleteFile()
        {
            bool flag = true;
            while (flag)
            {
                string commandUser = Console.ReadLine();
                string command = Program.DefiningCommands(commandUser);
                switch (command)
                {
                    case "delete":
                        Program.selectFileonDIr = Select.FileName("delete", commandUser);
                        File.Delete(Program.path);
                        Field.UpdateWindow();
                        Field.AddPathForInfo(Select.PathFile(Program.path), DeleteNameFile(Program.selectFileonDIr));
                        DataOutput.NextOrPreviousPage(DataOutput.Output(Select.PathFile(Program.path)), Program.publicPageDir);
                        break;

                    case "copy":
                        Program.selectFileonDIr = Select.FileName("copy ", commandUser);
                        File.Copy(Program.path, Program.selectFileonDIr);
                        Field.UpdateWindow();
                        Field.AddPathForInfo(Select.PathFile(Program.path), DeleteNameFile(Program.selectFileonDIr));
                        DataOutput.NextOrPreviousPage(DataOutput.Output(Select.PathFile(Program.path)), Program.publicPageDir);
                        break;

                    case "exit":
                        Field.UpdateWindow();
                        Field.AddPathForInfo(Select.PathFile(Program.path), DeleteNameFile(Program.selectFileonDIr));
                        DataOutput.NextOrPreviousPage(DataOutput.Output(Select.PathFile(Program.path)), Program.publicPageDir);
                        flag = false;
                        break;

                    default:
                        Field.UpdateWindow();
                        Field.AddPathForInfoNoCommand(Program.path);
                        break;
                }
            }
        }

        private static string DeleteNameFile(string path)
        {
            string newPath = null;
            for (int i = path.Length - 1; i > 0; i--)
            {
                if (path[i] == '\\')
                {
                    newPath = path.Substring(0, i);
                }
            }
            return newPath;
        }

        private static string newDir(string foleder1, string folder2)
        {
            for (int i = foleder1.Length - 1; i > 0; i--)
            {
                if (foleder1[i] == '\\')
                {
                    folder2 += foleder1.Substring(i, foleder1.Length - i);
                    return folder2;
                }
            }
            return folder2;
        }
    }
}
