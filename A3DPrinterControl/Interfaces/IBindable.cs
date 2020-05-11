using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace A3DPrinterControl
{
	public class IBindable
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string info)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
		}
	}
}
