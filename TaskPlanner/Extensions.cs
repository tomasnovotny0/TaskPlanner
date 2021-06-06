using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner
{
    public static class Extensions
    {
        public static void Sort<Source, Key>(this ObservableCollection<Source> observable, Func<ObservableCollection<Source>, IOrderedEnumerable<Source>> sorter)
        {
            List<Source> list = sorter.Invoke(observable).ToList();
            observable.Clear();
            foreach (Source source in list)
            {
                observable.Add(source);
            }
        }
    }
}
