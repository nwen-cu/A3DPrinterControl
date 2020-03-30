using System;
using System.Collections.Generic;
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
	public partial class RectangleShapeCommandOptionView : UserControl
	{
		private static RectangleShapeCommandOptionView Instance = new RectangleShapeCommandOptionView();

		public static RectangleShapeCommandOptionView Show(RectangleShapeCommand cmd)
		{
			Instance.DataContext = cmd;
			return Instance;
		}

		public RectangleShapeCommandOptionView()
		{
			InitializeComponent();
		}
	}
}
