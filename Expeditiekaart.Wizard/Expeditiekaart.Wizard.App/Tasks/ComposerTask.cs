using System.Threading.Tasks;
using Expeditiekaart.Wizard.App.Tasks.Common;

namespace Expeditiekaart.Wizard.App.Tasks
{
    public class ComposerTask : WizardTask
    {
        protected override async Task<TaskResult> Execute()
        {
            ProgressWriter.AddMessage("Installing Composer dependencies");
            await RunCommand("composer install --no-ansi --no-interaction --no-scripts --prefer-dist");

            return TaskResult.Ok;
        }
    }
}
