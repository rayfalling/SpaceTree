using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpaceTree.Libs.Controls.Layout {
    /// <summary>
    /// Interaction logic for TileLayout.xaml
    /// </summary>
    public partial class TileLayout {
        public int RowCount {
            get => (int) GetValue(RowCountProperty);
            set => SetValue(RowCountProperty, value);
        }

        public static readonly DependencyProperty RowCountProperty = DependencyProperty.Register(
            "RowCount",
            typeof(int),
            typeof(TileLayout),
            new PropertyMetadata(0)
        );

        public int ColumnCount {
            get => (int) GetValue(ColumnCountProperty);
            set => SetValue(ColumnCountProperty, value);
        }

        public static readonly DependencyProperty ColumnCountProperty = DependencyProperty.Register(
            "ColumnCount",
            typeof(int),
            typeof(TileLayout),
            new PropertyMetadata(0)
        );

        public Thickness MarginThickness {
            get => (Thickness) GetValue(MarginThicknessProperty);
            set => SetValue(MarginThicknessProperty, value);
        }

        public static readonly DependencyProperty MarginThicknessProperty = DependencyProperty.Register(
            "MarginThickness",
            typeof(Thickness),
            typeof(TileLayout),
            new PropertyMetadata(new Thickness(0))
        );

        public Grid? LayoutGrid { get; set; }

        internal ObservableCollection<LayoutItemData> ListData { get; set; }

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

        public double ItemHeight {
            get => (double) GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
            "ItemHeight",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(80.0)
        );

        public double ItemWidth {
            get => (double) GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
            "ItemWidth",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(80.0)
        );

        public TileLayout() {
            InitializeComponent();
            ListData = new ObservableCollection<LayoutItemData>();
            UpdateLayoutGrid();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateLayoutGrid();
        }

        private void ComputeLayout() {
            var count = ListData.Count;
            if (ActualWidth == 0 || ActualHeight == 0 || count == 0) {
                return;
            }

            var properColumn = (int) (ActualWidth / ItemWidth);
            var properRow = (count / properColumn) + (count % properColumn == 0 ? 0 : 1);
            var padding = (int) ((ActualWidth - ItemWidth * properColumn) / 2);
            MarginThickness = new Thickness(padding, 0, padding, 0);
            RowCount = properRow;
            ColumnCount = properColumn;
            if (LayoutGrid != null) LayoutGrid.Margin = MarginThickness;
        }

        private void SetChildrenStyle() {
            if (ColumnCount == 0) return;
            var tempList = ListData.Select(
                (t, index) => new LayoutItemData {Data = t.Data, Row = index / ColumnCount, Column = index % ColumnCount,}
            ).ToList();
            ListData = new ObservableCollection<LayoutItemData>(tempList);
        }

        private void UpdateLayoutGrid() {
            ComputeLayout();
            RefreshColumnAndRow();
            SetChildrenStyle();
            InnerItemsControl.ItemsSource = ListData;
        }

        protected void RefreshColumnAndRow() {
            if (LayoutGrid == null) return;
            var neededRow = Math.Abs(LayoutGrid.RowDefinitions.Count - RowCount);
            if (RowCount > LayoutGrid.RowDefinitions.Count)
                for (var count = 0; count < neededRow; count++) {
                    LayoutGrid.RowDefinitions.Add(new RowDefinition());
                }
            else
                for (var count = 0; count < neededRow; count++) {
                    LayoutGrid.RowDefinitions.RemoveAt(LayoutGrid.RowDefinitions.Count - 1);
                }

            var neededCol = Math.Abs(LayoutGrid.ColumnDefinitions.Count - ColumnCount);
            if (ColumnCount > LayoutGrid.ColumnDefinitions.Count)
                for (var count = 0; count < neededCol; count++) {
                    LayoutGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
            else
                for (var count = 0; count < neededCol; count++) {
                    LayoutGrid.ColumnDefinitions.RemoveAt(LayoutGrid.ColumnDefinitions.Count - 1);
                }
        }

        private static void ItemDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var layout = (TileLayout) d;
            layout.ItemSource = (IEnumerable<object>) e.NewValue;
            layout.ListData = new ObservableCollection<LayoutItemData>(
                (layout.ItemSource as IEnumerable<object> ?? new List<object>()).Select(item =>
                    new LayoutItemData {
                        Column = 0, Row = 0, Data = item
                    }
                )
            );
            layout.UpdateLayoutGrid();
        }

        private static void ItemTemplateDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var layout = (TileLayout) d;
            layout.ItemTemplateData = (DataTemplate) e.NewValue;
            layout.UpdateLayoutGrid();
        }

        private void GridLoaded(object sender, RoutedEventArgs e) {
            LayoutGrid = (Grid) sender;
            UpdateLayoutGrid();
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