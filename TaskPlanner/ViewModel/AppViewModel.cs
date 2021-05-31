using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.ViewModel
{
    public class AppViewModel
    {
        public bool DarkTheme { get; set; }
        public bool WindowBackground { get; set; }

        public AppViewModel()
        {
            DarkTheme = true;

        }

        private void Load()
        {

        }
    }
}
