using System;
using System.Collections.Generic;
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
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Icon;

namespace SpaceTree.Libs.Controls {
    /// <summary>
    /// Interaction logic for DiskCard.xaml
    /// </summary>
    public partial class DiskCard : UserControl {
        public DiskCard() {
            InitializeComponent();
            DiskInfoCache diskInfo = DataContext as DiskInfoCache ?? new DiskInfoCache("Null", "Fixed", 0, 0);
            DiskIcon.Source = IconHelper.GetDiskIcon(diskInfo.DiskType);
            DiskName.Text = diskInfo.Name;
            DiskUsage.Text = diskInfo.GetFormattedUsage();
        }
    }
}