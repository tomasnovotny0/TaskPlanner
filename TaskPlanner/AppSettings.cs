using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace TaskPlanner
{
    public class AppSettings
    {
        public string LastProjectPath { get; set; }
        public bool OpenLatestProjectOnLaunch { get; set; }
        public bool DarkTheme { get; set; }

        private readonly Regex propertyRegex = new Regex("[a-zA-z0-9_]+=[a-zA-Z0-9_/\\.]+");

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(Constants.OptionsFile))
            {
                SaveProperty(writer, "openLatestOnLaunch", OpenLatestProjectOnLaunch);
                SaveProperty(writer, "darkTheme", DarkTheme);
                SaveProperty(writer, "project", LastProjectPath);
            }
        }

        public void Load()
        {
            string options = Constants.OptionsFile;
            if (!File.Exists(options))
            {
                FileStream fileStream = File.Create(options);
                fileStream.Close();
                return; // no reason to try reading empty file
            }
            using (StreamReader reader = new StreamReader(Constants.OptionsFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (propertyRegex.IsMatch(line))
                    {
                        string[] components = line.Split('=');
                        try
                        {
                            LoadProperty(components[0], components[1]);
                        }
                        catch (Exception)
                        {
                            ; // ignore invalid property
                        }
                    }
                }
            }
        }

        private void SaveProperty(StreamWriter writer, string propertyKey, object value)
        {
            if (value != null)
                writer.WriteLine($"{propertyKey}={value}");
        }

        private void LoadProperty(string propertyKey, string propertyValue)
        {
            switch (propertyKey)
            {
                case "project":
                    LastProjectPath = propertyValue ?? "";
                    break;
                case "darkTheme":
                    if (!bool.TryParse(propertyValue, out bool dark))
                    {
                        DarkTheme = false;
                    }
                    DarkTheme = dark;
                    break;
                case "openLatestOnLaunch":
                    if (!bool.TryParse(propertyValue, out bool openLatest))
                    {
                        OpenLatestProjectOnLaunch = false;
                    }
                    OpenLatestProjectOnLaunch = openLatest;
                    break;
            }
        }
    }
}
