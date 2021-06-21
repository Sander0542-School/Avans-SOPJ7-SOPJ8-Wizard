using System.Threading.Tasks;

namespace Expeditiekaart.Wizard.App.Installers
{
    public interface IInstaller
    {
        public Task<InstallerResult> Install();
    }
}
