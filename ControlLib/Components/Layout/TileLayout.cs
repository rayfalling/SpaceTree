using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace ControlLib.Components.Layout {
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:ControlLib="clr-namespace:ControlLib.Components.Layout"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:ControlLib="clr-namespace:ControlLib.Components.Layout;assembly=ControlLib.Components.Layout"
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
    ///     <ControlLib:Container/>
    ///
    /// </summary>
    public class TileLayout : Control {
        #region Construct

        static TileLayout() {
            var type = typeof(TileLayout);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));
        }

        #endregion

        #region Property

        #region ItemMinHeight

        public double ItemMinHeight {
            get => (double) GetValue(ItemMinHeightProperty);
            set => SetValue(ItemMinHeightProperty, value);
        }

        public static readonly DependencyProperty ItemMinHeightProperty = DependencyProperty.Register(
            "ItemMinHeight",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(40.0)
        );

        #endregion

        #region ItemMinWidth

        public double ItemMinWidth {
            get => (double) GetValue(ItemMinWidthProperty);
            set => SetValue(ItemMinWidthProperty, value);
        }

        public static readonly DependencyProperty ItemMinWidthProperty = DependencyProperty.Register(
            "ItemMinWidth",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(40.0)
        );

        #endregion

        #region ItemMaxHeight

        public double ItemMaxHeight {
            get => (double) GetValue(ItemMaxHeightProperty);
            set => SetValue(ItemMaxHeightProperty, value);
        }

        public static readonly DependencyProperty ItemMaxHeightProperty = DependencyProperty.Register(
            "ItemMaxHeight",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(80.0)
        );

        #endregion

        #region ItemMaxWidth

        public double ItemMaxWidth {
            get => (double) GetValue(ItemMaxWidthProperty);
            set => SetValue(ItemMaxWidthProperty, value);
        }

        public static readonly DependencyProperty ItemMaxWidthProperty = DependencyProperty.Register(
            "ItemMaxWidth",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(80.0)
        );

        #endregion

        #region ItemsHeight

        public double ItemsHeight {
            get => (double) GetValue(ItemsHeightProperty);
            set => SetValue(ItemsHeightProperty, value);
        }

        public static readonly DependencyProperty ItemsHeightProperty = DependencyProperty.Register(
            "ItemsHeight",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(0.0)
        );

        #endregion

        #region ItemsWidth

        public double ItemsWidth {
            get => (double) GetValue(ItemsWidthProperty);
            set => SetValue(ItemsWidthProperty, value);
        }

        public static readonly DependencyProperty ItemsWidthProperty = DependencyProperty.Register(
            "ItemsWidth",
            typeof(double),
            typeof(TileLayout),
            new PropertyMetadata(0.0)
        );

        #endregion

        #region Rows

        public int Rows {
            get => (int) GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
            "Rows",
            typeof(int),
            typeof(TileLayout),
            new PropertyMetadata(0)
        );

        #endregion

        #region Columns

        public int Columns {
            get => (int) GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
            "Columns",
            typeof(int),
            typeof(TileLayout),
            new PropertyMetadata(0)
        );

        #endregion

        #endregion

        #region Override

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            //TODO here
        }

        #endregion

        #region Internal Methods

        internal void CalculateColumns() {
            var width = ActualWidth;
            var height = ActualHeight;
        }

        #endregion

        #region Events

        #endregion

        #region Data

        private ItemCollection _item;
        private ItemContainerGenerator _itemContainerGenerator;
        private Panel _itemsHost;
        private ScrollViewer _scrollHost;

        #endregion
    }
}