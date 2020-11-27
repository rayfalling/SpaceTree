using System.Windows;

namespace SpaceTree {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void StartUp(object sender, StartupEventArgs e) {
            //TODO Load config & cache here

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}