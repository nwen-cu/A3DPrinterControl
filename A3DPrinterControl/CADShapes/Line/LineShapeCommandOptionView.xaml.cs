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
	/// LineShapeCommandOptionView.xaml 的交互逻辑
	/// </summary>
	public partial class LineShapeCommandOptionView : UserControl
	{
		private static LineShapeCommandOptionView Instance = new LineShapeCommandOptionView();

		public static LineShapeCommandOptionView Show(LineShapeCommand cmd)
		{
			Instance.DataContext = cmd;
			return Instance;
		}

		public LineShapeCommandOptionView()
		{
			InitializeComponent();
		}
	}
}
