using System.IO;
using System.Threading.Tasks;
using Expeditiekaart.Wizard.App.Tasks.Common;

namespace Expeditiekaart.Wizard.App.Tasks
{
    public class EnvTask : WizardTask
    {
        protected override async Task<TaskResult> Execute()
        {
            ProgressWriter.AddMessage("Creating .env file");
            string[] lines =
            {
                "APP_URL=expeditiekaart.nl",
                "DB_CONNECTION=mysql",
                "",
                $"DB_HOST={Options.Database.Host}",
                $"DB_PORT={Options.Database.Port}",
                $"DB_USERNAME={Options.Database.Username}",
                $"DB_PASSWORD={Options.Database.Password}",
                $"DB_DATABASE={Options.Database.Database}",
                "",
                "SESSION_DRIVER=database",
            };
            await File.WriteAllLinesAsync($"{Options.Location.Path}/.env", lines);

            ProgressWriter.AddMessage("Generating app key");
            await RunCommand("php artisan key:generate");

            return TaskResult.Ok;
        }
    }
}
