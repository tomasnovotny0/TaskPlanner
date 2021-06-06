using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace TaskPlanner.Model
{
    public sealed class Project : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string DataPath { get; }
        public ObservableCollection<TaskCard> Cards { get; }

        public Project(string dataPath)
        {
            DataPath = dataPath;
            Cards = new ObservableCollection<TaskCard>();
        }

        public void AddCard(string cardName)
        {
            TaskCard card = new TaskCard();
            Cards.Add(card);
        }

        public void RemoveCard(TaskCard card)
        {
            Cards.Remove(card);
        }

        public void SaveProject()
        {
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlElement project = document.CreateElement("project");
            XmlElement cards = document.CreateElement("cards");
            foreach (TaskCard card in Cards)
            {
                XmlElement element = document.CreateElement("card");
                card.SaveTo(element);
                cards.AppendChild(element);
            }
            project.AppendChild(cards);
            document.AppendChild(project);
            document.Save(DataPath);
        }

        public void LoadProject()
        {
            string path = DataPath;
            if (!File.Exists(path))
                throw new ArgumentException("Invalid project path");
            XmlDocument document = new XmlDocument();
            document.LoadXml(File.ReadAllText(path));
            XmlElement project = document.DocumentElement;
            Cards.Clear();
            foreach (XmlNode node in project.ChildNodes)
            {
                if (node.Name == "cards")
                {
                    foreach (XmlNode cardNode in node.ChildNodes)
                    {
                        if (node.Name == "card")
                        {
                            TaskCard card = new TaskCard();
                            try
                            {
                                card.LoadFrom((XmlElement)cardNode);
                                Cards.Add(card);
                            }
                            catch (Exception)
                            {
                                // ignore invalid card
                            }
                        }
                    }
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
