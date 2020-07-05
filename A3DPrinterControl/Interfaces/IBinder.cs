using System;
using System.Collections.Generic;
using System.Text;

namespace A3DPrinterControl
{
	public class IBinder : IBindable
	{
		public new void OnPropertyChanged(string info)
		{
			base.OnPropertyChanged(info);
		}
	}
}
