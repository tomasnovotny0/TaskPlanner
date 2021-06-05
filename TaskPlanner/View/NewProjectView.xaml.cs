using Ookii.Dialogs.Wpf;
using System;
using System.Windows;
using TaskPlanner.ViewModel;

namespace TaskPlanner.View
{
    public partial class NewProjectView : Window
    {
        private readonly SelectProjectViewModel vm;

        public NewProjectView(SelectProjectViewModel _vm)
        {
            InitializeComponent();
            vm = _vm;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string projectName = ProjectName.Text;
            string projectDescription = ProjectDescription.Text;
            string projectDirectory = ProjectPath.Text;
            try
            {
                vm.CreateProject(projectName, projectDescription, projectDirectory, OpenProject.IsEnabled);
                Close();
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.Desktop
            };
            if (dialog.ShowDialog() == true)
            {
                ProjectPath.Text = dialog.SelectedPath;
            }
        }
    }
}