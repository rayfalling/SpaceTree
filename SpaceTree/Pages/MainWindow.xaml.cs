using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Helper.Disk;

namespace SpaceTree.Pages {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private List<DiskInfoCache> _driveInfos;

        public MainWindow() {
            InitializeComponent();
            _driveInfos = DiskInfo.GetSystemDriveInfos();
        }
    }
}