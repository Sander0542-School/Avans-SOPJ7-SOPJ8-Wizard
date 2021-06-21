using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Expeditiekaart.Wizard.App.Exceptions;
using Expeditiekaart.Wizard.App.Models;
using Expeditiekaart.Wizard.App.Progress;
using Expeditiekaart.Wizard.App.Tasks;
using Expeditiekaart.Wizard.App.Tasks.Common;

namespace Expeditiekaart.Wizard.App.Installers
{
    public class ProjectInstaller : IInstaller
    {
        private ExpeditiekaartOptions _options;
        private ProgressWriter _progressWriter;
        private Process _process;

        public ProjectInstaller(ExpeditiekaartOptions options, ProgressWriter progressWriter)
        {
            _options = options;
            _progressWriter = progressWriter;
        }

        public async Task<InstallerResult> Install()
        {
            try
            {
                await RunTask<CloneTask>();
                await RunTask<ComposerTask>();
                await RunTask<NodeTask>();
                await RunTask<EnvTask>();
                await RunTask<MigrateTask>();
            }
            catch (Exception e)
            {
                _progressWriter.AddMessage(e.Message);
                return InstallerResult.Error;
            }

            return InstallerResult.Ok;
        }

        private async Task RunTask<T>() where T : WizardTask, new()
        {
            var task = new T();
            var result = await task.Run(_options, _progressWriter);

            if (result == TaskResult.Error)
            {
                throw new TaskException(task);
            }
        }
    }
}
