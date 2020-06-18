using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace A3DPrinterControl
{
	[DataContract]
	public class InfillOption : IBindable
	{
		private double _density = 1;
		[DataMember]
		public double Density
		{
			get
			{
				return _density;
			}
			set
			{
				_density = value;
				OnPropertyChanged("Density");
			}
		}

		private string _pattern = "Lines";
		[DataMember]
		public string Pattern 
		{
			get
			{
				return _pattern;
			}
			set
			{
				_pattern = value;
				OnPropertyChanged("Pattern");
			}
		}

		public InfillOptionView OptionView => InfillOptionView.Show(this);
	}
}
