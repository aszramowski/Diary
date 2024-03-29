﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diary.Model
{
    public enum TaskPriority : byte { LessImportant, Important, Critical };
    public class SingleTaskModel
    {
        #region Properties

        public string Descripton { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime RealizationDate { get; set; }
        public TaskPriority Priority { get; set; }
        public bool IsAccomplished { get; set; }

        #endregion

        public SingleTaskModel(string description, DateTime creationDate, DateTime realizationDate, TaskPriority priority, bool isAccompished = false)
        {
            Descripton = description;
            CreationDate = creationDate;
            RealizationDate = realizationDate;
            Priority = priority;
            IsAccomplished = isAccompished;
        }

        public static string PriorityDescription(TaskPriority priority)
        {
            switch(priority)
            {
                case TaskPriority.LessImportant:
                    return "Less important";
                case TaskPriority.Important:
                    return "Important";
                case TaskPriority.Critical:
                    return "Critical";
                default:
                    throw new Exception("Unknown task priority");
            }
        }
    }
}
