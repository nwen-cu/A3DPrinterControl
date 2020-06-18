using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public static class Debug
	{
		public static void Log(string msg)
		{
			TextBox debug = MainWindow.Instance.FindName("DebugBox") as TextBox;

			if (string.IsNullOrEmpty(msg)) debug.Text = "";
			else debug.Text += msg;
		}

		public static void MsgBox(string msg)
		{
			MessageBox.Show(msg);
		}
	}
}
