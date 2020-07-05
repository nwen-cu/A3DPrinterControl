using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace A3DPrinterControl 
{
	[DataContract(IsReference = true), KnownType(typeof(PolygonCADShape))]
	public class PolygonShapeCommand : IBindable, IShapeCommand, IInfill
	{
		[DataMember]
		public ICADShape Shape { get; private set; }

		[DataMember]
		private string _descriptionName = "Polygon";

		public PolygonShapeCommand()
		{
			Shape = new PolygonCADShape(this); 
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
		public ListViewItem RecipeViewItem { get; private set; }

		public UserControl OptionView => PolygonShapeCommandOptionView.Show(this);

		[DataMember]
		public IActionCommand ParentCommand { get; set; } = null;

		[DataMember]
		public ActionCommandCollection ChildrenCommands { get; private set; } = new ActionCommandCollection();

		[DataMember]
		public MotionOption MotionOption { get; private set; } = new MotionOption();

		[DataMember]
		public InfillOption InfillOption { get; private set; } = new InfillOption();

		public ImageSource Icon => ImageResources.Load("Icons", "PolygonShape");

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
			CADCanvas.MainCanvas.SizeChanged += (Shape as PolygonCADShape).UpdateControl;
		}

		public void OnRemove()
		{
			CADCanvas.RemoveShape(Shape);
			CADCanvas.MainCanvas.SizeChanged -= (Shape as PolygonCADShape).UpdateControl;
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
