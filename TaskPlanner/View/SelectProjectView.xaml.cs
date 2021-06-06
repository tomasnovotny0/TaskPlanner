using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using TaskPlanner.Model;
using TaskPlanner.ViewModel;

namespace TaskPlanner.View
{
    /// <summary>
    /// Interaction logic for SelectProjectView.xaml
    /// </summary>
    public partial class SelectProjectView : Window
    {
        public SelectProjectView()
        {
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DisplayNewProjectDialog();
        }

        private void ButtonNewProject_Click(object sender, RoutedEventArgs e)
        {
            DisplayNewProjectDialog();
        }

        private void DisplayNewProjectDialog()
        {
            NewProjectView view = new NewProjectView(DataContext as SelectProjectViewModel);
            view.ShowDialog();
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            SelectProjectViewModel viewModel = DataContext as SelectProjectViewModel;
            RunActionOnSelectedProject(info =>
            {
                File.Delete(info.Path);
                File.Delete(Path.Combine(Constants.ProjectDataDir, info.ProjectName + ".xml"));
                viewModel.Projects.Remove(info);
            });
        }

        private void MenuExport_Click(object sender, RoutedEventArgs e)
        {
            RunActionOnSelectedProject(info =>
            {

            });
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            RunActionOnSelectedProject(info =>
            {
                EditProjectView view = new EditProjectView(info);
                string oldPath = info.Path;
                if (view.ShowDialog() == true)
                {
                    info.ProjectName = view.ProjectName.Text;
                    info.ProjectDir = view.ProjectPath.Text;
                    info.ProjectDescription = view.ProjectDescription.Text;
                }
                MoveExistingFiles(oldPath, info.Path);
            });
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            RunActionOnSelectedProject(Open);
        }

        private void ProjectSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext is ProjectInfo info)
            {
                Open(info);
            }
        }

        private void Open(ProjectInfo info)
        {
            App.Settings.LastProjectPath = Path.Combine(Constants.ProjectDataDir, info.ProjectName + ".xml");
            info.LastOpened = DateTime.Now;
            info.SaveXML();
            ProjectView projectView = new ProjectView();
            projectView.Show();
            Close();
        }

        private void MoveExistingFiles(string oldPath, string newPath)
        {
            if (oldPath != newPath)
            {
                if (File.Exists(newPath))
                    File.Delete(newPath);
                File.Move(oldPath, newPath);
            }
        }

        private void RunActionOnSelectedProject(Action<ProjectInfo> action)
        {
            if (ProjectSelector.SelectedItem is ProjectInfo info)
            {
                action.Invoke(info);
            }
            else
            {
                MessageBox.Show("Project is not selected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
