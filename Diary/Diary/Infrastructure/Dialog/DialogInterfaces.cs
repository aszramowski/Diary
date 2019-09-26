using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Diary.Infrastructure.Dialog
{
    /// <summary>
    /// Intended for WPF Window
    /// </summary>
    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
    }

    /// <summary>
    /// Registering ViewModels and associate them with Views
    /// IDialogRequestClose - ViewModel needs to communicate with DialogService when the View should be closed
    /// We will create an instance of the ViewModel but we dont want to be responsible for creation of the associated View
    /// </summary>
    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                           where TView : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;
    }

    /// <summary>
    /// Expose the event that viewmodel will invoke; Event arguments = what should the dialog result be, when closing 
    /// </summary>
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
