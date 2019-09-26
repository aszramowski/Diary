using Diary.Infrastructure.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Diary.ViewModel
{
    public class YesNoDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        private string _message = "";
        private string _title = "";
        private ICommand _noCommand;
        private ICommand _yesCommand;

        public YesNoDialogViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value)
                {
                    return;
                }
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                {
                    return;
                }
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public ICommand NoCommand
        {
            get
            {
                return _noCommand
                    ?? (_noCommand = new RelayCommand(
                    (o) =>
                    {
                        CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false));
                    },
                    (o) => true));
            }
        }        

        public ICommand YesCommand
        {
            get
            {
                return _yesCommand
                    ?? (_yesCommand = new RelayCommand(
                    (o) =>
                    {
                        CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
                    },
                    (o) => true));
            }
        }
    }
}
