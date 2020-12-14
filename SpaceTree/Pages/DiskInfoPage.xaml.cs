using System.Collections.ObjectModel;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Helper.Disk;

namespace SpaceTree.Pages {
    /// <summary>
    /// Interaction logic for DiskInfo.xaml
    /// </summary>
    public partial class DiskInfoPage {
        public DiskInfoPage() {
            InitializeComponent();
            TileLayout.ItemSource = new DiskInfoPageViewModel().DiskInfoCaches;
        }
    }

    internal class DiskInfoPageViewModel {
        public ObservableCollection<DiskInfoCache> DiskInfoCaches { get; set; }

        public DiskInfoPageViewModel() {
            DiskInfoCaches = new ObservableCollection<DiskInfoCache>(DiskInfo.GetSystemDriveInfos());
        }
    }
}