using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ControlLib.Components.Layout {
    /// TODO: Finish TileLayout
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlLib.Components.Layout"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ControlLib.Components.Layout;assembly=ControlLib.Components.Layout"
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
    ///     <MyNamespace:TileLayout/>
    ///
    /// </summary>
    public class TileLayout : ItemsControl {
        private Grid TileGrid { get; set; }

        static TileLayout() {
            var type = typeof(TileLayout);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));
        }

        public TileLayout() {
            TileGrid = new Grid();
            ListData = new List<LayoutItemData>();
            RowCount = 2;
            ColumnCount = 2;
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            // Try get inner grid
            try {
                TileGrid = ((Grid) GetVisualChild(0))!;
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        #region Property

        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public Thickness MarginThickness { get; set; }

        internal List<LayoutItemData> ListData { get; set; }

        public IEnumerable ItemSource {
            get => (IEnumerable) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register(
            "ItemSource",
            typeof(IEnumerable),
            typeof(TileLayout),
            new UIPropertyMetadata(null, ItemDataPropertyChanged)
        );

        public DataTemplate ItemTemplateData {
            get => (DataTemplate) GetValue(ItemTemplateDataProperty);
            set => SetValue(ItemTemplateDataProperty, value);
        }

        public static readonly DependencyProperty ItemTemplateDataProperty = DependencyProperty.Register(
            "ItemTemplateData",
            typeof(DataTemplate),
            typeof(TileLayout),
            new UIPropertyMetadata(null, ItemTemplateDataPropertyChanged)
        );

        protected void ComputeChildren() {
            for (var index = 0; index < ListData.Count; index++) {
                ListData[index].Row = index    / ColumnCount;
                ListData[index].Column = index % ColumnCount;
            }
        }

        private void UpdateLayoutGrid() {
            ListData = (ItemSource as IEnumerable<object> ?? new List<object>()).Select(item => new LayoutItemData {
                Column = 0, Row = 0, Data = item
            }).ToList();
            RefreshColumnAndRow();
            ComputeChildren();
            ItemsSource = ListData;
        }

        protected void RefreshColumnAndRow() {
            var neededRow = Math.Abs(TileGrid.RowDefinitions.Count - RowCount);
            if (RowCount > TileGrid.RowDefinitions.Count)
                for (var count = 0; count < neededRow; count++) {
                    TileGrid.RowDefinitions.Add(new RowDefinition());
                }
            else
                for (var count = 0; count < neededRow; count++) {
                    TileGrid.RowDefinitions.RemoveAt(TileGrid.RowDefinitions.Count - 1);
                }

            var neededCol = Math.Abs(TileGrid.ColumnDefinitions.Count - RowCount);
            if (ColumnCount > TileGrid.ColumnDefinitions.Count)
                for (var count = 0; count < neededCol; count++) {
                    TileGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
            else
                for (var count = 0; count < neededCol; count++) {
                    TileGrid.ColumnDefinitions.RemoveAt(TileGrid.ColumnDefinitions.Count - 1);
                }
        }

        #endregion

        private static void ItemDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var layout = (TileLayout) d;
            layout.ItemSource = (IEnumerable<object>) e.NewValue;
            layout.UpdateLayoutGrid();
        }

        private static void ItemTemplateDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var layout = (TileLayout) d;
            layout.ItemTemplateData = (DataTemplate) e.NewValue;
            layout.UpdateLayoutGrid();
        }
    }

    internal class LayoutItemData {
        public LayoutItemData(object data, int row, int column) {
            Data = data;
            Row = row;
            Column = column;
        }

        public LayoutItemData() {
            Data = new object();
        }

        public object Data { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}