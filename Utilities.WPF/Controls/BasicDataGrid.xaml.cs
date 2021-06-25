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
				var CenterStyle = (Style)FindResource("CenterStyle");

				foreach (DataGridColumn Col in Columns)
					if (GetAlignment(Col) == HorizontalAlignment.Center)
						Col.CellStyle = CenterStyle;
			};
		}

		public static void SetAlignment(DataGridColumn Col, HorizontalAlignment Value) => Col.SetValue(AlignmentProperty, Value);
		public static HorizontalAlignment GetAlignment(DataGridColumn Col) => (HorizontalAlignment)Col.GetValue(AlignmentProperty);
	}
}
