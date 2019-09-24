using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diary.Model
{
    public class TasksModel : IEnumerable<SingleTaskModel>
    {
        private List<SingleTaskModel> tasksList = new List<SingleTaskModel>();

        public void AddTask(SingleTaskModel task)
        {
            tasksList.Add(task);
        }
        public bool RemoveTask(SingleTaskModel task)
        {
            return tasksList.Remove(task);
        }
        public int NumberOfTasks
        {
            get { return tasksList.Count(); }
        }
        public SingleTaskModel this[int index]
        {
            get { return tasksList[index]; }
        }

        public IEnumerator<SingleTaskModel> GetEnumerator()
        {
            return tasksList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }
    }
}
