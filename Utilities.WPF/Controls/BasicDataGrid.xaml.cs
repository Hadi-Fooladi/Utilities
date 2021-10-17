using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace HaFT.Utilities.WPF
{
	public partial class BasicDataGrid
	{
		private static readonly DependencyProperty
			AlignmentProperty = DPH<HorizontalAlignment, BasicDataGrid>.RegisterAttached("Alignment", HorizontalAlignment.Left);

		public BasicDataGrid()
		{
			InitializeComponent();
			if (DesignerProperties.GetIsInDesignMode(this)) return;

			Loaded += (s, e) =>
			{
				Style
					rightStyle = (Style)FindResource("RightStyle"),
					centerStyle = (Style)FindResource("CenterStyle");

				foreach (DataGridColumn col in Columns)
					switch (GetAlignment(col))
					{
					case HorizontalAlignment.Right:
						col.CellStyle = rightStyle;
						break;

					case HorizontalAlignment.Center:
						col.CellStyle = centerStyle;
						break;
					}
			};
		}

		public static void SetAlignment(DataGridColumn Col, HorizontalAlignment Value) => Col.SetValue(AlignmentProperty, Value);
		public static HorizontalAlignment GetAlignment(DataGridColumn Col) => (HorizontalAlignment)Col.GetValue(AlignmentProperty);
	}
}
