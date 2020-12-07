using System;
using System.Windows;
using System.Windows.Interop;

namespace ControlLib.Components.Theme {
    public abstract class ThemeHandler {
        protected static ThemeHandler Instance { get; set; } = null!;

        #region Construct

        static ThemeHandler() {
        }

        internal ThemeHandler() {
            var win = Application.Current.MainWindow;
            if (win != null) {
                Initialize(win);
            } else {
                void Handler(object? e, EventArgs args) {
                    Initialize(Application.Current.MainWindow!);
                    Application.Current.Activated -= Handler;
                }

                Application.Current.Activated += Handler;
            }
        }

        #endregion

        #region Initialize

        private void Initialize(System.Windows.Window win) {
            if (win.IsLoaded) {
                InitializeCore(win);
            } else {
                win.Loaded += (_, __) => { InitializeCore(win); };
            }
        }

        protected virtual void InitializeCore(System.Windows.Window win) {
            var source = HwndSource.FromHwnd(new WindowInteropHelper(win).Handle);
            source?.AddHook(WndProc);
        }

        #endregion

        #region Win32 API

        protected abstract IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);

        #endregion
    }
}