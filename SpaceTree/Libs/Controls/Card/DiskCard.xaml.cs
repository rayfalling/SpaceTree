using System.Windows;
using System.Windows.Controls;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Icon;

namespace SpaceTree.Libs.Controls {
    /// <summary>
    /// Interaction logic for DiskCard.xaml
    /// </summary>
    public partial class DiskCard : UserControl {
        public DiskCard() {
            InitializeComponent();
            DataContextChanged += CustomDataContextChanged;
        }

        private void CustomDataContextChanged(object? sender, DependencyPropertyChangedEventArgs e) {
            // You can also validate the data going into the DataContext using the event args
            DiskInfoCache diskInfo = e.NewValue as DiskInfoCache ?? new DiskInfoCache("Null", "Fixed", 0, 0);
            DiskIcon.Source = IconHelper.GetDiskIcon(diskInfo.DiskType);
            DiskName.Text = diskInfo.Name;
            DiskUsage.Text = diskInfo.GetFormattedUsage();
            DiskTotal.Text = diskInfo.GetFormattedTotal();
        }

        ~DiskCard() { DataContextChanged -= CustomDataContextChanged; }
    }
}