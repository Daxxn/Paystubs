using PaystubJsonApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PaystubJsonApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            Debug.Debug.Instance.OnStartup(e.Args);
            FileControl.FileManager.OnStartup();
            var main = new MainWindow(new MainViewModel());
            main.Show();
            Debug.Debug.Instance.Post("Startup Completed.");
        }

        protected override void OnExit( ExitEventArgs e )
        {
            Debug.Debug.Instance.OnExit(e.ApplicationExitCode);
        }
    }
}
