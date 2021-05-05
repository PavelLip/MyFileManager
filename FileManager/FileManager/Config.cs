using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class Config
    {
        public static (string, char, int, int) ReadAllSettings()
        {
            string[] allSettings = File.ReadAllLines("Settings.cfg");
            string path = allSettings[0];
            char symDelimiter = Convert.ToChar(allSettings[1]);
            int minHeightWinConsole = Convert.ToInt32(allSettings[2]);
            int minWidthWinConsole = Convert.ToInt32(allSettings[3]);
            return (path, symDelimiter, minHeightWinConsole, minWidthWinConsole);
        }

        public static void AddPath()
        {
            string[] str = new string[4];
            str[0] = Program.path;
            str[1] = Convert.ToString(Program.symDelimiter);
            str[2] = Convert.ToString(Program.minHeightWinConsole);
            str[3] = Convert.ToString(Program.minWidthWinConsole);

            File.WriteAllLines("Settings.cfg", str);
        }
    }
}
