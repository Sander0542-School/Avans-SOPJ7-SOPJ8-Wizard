using System;

namespace Expeditiekaart.Wizard.App.Progress
{
    public class ProgressWriter
    {
        public void AddMessage(string message)
        {
            OnMessagesAdded(new ProgressEventArgs
            {
                Message = message
            });
        }

        protected virtual void OnMessagesAdded(ProgressEventArgs e)
        {
            NewMessage?.Invoke(this, e);
        }

        public event EventHandler<ProgressEventArgs> NewMessage;
    }
}
