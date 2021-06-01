using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Model
{
    public sealed class ProjectInfo
    {
        public Guid ProjectID { get; }
        public DateTime DateCreated { get; set; }
        public DateTime LastOpened { get; set; }
        public string ProjectName { get; private set; }
        public string ProjectDescription { get; private set; }
        public string ProjectDataPath { get; private set; }

        public void Rename(string newProjectName)
        {
            ProjectName = newProjectName;
        }
    }
}
