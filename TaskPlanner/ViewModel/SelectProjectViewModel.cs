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

        public void CreateProject(string projectName, string projectDescription, string projectFolder, bool open)
        {
            Helper.CreateDirectoryIfAbsent(projectFolder);
            ProjectInfo project = new ProjectInfo
            {
                ProjectName = ValidateProjectName(projectName),
                ProjectDescription = projectDescription,
                ProjectDir = projectFolder,
                Created = DateTime.Now,
                LastOpened = DateTime.Now
            };
            Projects.Add(project);
            SortProjects();
            project.SaveXML();
            
            if (open)
            {
                // TODO open
            }
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
            SortProjects();
        }

        private void LoadProjectInfo(string path)
        {
            ProjectInfo info = new ProjectInfo();
            try
            {
                info.LoadXML(path);
                Projects.Add(info);
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't load project " + path);
            } 
        }

        private void SortProjects()
        {
            Projects.Sort(info => info.LastOpened);
        }

        private string ValidateProjectName(string name)
        {
            foreach (ProjectInfo info in Projects)
            {
                if (info.ProjectName == name || !ProjectInfo.NAME_VALIDATOR.IsMatch(name))
                {
                    throw new ArgumentException("Duplicate project name");
                }
            }
            return name;
        }

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
