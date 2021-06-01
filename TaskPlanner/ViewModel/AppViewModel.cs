using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TaskPlanner.Model;

namespace TaskPlanner.ViewModel
{
    public class AppViewModel
    {
        public bool DarkTheme { get; set; }
        public bool WindowBackground { get; set; }
        public ObservableCollection<ProjectInfo> ProjectInfoList { get; }

        public AppViewModel()
        {
            DarkTheme = true;
            ProjectInfoList = new ObservableCollection<ProjectInfo>();
        }

        public void ProcessProperty(string property, string value)
        {
            switch (property)
            {
                case "project":
                    Guid projectID = Guid.Parse(value);
                    break;
            }
        }

        public void Load(string projectInfoPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProjectInfo));
            ProjectInfo info = (ProjectInfo) serializer.Deserialize(File.OpenRead(projectInfoPath));
        }
    }
}
