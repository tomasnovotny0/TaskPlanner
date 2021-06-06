using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskPlanner.Model;
using TaskPlanner.View;

namespace TaskPlanner
{
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

            if (Settings.OpenLatestProjectOnLaunch)
            {
                try
                {
                    if (Settings.LastProjectPath == null || !File.Exists(Settings.LastProjectPath))
                        throw new ArgumentException("Invalid project path");
                    ProjectInfo info = new ProjectInfo();
                    info.LoadXML(Settings.LastProjectPath); // test load xml file to make sure it is valid
                    info.LastOpened = DateTime.Now;
                    info.SaveXML();
                    StartupUri = new Uri("View/ProjectView.xaml", UriKind.Relative);
                }
                catch (Exception)
                {

                }
            }

            if (StartupUri == null)
                StartupUri = new Uri("View/SelectProjectView.xaml", UriKind.Relative);
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
