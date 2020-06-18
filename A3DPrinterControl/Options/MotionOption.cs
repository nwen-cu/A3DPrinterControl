using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace A3DPrinterControl
{
	[DataContract]
	public class MotionOption : IBindable
	{
		private double _markingSpeed = 10;
		[DataMember]
		public double MarkingSpeed
		{
			get
			{
				return _markingSpeed;
			}
			set
			{
				_markingSpeed = value;
				OnPropertyChanged("MarkingSpeed");
			}
		}

		private double _jumpSpeed = 10;
		[DataMember]
		public double JumpSpeed
		{
			get
			{
				return _jumpSpeed;
			}
			set
			{
				_jumpSpeed = value;
				OnPropertyChanged("JumpSpeed");
			}
		}

		public MotionOptionView OptionView => MotionOptionView.Show(this);
	}
}
