using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TaskPlanner.ViewModel;

namespace TaskPlanner.View
{
    public partial class AppView : Window
    {
        public AppView()
        {
            InitializeComponent();
            CreateOrReadAppData();
        }

        private void CreateOrReadAppData()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TaskPlanner");
            CreateDirectoryIfAbsent(path);
            // TODO load projects
            ProcessOptionsFile(path);
        }

        private void CreateDirectoryIfAbsent(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void ProcessOptionsFile(string dir)
        {
            string optionsPath = Path.Combine(dir, "options.txt");
            if (!File.Exists(optionsPath))
            {
                File.Create(optionsPath).Close();
            }
            using (StreamReader reader = new StreamReader(optionsPath))
            {
                AppViewModel avm = DataContext as AppViewModel;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        string[] components = line.Split('=');
                        if (components.Length != 2)
                            continue;
                        avm.ProcessProperty(components[0], components[1]);
                    }
                }
            }
        }

        private void WriteProperty(FileStream stream, string property, string value)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
