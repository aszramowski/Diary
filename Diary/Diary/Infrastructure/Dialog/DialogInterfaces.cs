using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Diary.Infrastructure.Dialog
{
    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
    }

    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                           where TView : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;
    }

    /// <summary>
    /// Expose the event that viewmodel will invoke; Event arguments = what should the dialog result be when closing 
    /// </summary>
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
