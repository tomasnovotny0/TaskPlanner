using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.Model
{
    public interface IProjectTemplate
    {
        string TemplateName { get; }
    }

    public sealed class EmptyTemplate : IProjectTemplate
    {
        public string TemplateName { get => "None"; }
    }

    public static class ProjectTemplates
    {
        public static IList<IProjectTemplate> Templates { get; }

        static ProjectTemplates()
        {
            Templates = new List<IProjectTemplate>();

            RegisterTemplate(new EmptyTemplate());
        }

        public static void RegisterTemplate(IProjectTemplate template)
        {
            Templates.Add(template);
        }
    }
}
