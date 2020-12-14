using System.Windows.Controls;
using SpaceTree.Pages;

namespace SpaceTree {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            
            ContentFrame.Content = new Frame() {
                Content = new DiskInfoPage()
            };
        }
    }
}