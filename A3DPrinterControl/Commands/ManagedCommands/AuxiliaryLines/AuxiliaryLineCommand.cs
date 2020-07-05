using A3DPrinterControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace A3DPrinterControl
{
	[DataContract]
	class AuxiliaryLineCommand : LineShapeCommand
	{

		[DataMember]
		private string _descriptionName = "AuxiliaryLine";
		public new string DescriptionName
		{
			get
			{
				return _descriptionName;
			}
			set
			{
				_descriptionName = value;
				OnPropertyChanged("DescriptionName");
			}
		}

		public new UserControl OptionView => null;

		public new ImageSource Icon => null;
	}
}
