using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TaskPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppSettings Settings { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Settings = new AppSettings();
            CreateDirectoryIfAbsent(Constants.AppDir);
            CreateDirectoryIfAbsent(Constants.ProjectDataDir);
            CreateDirectoryIfAbsent(Constants.ProjectDirectory);
            Settings.Load();

            if (TryLoadLatestProject())
            {
                // TODO open project window
            }
            else
            {
                StartupUri = new Uri("View/SelectProjectView.xaml", UriKind.Relative);
            }
        }

        private bool TryLoadLatestProject()
        {
            // TODO implement
            return false;
        }

        private void CreateDirectoryIfAbsent(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.Save();
        }
    }
}
