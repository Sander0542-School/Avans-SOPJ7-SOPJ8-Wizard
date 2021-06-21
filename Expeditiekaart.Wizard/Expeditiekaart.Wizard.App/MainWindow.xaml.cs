using System.IO;
using System.Windows;
using System.Windows.Forms;
using Expeditiekaart.Wizard.App.Installers;
using Expeditiekaart.Wizard.App.Models;
using Expeditiekaart.Wizard.App.Progress;
using MessageBox=System.Windows.MessageBox;

namespace Expeditiekaart.Wizard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FolderBrowserDialog _locationFolderDialog;

        public MainWindow()
        {
            InitializeComponent();

            _locationFolderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };
        }

        private async void InstallApplication(object sender, RoutedEventArgs e)
        {
            var progressWriter = new ProgressWriter();
            progressWriter.NewMessage += ProgressMessageReceived;

            if (!Directory.Exists(TbLocationPath.Text))
            {
                MessageBox.Show(this, "The location path does not exist.", "Invalid Configuration", MessageBoxButton.OK);
                return;
            }

            var dbHost = ValueOrFallback(TbDatabaseHost.Text, "localhost");
            var dbPort = ValueOrFallback(TbDatabasePort.Text, "3306");
            var dbUsername = ValueOrFallback(TbDatabaseUsername.Text, "root");
            var dbPassword = ValueOrFallback(TbDatabasePassword.Text, "");
            var dbDatabase = ValueOrFallback(TbDatabaseDatabase.Text, "expeditiekaart");

            var options = new ExpeditiekaartOptions
            {
                Location = new LocationOptions
                {
                    Path = TbLocationPath.Text
                },
                Database = new DatabaseOptions
                {
                    Host = dbHost,
                    Port = dbPort,
                    Username = dbUsername,
                    Password = dbPassword,
                    Database = dbDatabase,
                }
            };

            var installer = new ProjectInstaller(options, progressWriter);
            var result = await installer.Install();

            switch (result)
            {
                case InstallerResult.Ok:
                    progressWriter.AddMessage("Finishes installing");
                    break;
                case InstallerResult.Error:
                    progressWriter.AddMessage("Could not install website");
                    break;
            }
        }

        private void ProgressMessageReceived(object? sender, ProgressEventArgs e)
        {
            tbProgress.Text += $"\n{e.Message}";
        }

        private void PickLocationFolder(object sender, RoutedEventArgs e)
        {
            var result = _locationFolderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TbLocationPath.Text = _locationFolderDialog.SelectedPath;
            }
        }

        private static string ValueOrFallback(string value, string fallback) => string.IsNullOrWhiteSpace(value) ? fallback : value;
    }
}
