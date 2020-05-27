using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true)]
	[KnownType(typeof(IBindable))]
	public class IBindable
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string info)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
		}
	}
}
