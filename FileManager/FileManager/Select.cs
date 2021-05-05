using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class Select
    {
        public static int lengthPuth = 19;
        public static string FileName(string command, string commandUser)
        {
            if (commandUser.Length < command.Length)
            {
                return PathName(command);
            }
            return commandUser.Substring(command.Length);
        }
        public static string DirName(string command, string commandUser)
        {
            if (commandUser.Length < command.Length)
            {
                return PathName(command);
            }
            string outputCommand = commandUser.Substring(command.Length);
            return outputCommand;
        }
        public static string FileNameFull(string command, string commandUser)
        {
            string outputCommand = commandUser.Substring(command.Length);
            return outputCommand;

        }
        public static string PathName(string path)
        {
            string outputCommand;
            outputCommand = SecondTirName(path);
            outputCommand = CountSymPath(outputCommand);
            return outputCommand;
        }
        private static string CountSymPath(string puthFile)
        {
            string newName;
            int countSymPath = (lengthPuth - 3) / 2;
            if (puthFile.Length > lengthPuth)
            {
                newName = puthFile.Substring(0, countSymPath);
                newName += "...";
                newName += puthFile.Substring(puthFile.Length - countSymPath, countSymPath);
                //newName += puthFile.Substring(9, 14);
                return newName;
            }
            return puthFile;
        }
        private static string SecondTirName(string puthFile)
        {
            string newName;
            int countDelimeter = 0;
            for (int i = puthFile.Length - 1; i != 0; i--)
            {
                if (puthFile[i] == '\u005c')
                {
                    countDelimeter += 1;
                }
                if (countDelimeter == 2)
                {
                    newName = puthFile.Substring(i);
                    return newName;
                }
            }
            return puthFile;
        }
        public static string PathFile(string puthFile)
        {
            string newName;
            int countDelimeter = 0;
            for (int i = puthFile.Length - 1; i != 0; i--)
            {
                if (puthFile[i] == '\u005c')
                {
                    countDelimeter += 1;
                }
                if (countDelimeter == 1)
                {
                    newName = puthFile.Substring(i);
                    newName = puthFile.Substring(0, puthFile.Length - newName.Length);
                    return newName;
                }
            }
            return puthFile;
        }
    }

}
