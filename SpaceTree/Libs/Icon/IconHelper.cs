using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceTree.Libs.Icon {
    internal class IconHelper {
        internal static ImageSource GetDiskIcon(string diskType) {
            return diskType switch {
                "Fixed" => (Application.Current.FindResource("Disk") as Image)?.Source!,
                "CDRom" => (Application.Current.FindResource("CdRom") as Image)?.Source!,
                "Network" => (Application.Current.FindResource("Network") as Image)?.Source!,
                "Removable" => (Application.Current.FindResource("Removable") as Image)?.Source!,
                "SystemDisk" => (Application.Current.FindResource("SystemDisk") as Image)?.Source!,
                _ => (Application.Current.FindResource("Disk") as Image)?.Source!
            };
        }
    }
}