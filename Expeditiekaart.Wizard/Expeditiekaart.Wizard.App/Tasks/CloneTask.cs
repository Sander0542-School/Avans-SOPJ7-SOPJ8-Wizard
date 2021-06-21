using System.Threading.Tasks;
using Expeditiekaart.Wizard.App.Tasks.Common;

namespace Expeditiekaart.Wizard.App.Tasks
{
    public class CloneTask : WizardTask
    {
        protected override async Task<TaskResult> Execute()
        {
            ProgressWriter.AddMessage("Cloning repository from GitHub");
            await RunCommand("git clone git@github.com:avrijn/SOPROJ7.git .");

            return TaskResult.Ok;
        }
    }
}
