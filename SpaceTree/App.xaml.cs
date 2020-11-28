using System.Windows;
using SpaceTree.Libs.Logger;
using SpaceTree.Pages;

namespace SpaceTree {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {

        private void StartUp(object sender, StartupEventArgs e) {
            //TODO Load config & cache here

            // Log start
            Logger.GetInstance().Log(LogLevel.Info, "SpaceTree start.");

            // var directoryCache = FileHelper.LoadDirectory("j:/VisReal");
            // _logHelper.Log(LogLevel.Info, directoryCache.GetFormatSize());

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}