using System;
using System.Windows;
using System.Windows.Input;

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
            // TODO create new project
        }
    }
}
