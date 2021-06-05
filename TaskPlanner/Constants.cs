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
        public static string AppDir { get => _appDir; }
        public static string ProjectDataDir { get => _projectDataDir; }
        public static string OptionsFile { get => _optionsFile; }
        public static string ProjectDirectory { get => _projectDir; }

        private static string _appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TaskPlanner");
        private static string _projectDataDir = Path.Combine(AppDir, "data");
        private static string _optionsFile = Path.Combine(AppDir, "options.txt");
        private static string _projectDir = Path.Combine(AppDir, "projects");
    }
}
