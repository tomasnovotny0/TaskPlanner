using System;
using System.IO;
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
            ProjectInfo info = ProjectSelector.SelectedItem as ProjectInfo;
            SelectProjectViewModel viewModel = DataContext as SelectProjectViewModel;

            File.Delete(info.Path);
            File.Delete(Path.Combine(Constants.ProjectDataDir, info.ProjectName + ".xml"));
            viewModel.Projects.Remove(info);
        }

        private void MenuExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectSelector.SelectedItem is ProjectInfo info)
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
            }
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectSelector.SelectedItem is ProjectInfo info)
            {
                Open(info);
            }
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
    }
}
