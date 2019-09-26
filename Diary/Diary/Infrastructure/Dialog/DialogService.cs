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

        public DialogService(Window owner)
        {
            this.owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        public IDictionary<Type, Type> Mappings { get; }

        /// <summary>
        /// Mapping ViewModel with associated view in a dictionary
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TView"></typeparam>
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
            Type viewType = Mappings[typeof(TViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            EventHandler<DialogCloseRequestedEventArgs> handler = null;

            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                    dialog.DialogResult = e.DialogResult;
                else
                    dialog.Close();
            };

            viewModel.CloseRequested += handler;  // If viewModel fires an event the handler will be fired

            dialog.DataContext = viewModel;
            dialog.Owner = owner;

            //return dialog result from this method
            return dialog.ShowDialog();
        }        
    }
}
