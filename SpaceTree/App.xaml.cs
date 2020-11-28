using System.IO;
using System.Windows;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Config.Exclude;
using SpaceTree.Libs.Helper;
using SpaceTree.Libs.Logger;
using SpaceTree.Pages;

namespace SpaceTree {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        internal static ExcludeMatch ExcludeMatch = ExcludeMatch.GetInstance();
        internal static Logger Logger = Logger.GetInstance();

        static App() {
        }

        private void StartUp(object sender, StartupEventArgs e) {
            // Log start
            Logger.Log(LogLevel.Info, "SpaceTree start.");

            // Load exclude config
            ExcludeMatch.LoadConfig();

            //TODO Load config & cache here

            var directoryCache = FileHelper.LoadDirectory("c:/");
            Logger.Log(LogLevel.Info, directoryCache.GetFormatSize());
            Logger.Log(LogLevel.Info, UnauthorizedAccessCache.GetInstance().GetMissCount().ToString());

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}