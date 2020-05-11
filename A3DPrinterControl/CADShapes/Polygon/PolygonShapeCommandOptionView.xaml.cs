using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	/// <summary>
	/// RectangleShapeCommandOptionView.xaml 的交互逻辑
	/// </summary>
	public partial class PolygonShapeCommandOptionView : UserControl
	{
		private static PolygonShapeCommandOptionView Instance = new PolygonShapeCommandOptionView();

		public static PolygonShapeCommandOptionView Show(PolygonShapeCommand cmd)
		{
			Instance.DataContext = cmd;
			return Instance;
		}

		public PolygonShapeCommandOptionView()
		{
			InitializeComponent();
		}

		private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			((Instance.DataContext as PolygonShapeCommand).Shape as PolygonCADShape).UpdateControl();
		}
	}
}
