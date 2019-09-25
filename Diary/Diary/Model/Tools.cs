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
        private const string taskElement = "Task";
        private const string descriptionElement = "Description";
        private const string creationDateElement = "CreationDate";
        private const string realizationDateElement = "RealizationDate";
        private const string isAccomplishedElement = "IsAccomplished";
        private const string taskPriorityElement = "TaskPriority";


        public static void Save(string filePath, TasksModel tasks)
        {
            try
            {
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Date of saving: " + DateTime.Now.ToString(formatProvider)),
                    new XElement("Tasks",
                        from SingleTaskModel task in tasks
                        select new XElement(taskElement,
                                new XElement(descriptionElement, task.Descripton),
                                new XElement(creationDateElement, task.CreationDate),
                                new XElement(realizationDateElement, task.RealizationDate.ToString(formatProvider)),
                                new XElement(taskPriorityElement, (byte)task.Priority),
                                new XElement(isAccomplishedElement, task.IsAccomplished))
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
                            task.Element(descriptionElement).Value,
                            DateTime.Parse(task.Element(creationDateElement).Value, formatProvider),
                            DateTime.Parse(task.Element(realizationDateElement).Value, formatProvider),
                            (TaskPriority)byte.Parse(task.Element(taskPriorityElement).Value, formatProvider),
                            bool.Parse(task.Element(isAccomplishedElement).Value));

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
