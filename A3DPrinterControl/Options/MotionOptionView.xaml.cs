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
	/// MotionOptionView.xaml 的交互逻辑
	/// </summary>
	public partial class MotionOptionView : UserControl
	{
		private static MotionOptionView Instance = new MotionOptionView();

		public MotionOptionView()
		{
			InitializeComponent();
		}

		public static MotionOptionView Show(MotionOption motion)
		{
			Instance.DataContext = motion;
			return Instance;
		}
	}
}
