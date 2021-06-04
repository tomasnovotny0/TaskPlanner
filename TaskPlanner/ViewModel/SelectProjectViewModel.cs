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

        }

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
