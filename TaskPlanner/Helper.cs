using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner
{
    public static class Helper
    {
        public static string GetFileExtension(string filename)
        {
            int index = -1;
            for (int i = 0; i < filename.Length; i++)
            {
                if (filename[i] == '.')
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)
                return "";
            if (filename.Length <= index + 1)
                throw new ArgumentException("Invalid filename - file cannot end with '.' character");
            return filename.Substring(index + 1);
        }
    }
}
