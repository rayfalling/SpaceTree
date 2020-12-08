using System.Windows;
using SpaceTree.Libs.Config.Disk;
using SpaceTree.Libs.Config.File;
using SpaceTree.Libs.Logger;
using SpaceTree.Pages;

namespace SpaceTree {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        internal static FileExcludeConfig FileExcludeConfig = FileExcludeConfig.GetInstance();
        internal static DiskConfig DiskConfig = DiskConfig.GetInstance();
        internal static Logger Logger = Logger.GetInstance();

        static App() {
        }

        private void StartUp(object sender, StartupEventArgs e) {
            // Log start
            Logger.Log(LogLevel.Info, "SpaceTree start.");

            // Load exclude config
            FileExcludeConfig.LoadConfig();
            Logger.Log(LogLevel.Info, "Loading file exclude config......");

            // Load disk info config
            DiskConfig.LoadConfig();
            Logger.Log(LogLevel.Info, "Loading disk info config......");

            //TODO Load config & cache here
            //TODO judge junction in directories

            // var directoryCache = FileSizeHelper.GetDirectorySizeCache("c:/Users");;
            // Logger.Log(LogLevel.Info, directoryCache.GetFormatSize());
            // Logger.Log(LogLevel.Info, UnauthorizedAccessCache.GetInstance().GetMissCount().ToString());

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}