using System;
using System.Runtime.InteropServices;

namespace ControlLib.Components.Window.Data {
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }
}