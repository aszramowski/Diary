using Diary.Infrastructure.Dialog;
using Diary.View;
using Diary.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Diary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static IDialogService dialogService;

        protected override void OnStartup(StartupEventArgs e)
        {
            dialogService = new DialogService(MainWindow);
            dialogService.Register<YesNoDialogViewModel, YesNoDialogView>();
        }
    }
}
