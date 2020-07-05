using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true), KnownType(typeof(EllipseArcCADShape))]
	public class EllipseArcShapeCommand : IBindable, IShapeCommand, IInfill
	{
		[DataMember]
		private string _descriptionName = "Ellipse Arc";

		public EllipseArcShapeCommand()
		{
			Shape = new EllipseArcCADShape(this);
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			
		}

		[DataMember]
		public ICADShape Shape { get; private set; }

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

		public UserControl OptionView => EllipseArcShapeCommandOptionView.Show(this);

		[DataMember]
		public IActionCommand ParentCommand { get; set; } = null;

		[DataMember]
		public ActionCommandCollection ChildrenCommands { get; private set; } = new ActionCommandCollection();

		[DataMember]
		public InfillOption InfillOption { get; private set; } = new InfillOption();

		[DataMember]
		public MotionOption MotionOption { get; private set; } = new MotionOption();

		public ImageSource Icon => ImageResources.Load("Icons", "EllipseArcShape");


		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
			CADCanvas.MainCanvas.SizeChanged += (Shape as EllipseArcCADShape).UpdateControl;
		}

		public void OnRemove()
		{
			CADCanvas.RemoveShape(Shape);
			CADCanvas.MainCanvas.SizeChanged -= (Shape as EllipseArcCADShape).UpdateControl;
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