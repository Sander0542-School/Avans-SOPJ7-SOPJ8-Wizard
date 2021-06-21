using System.Threading.Tasks;
using Expeditiekaart.Wizard.App.Tasks.Common;

namespace Expeditiekaart.Wizard.App.Tasks
{
    public class NodeTask : WizardTask
    {
        protected override async Task<TaskResult> Execute()
        {
            ProgressWriter.AddMessage("Installing NPM Modules");
            await RunCommand("npm ci");

            ProgressWriter.AddMessage("Building website assets");
            await RunCommand("npm run production");

            return TaskResult.Ok;
        }
    }
}
