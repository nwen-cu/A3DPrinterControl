using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true), KnownType(typeof(LineCADShape))]
	public class LineShapeCommand : IBindable, IShapeCommand
	{
		[DataMember]
		private string _descriptionName = "Line";
		public LineShapeCommand()
		{
			Shape = new LineCADShape(this);
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

		public UserControl OptionView { get => LineShapeCommandOptionView.Show(this); }

		[DataMember]
		public IActionCommand ParentCommand { get; set; } = null;

		[DataMember]
		public ActionCommandCollection ChildrenCommands { get; private set; } = new ActionCommandCollection();

		public ListViewItem RecipeViewItem { get; private set; }

		[DataMember]
		public MotionOption MotionOption { get; private set; } = new MotionOption();

		public ImageSource Icon => ImageResources.Load("Icons", "LineShape");

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
			CADCanvas.MainCanvas.SizeChanged += (Shape as LineCADShape).UpdateControl;
		}

		public void OnDeselect()
		{
			Shape.OnDeselect();
		}

		public void OnRemove()
		{
			CADCanvas.RemoveShape(Shape);
			CADCanvas.MainCanvas.SizeChanged -= (Shape as LineCADShape).UpdateControl;
		}

		public void OnSelect()
		{
			Shape.OnSelect();
		}
	}
}
