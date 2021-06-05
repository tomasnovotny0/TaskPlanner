using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using TaskPlanner.Model;

namespace TaskPlanner.ViewModel
{
    public class SelectProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ProjectInfo> Projects { get; }
        public Visibility CreateProjectHint { get => Projects.Count == 0 ? Visibility.Visible : Visibility.Hidden; }

        public SelectProjectViewModel()
        {
            Projects = new ObservableCollection<ProjectInfo>();

            LoadProjects();
        }

        private void LoadProjects()
        {
            string[] projectFiles = Directory.GetFiles(Constants.ProjectDataDir);
            foreach (string filePath in projectFiles)
            {
                string fileName = Path.GetFileName(filePath);
                string extension = Helper.GetFileExtension(fileName);
                if (extension != "xml")
                    continue;
                LoadProjectInfo(filePath);
            }
            Projects.Sort(project => project.LastOpened);
        }

        private void LoadProjectInfo(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProjectInfo));
            ProjectInfo info = (ProjectInfo) serializer.Deserialize(File.OpenRead(path));

            Projects.Add(info);
        }

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
