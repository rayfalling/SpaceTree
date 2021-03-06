﻿using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using ControlLib.Components.Window.Data;
using ControlLib.Libs.Acrylic;

namespace ControlLib.Components.Window {
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:ControlLib="clr-namespace:ControlLib.Components.Window"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:ControlLib="clr-namespace:ControlLib.Components.Window;assembly=ControlLib.Components.Window"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <ControlLib:AcrylicPopup/>
    ///
    /// </summary>
    public class AcrylicPopup : Popup {
        static AcrylicPopup() {
            var type = typeof(AcrylicPopup);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));
        }

        protected override void OnOpened(EventArgs e) {
            base.OnOpened(e);

            var accent = (Color) GetValue(AcrylicElement.AccentColorProperty);
            var accentColor = (uint) (accent.A << 24 | accent.R << 18 | accent.G << 12 | accent.B);
            var hwnd = ((HwndSource) PresentationSource.FromVisual(Child))!;
            AcrylicHelper.EnableBlur(hwnd.Handle, accentColor, AccentFlagsType.Popup);
        }
    }
}