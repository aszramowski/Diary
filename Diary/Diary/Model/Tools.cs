using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Diary.Model
{
    public static class Tools
    {
        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        public static void Save(string filePath, TasksModel tasks)
        {
            try
            {
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Date of saving: " + DateTime.Now.ToString(formatProvider)),
                    new XElement("Tasks",
                        from SingleTaskModel task in tasks
                        select new XElement("Task",
                                new XElement("Description", task.Descripton),
                                new XElement("CreationDate", task.CreationDate),
                                new XElement("RealizationDate", task.RealizationDate.ToString(formatProvider)),
                                new XElement("IsAccomplished", task.IsAccomplished))
                    )
                );
                xml.Save(filePath);
            }
            catch (Exception exc)
            {

                throw new Exception("Error: saving tasks to XML file", exc);
            }
        }
        public static TasksModel Read(string filePath)
        {
            try
            {
                XDocument xml = XDocument.Load(filePath);
                IEnumerable<SingleTaskModel> data =
                    from task in xml.Root.Descendants("Task")
                    select new SingleTaskModel(
                        task.Element("Description").Value,
                        DateTime.Parse(task.Element("CreationDate").Value, formatProvider),
                        DateTime.Parse(task.Element("RealizationDate").Value, formatProvider),
                        (TaskPriority)byte.Parse(task.Element("Priority").Value, formatProvider),
                        bool.Parse(task.Element("IsAccomplished").Value));

                TasksModel tasks = new TasksModel();
                foreach (SingleTaskModel task in data) tasks.AddTask(task);

                return tasks;
            }
            catch (Exception exc)
            {

                throw new Exception("Error: loading tasks from XML file", exc);
            }
        }
    }
}
