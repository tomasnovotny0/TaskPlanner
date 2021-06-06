using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace TaskPlanner.Model
{
    public sealed class ProjectInfo
    {
        public static readonly Regex NAME_VALIDATOR = new Regex("[a-zA-Z0-9\\s-_]+");
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectDir { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastOpened { get; set; }
        public string CreatedFormatted { get => Created.ToString(CultureInfo.CurrentCulture); }
        public string OpenedFormatted { get => LastOpened.ToString(CultureInfo.CurrentCulture); }
        public string Path { get => System.IO.Path.Combine(Constants.ProjectDataDir, ProjectName + ".xml"); }

        public void SaveXML()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(declaration);
            XmlElement root = doc.CreateElement("project");
            root.SetAttribute("name", ProjectName);
            root.SetAttribute("description", ProjectDescription);
            root.SetAttribute("path", ProjectDir);
            root.SetAttribute("created", Created.ToString(CultureInfo.InvariantCulture));
            root.SetAttribute("opened", LastOpened.ToString(CultureInfo.InvariantCulture));
            doc.AppendChild(root);
            doc.Save(Path);
        }

        public void LoadXML(string path)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(File.ReadAllText(path));
            XmlElement element = document.DocumentElement;
            string name = element.GetAttribute("name");
            string description = element.GetAttribute("description");
            string dir = element.GetAttribute("path");
            DateTime created = DateTime.Parse(element.GetAttribute("created"), CultureInfo.InvariantCulture);
            DateTime opened = DateTime.Parse(element.GetAttribute("opened"), CultureInfo.InvariantCulture);

            if (!NAME_VALIDATOR.IsMatch(name))
                throw new ArgumentException("Invalid project name");
            if (dir.Length == 0)
                throw new ArgumentException("Invalid project path");
            Helper.CreateDirectoryIfAbsent(dir);

            ProjectName = name;
            ProjectDescription = description;
            ProjectDir = dir;
            Created = created;
            LastOpened = opened;
        }

        public Project LoadProject()
        {
            Project project = new Project(Path);
            project.LoadProject();
            return project;
        }
    }
}
