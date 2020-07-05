using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true), KnownType(typeof(RectangleCADShape))]
	public class RectangleShapeCommand : IBindable, IShapeCommand, IInfill
	{
		[DataMember]
		private string _descriptionName = "Rectangle";
		public RectangleShapeCommand()
		{
			Shape = new RectangleCADShape(this);
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{

		}

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

		[DataMember]
		public ICADShape Shape { get; private set; }

		public UserControl OptionView => RectangleShapeCommandOptionView.Show(this);

		[DataMember]
		public IActionCommand ParentCommand { get; set; } = null;

		[DataMember]
		public ActionCommandCollection ChildrenCommands { get; private set; } = new ActionCommandCollection();

		public ListViewItem RecipeViewItem { get; private set; }

		[DataMember]
		public MotionOption MotionOption { get; private set; } = new MotionOption();

		[DataMember]
		public InfillOption InfillOption { get; private set; } = new InfillOption();

		public ImageSource Icon => ImageResources.Load("Icons", "RectangleShape");

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
		}

		public void OnRemove()
		{
			CADCanvas.RemoveShape(Shape);
		}

		public void OnSelect()
		{
			Shape.OnSelect();
		}

		public void OnDeselect()
		{
			Shape.OnDeselect();
		}
	}
}
