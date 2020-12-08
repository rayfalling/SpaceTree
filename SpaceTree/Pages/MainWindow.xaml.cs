using System.Collections.ObjectModel;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Helper.Disk;

namespace SpaceTree.Pages {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
    
    internal class MainWindowViewModel {
        public ObservableCollection<DiskInfoCache> DiskInfoCaches { get; set; }

        public MainWindowViewModel() {
            DiskInfoCaches = new ObservableCollection<DiskInfoCache>(DiskInfo.GetSystemDriveInfos());
        }
    }
}