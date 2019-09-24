using Diary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Diary.ViewModel
{
    public class SingleTaskViewModel : BaseViewModel
    {
        private SingleTaskModel singleTaskModel;
        private ICommand markAsAccomplished;
        private ICommand markAsNotAccomplished;

        #region Properties

        public string Description
        {
            get { return singleTaskModel.Descripton; }
        }
        public TaskPriority Priority
        {
            get { return singleTaskModel.Priority; }
        }
        public DateTime CreationDate
        {
            get { return singleTaskModel.CreationDate; }
        }
        public DateTime RealizationDate
        {
            get { return singleTaskModel.RealizationDate; }
        }
        public bool IsAccomplished
        {
            get { return singleTaskModel.IsAccomplished; }
        }
        public bool IsntAccomplishedAfterRealizationDate
        {
            get { return !IsAccomplished && (DateTime.Now > RealizationDate); }
        }

        #endregion

        #region Constructors

        public SingleTaskViewModel(SingleTaskModel task)
        {
            singleTaskModel = task;
        }
        public SingleTaskViewModel(string description, DateTime creationDate, DateTime realizationDate, TaskPriority priority, bool isAccomplished)
        {
            singleTaskModel = new SingleTaskModel(description, creationDate, realizationDate, priority, isAccomplished);
        }

        #endregion

        public SingleTaskModel GetModel()
        {
            return singleTaskModel;
        }

        public ICommand MarkAsAccomplished
        {
            get
            {
                if (markAsAccomplished == null)
                    markAsAccomplished = new RelayCommand(
                        o =>
                        {
                            singleTaskModel.IsAccomplished = true;
                            OnPropertyChanged(nameof(IsAccomplished));
                            OnPropertyChanged(nameof(IsntAccomplishedAfterRealizationDate));
                        },
                        o =>
                        {
                            return !singleTaskModel.IsAccomplished;
                        });
                return markAsAccomplished;
            }
        }
        public ICommand MarkAsNotAccomplished
        {
            get
            {
                if (markAsNotAccomplished == null)
                    markAsNotAccomplished = new RelayCommand(
                        o =>
                        {
                            singleTaskModel.IsAccomplished = false;
                            OnPropertyChanged(nameof(IsAccomplished));
                            OnPropertyChanged(nameof(IsntAccomplishedAfterRealizationDate));
                        },
                        o =>
                        {
                            return singleTaskModel.IsAccomplished;
                        });
                return markAsNotAccomplished;
            }
        }
    }
}
