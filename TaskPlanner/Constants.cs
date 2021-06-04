using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner
{
    public static class Constants
    {
        public static readonly string AppDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TaskPlanner");

        public static readonly string ProjectDataDir = Path.Combine(AppDir, "data");

        public static readonly string OptionsFile = Path.Combine(AppDir, "options.txt");
    }
}
