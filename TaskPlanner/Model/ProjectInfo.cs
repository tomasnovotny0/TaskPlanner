using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Model
{
    public sealed class ProjectInfo
    {
        public string ProjectName { get; set; }
        public string ContentPath { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastOpened { get; set; }
    }
}
