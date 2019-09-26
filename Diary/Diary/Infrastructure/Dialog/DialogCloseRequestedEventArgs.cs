using System;

namespace Diary.Infrastructure.Dialog
{
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public bool? DialogResult { get; }

        public DialogCloseRequestedEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}