using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Expeditiekaart.Wizard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InstallApplication(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(TbLocationPath.Text))
            {
                MessageBox.Show(this, "The location path does not exist.", "Invalid Configuration", MessageBoxButton.OK);
                return;
            }
            
            
        }
    }
}