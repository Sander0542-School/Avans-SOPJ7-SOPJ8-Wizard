using System;
using Expeditiekaart.Wizard.App.Tasks.Common;

namespace Expeditiekaart.Wizard.App.Exceptions
{
    public class TaskException : Exception
    {
        public TaskException(WizardTask task) : base($"The {task.GetType().Name} did not finish successfully.")
        {
        }
    }
}
