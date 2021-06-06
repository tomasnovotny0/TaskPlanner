using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskPlanner.Model;
using TaskPlanner.View;

namespace TaskPlanner.ViewModel
{
    public class ProjectViewModel
    {
        public ProjectInfo Info { get; }
        public Project Project { get; }

        public ProjectViewModel()
        {
            ProjectInfo info = new ProjectInfo();
            info.LoadXML(App.Settings.LastProjectPath);
            Info = info;
            Project = Info.LoadProject();
        }

        public void Save()
        {

        }
    }
}
