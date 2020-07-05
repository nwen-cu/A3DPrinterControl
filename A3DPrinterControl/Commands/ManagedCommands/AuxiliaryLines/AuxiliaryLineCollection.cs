using A3DPrinterControl.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace A3DPrinterControl
{
	[DataContract]
	public class AuxiliaryLineCollection : IBindable, IManagedCommand, IMotion
	{

		[DataMember]
		private string _descriptionName = "AuxiliaryLines";
		public string DescriptionName
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

		public UserControl OptionView { get; }

		public ImageSource Icon => ImageResources.Load("Icons", "AuxiliaryLines");

		[DataMember]
		public IActionCommand ParentCommand { get; set; }

		[DataMember]
		public ActionCommandCollection ChildrenCommands { get; private set; } = new ActionCommandCollection();

		[DataMember]
		public MotionOption MotionOption { get; private set; } = new MotionOption();

		public void OnCompile()
		{
			Recipe.ClearCommand(ChildrenCommands);
		}
	}
}
