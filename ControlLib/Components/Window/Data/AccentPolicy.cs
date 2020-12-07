using System.Runtime.InteropServices;

namespace ControlLib.Components.Window.Data {
    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy {
        public AccentState AccentState;
        public int AccentFlags;
        public uint GradientColor;
        public int AnimationId;
    }
}