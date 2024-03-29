﻿using Diary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Diary.Model;
using System.Windows.Input;

namespace Diary.ViewModel
{
    public class TasksViewModel : BaseViewModel
    {
        private const string filePath = "tasks.xml";

        // storing collections
        private TasksModel tasksModel;
        private ICommand saveTasksCommand;
        private ICommand removeTasksCommand;
        private ICommand addTasksCommand;

        public ObservableCollection<SingleTaskViewModel> TasksList { get; } = new ObservableCollection<SingleTaskViewModel>();

        private void copyTasks()
        {
            TasksList.CollectionChanged -= tasksModelSynchronization;
            TasksList.Clear();

            foreach (SingleTaskModel task in tasksModel)
                TasksList.Add(new SingleTaskViewModel(task));

            TasksList.CollectionChanged += tasksModelSynchronization;
        }

        public TasksViewModel()
        {
            if (System.IO.File.Exists(filePath))
                tasksModel = Tools.Read(filePath);
            else
                tasksModel = new TasksModel();

            // tests - begining
            //tasksModel.AddTask(new SingleTaskModel("First", DateTime.Now, DateTime.Now.AddDays(2), TaskPriority.Important));
            //tasksModel.AddTask(new SingleTaskModel("Second", DateTime.Now, DateTime.Now.AddDays(2), TaskPriority.Important));
            //tasksModel.AddTask(new SingleTaskModel("Third", DateTime.Now, DateTime.Now.AddDays(3), TaskPriority.LessImportant));
            //tasksModel.AddTask(new SingleTaskModel("Fourth", DateTime.Now, DateTime.Now.AddDays(4), TaskPriority.Critical));
            //tasksModel.AddTask(new SingleTaskModel("Fifth", DateTime.Now, DateTime.Now.AddDays(5), TaskPriority.Important));
            //tasksModel.AddTask(new SingleTaskModel("Sixth", DateTime.Now, DateTime.Now.AddDays(-6), TaskPriority.Important));

            copyTasks();
        }

        private void tasksModelSynchronization(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SingleTaskViewModel newTaskViewModel = (SingleTaskViewModel)e.NewItems[0];
                    if (newTaskViewModel != null)
                        tasksModel.AddTask(newTaskViewModel.GetModel());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    SingleTaskViewModel removedTask = (SingleTaskViewModel)e.OldItems[0];
                    if (removedTask != null)
                        tasksModel.RemoveTask(removedTask.GetModel());
                    break;
            }
        }
        public ICommand SaveTasksCommand
        {
            get
            {
                if (saveTasksCommand == null)
                    saveTasksCommand = new RelayCommand(
                        o =>
                        {
                            Tools.Save(filePath, tasksModel);
                        });
                return saveTasksCommand;
            }
        }
        public ICommand AddTasksCommand
        {
            get
            {
                if (addTasksCommand == null)
                    addTasksCommand = new RelayCommand(
                        o =>
                        {
                            var result = App.dialogService.ShowDialog(new YesNoDialogViewModel("Adding task", $"Do you want to add a new task?"));
                            if (result == true)
                            {
                                SingleTaskViewModel taskViewModel = o as SingleTaskViewModel;
                                if (taskViewModel != null) TasksList.Add(taskViewModel);
                            }
                        },
                        o =>
                        {
                            return (o as SingleTaskViewModel) != null;
                        });
                return addTasksCommand;
            }
        }
        public ICommand RemoveTasksCommand
        {
            get
            {
                if (removeTasksCommand == null)
                    removeTasksCommand = new RelayCommand(
                        o =>
                        {
                            var result = App.dialogService.ShowDialog(new YesNoDialogViewModel("Removing task", $"Do you want to remove this task?"));
                            if (result == true)
                            {
                                int taksIndex = (int)o;
                                SingleTaskViewModel taskViewModel = TasksList[taksIndex];
                                TasksList.Remove(taskViewModel); 
                            }
                        },
                        o =>
                        {
                            if (o == null) return false;
                            int taskIndex = (int)o;
                            return taskIndex >= 0;
                        });
                return removeTasksCommand;
            }
        }
    }
}
