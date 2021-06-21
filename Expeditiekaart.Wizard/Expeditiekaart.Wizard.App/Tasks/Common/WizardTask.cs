using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Expeditiekaart.Wizard.App.Models;
using Expeditiekaart.Wizard.App.Progress;

namespace Expeditiekaart.Wizard.App.Tasks.Common
{
    public abstract class WizardTask
    {
        protected ExpeditiekaartOptions Options;
        protected ProgressWriter ProgressWriter;

        protected abstract Task<TaskResult> Execute();

        public async Task<TaskResult> Run(ExpeditiekaartOptions options, ProgressWriter progressWriter)
        {
            Options = options;
            ProgressWriter = progressWriter;

            try
            {
                return await Execute();
            }
            catch (Exception)
            {
                return TaskResult.Error;
            }
        }

        protected async Task RunCommand(string command)
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = Options.Location.Path,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe",
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
            };

            var process = Process.Start(startInfo);

            if (process == null)
            {
                throw new NullReferenceException("Process should not be null");
            }

            await process.StandardInput.WriteLineAsync($"{command} & exit");
            await process.WaitForExitAsync();
        }
    }

}
