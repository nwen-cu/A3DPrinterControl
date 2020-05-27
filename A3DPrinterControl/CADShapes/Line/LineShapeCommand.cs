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
			RecipeViewItem = Recipe.CreateRecipeViewItem(this, "LineShape");
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			RecipeViewItem = Recipe.CreateRecipeViewItem(this, "LineShape");
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
		public IActionCommand ParentCommand { get; private set; } = null;

		[DataMember]
		public List<IActionCommand> ChildrenCommands { get; private set; } = null;

		public ListViewItem RecipeViewItem { get; private set; }
		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
		}

		public void OnCompile()
		{
			
		}

		public void OnDeselect()
		{
			Shape.OnDeselect();
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
		}

		public void OnReset()
		{
			
		}

		public void OnRun()
		{
			
		}

		public void OnSelect()
		{
			Shape.OnSelect();
		}

		public void OnStop()
		{
			
		}
	}
}
