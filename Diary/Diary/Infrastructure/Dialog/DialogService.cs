using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Diary.Infrastructure.Dialog
{
    public class DialogService : IDialogService
    {
        private readonly Window owner;
        
        // Constructor specify the owner window of the dialog
        public DialogService(Window owner)
        { 
            this.owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        public IDictionary<Type, Type> Mappings { get; }

        public void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
                throw new Exception($"Type of {typeof(TViewModel)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TViewModel), typeof(TView)); 
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            // A caller will give us an instance of ViewModel and we will use the mappings to find the corresponding View that ViewModel is associated with
            Type viewType = Mappings[typeof(TViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            EventHandler<DialogCloseRequestedEventArgs> handler = null;

            // Responding to the close event
            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                    dialog.DialogResult = e.DialogResult;
                else
                    dialog.Close();
            };

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = owner;
            
            return dialog.ShowDialog();
        }        
    }
}
