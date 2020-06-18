using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
	/// InfillOptionView.xaml 的交互逻辑
	/// </summary>
	public partial class InfillOptionView : UserControl
	{
		private static InfillOptionView Instance = new InfillOptionView();

		public InfillOptionView()
		{
			InitializeComponent();
		}

		public static InfillOptionView Show(InfillOption infill)
		{
			Instance.DataContext = infill;
			return Instance;
		}
	}
}
