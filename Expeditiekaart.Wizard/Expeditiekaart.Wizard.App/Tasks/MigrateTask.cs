using System.Threading.Tasks;
using Expeditiekaart.Wizard.App.Tasks.Common;

namespace Expeditiekaart.Wizard.App.Tasks
{
    public class MigrateTask : WizardTask
    {
        protected override async Task<TaskResult> Execute()
        {
            ProgressWriter.AddMessage("Migrating database");
            await RunCommand("php artisan migrate:fresh --seed --force");

            return TaskResult.Ok;
        }
    }
}
