using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;

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
			RecipeViewItem = Recipe.CreateRecipeViewItem(this, "PolygonShape");
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			RecipeViewItem = Recipe.CreateRecipeViewItem(this, "PolygonShape");
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
		public IActionCommand ParentCommand { get; private set; } = null;

		[DataMember]
		public List<IActionCommand> ChildrenCommands { get; private set; } = null;

		[DataMember]
		public MotionOption MotionOption { get; private set; } = new MotionOption();

		[DataMember]
		public InfillOption InfillOption { get; private set; } = new InfillOption();

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
			CADCanvas.MainCanvas.SizeChanged += (Shape as PolygonCADShape).UpdateControl;
		}

		public void OnCompile()
		{
			
		}

		public void OnMove()
		{
			
		}

		public void OnPause()
		{
			
		}

		public void OnRecipeFinish()
		{
			
		}

		public void OnRecipeStart()
		{
			
		}

		public void OnRecipeStop()
		{
			
		}

		public void OnRemove()
		{
			CADCanvas.RemoveShape(Shape);
			CADCanvas.MainCanvas.SizeChanged -= (Shape as PolygonCADShape).UpdateControl;
		}

		public void OnReset()
		{
			
		}

		public void OnRun()
		{
			
		}

		public void OnStop()
		{
			
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
