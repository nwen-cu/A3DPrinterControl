using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace A3DPrinterControl 
{
	public class PolygonShapeCommand : IBindable, IShapeCommand
	{
		public ICADShape Shape { get; }

		private string _descriptionName = "Polygon";

		public PolygonShapeCommand()
		{
			Shape = new PolygonCADShape(this);
			RecipeViewItem = new ListViewItem();
			RecipeViewItem.Tag = this;
			RecipeViewItem.Content = new StackPanel() { Orientation = Orientation.Horizontal };
			(RecipeViewItem.Content as StackPanel).Children.Add(new Image() { Width = 16, Height = 16, Source = ImageResources.Load("Icons", "PolygonShape") });
			TextBlock text = new TextBlock();
			text.SetBinding(TextBlock.TextProperty, "DescriptionName");
			text.DataContext = this;
			(RecipeViewItem.Content as StackPanel).Children.Add(text);
			RecipeViewItem.Selected += RecipeViewItem_Selected;
		}

		private void RecipeViewItem_Selected(object sender, System.Windows.RoutedEventArgs e)
		{
			Recipe.SelectedCommand?.OnDeselect();
			Recipe.SelectedCommand = this;
			MainWindow.Instance.CommandOptionPanel.Children.Clear();
			MainWindow.Instance.CommandOptionPanel.Children.Add(OptionView);
			OnSelect();
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
		public ListViewItem RecipeViewItem { get; }

		public UserControl OptionView => PolygonShapeCommandOptionView.Show(this);

		public IActionCommand ParentCommand => throw new NotImplementedException();

		public List<IActionCommand> ChildrenCommands => null;

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
			CADCanvas.MainCanvas.SizeChanged += (Shape as PolygonCADShape).UpdateControl;
		}

		public void OnCompile()
		{
			throw new NotImplementedException();
		}

		public void OnMove()
		{
			throw new NotImplementedException();
		}

		public void OnPause()
		{
			throw new NotImplementedException();
		}

		public void OnRecipeFinish()
		{
			throw new NotImplementedException();
		}

		public void OnRecipeStart()
		{
			throw new NotImplementedException();
		}

		public void OnRecipeStop()
		{
			throw new NotImplementedException();
		}

		public void OnRemove()
		{
			CADCanvas.MainCanvas.SizeChanged -= (Shape as PolygonCADShape).UpdateControl;
		}

		public void OnReset()
		{
			throw new NotImplementedException();
		}

		public void OnRun()
		{
			throw new NotImplementedException();
		}

		public void OnStop()
		{
			throw new NotImplementedException();
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
