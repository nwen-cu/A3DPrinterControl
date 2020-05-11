using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	public class LineShapeCommand : IBindable, IShapeCommand
	{
		private string _descriptionName = "Line";
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
		public ICADShape Shape { get; }

		public UserControl OptionView { get => LineShapeCommandOptionView.Show(this); }

		public IActionCommand ParentCommand => throw new NotImplementedException();

		public List<IActionCommand> ChildrenCommands => null;

		public ListViewItem RecipeViewItem { get; }

		public LineShapeCommand()
		{
			Shape = new LineCADShape(this);
			RecipeViewItem = new ListViewItem();
			RecipeViewItem.Tag = this;
			RecipeViewItem.Content = new StackPanel() { Orientation = Orientation.Horizontal };
			(RecipeViewItem.Content as StackPanel).Children.Add(new Image() { Width = 16, Height = 16, Source = ImageResources.Load("Icons", "LineShape")});
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

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
		}

		public void OnCompile()
		{
			throw new NotImplementedException();
		}

		public void OnDeselect()
		{
			Shape.OnDeselect();
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
			throw new NotImplementedException();
		}

		public void OnReset()
		{
			throw new NotImplementedException();
		}

		public void OnRun()
		{
			throw new NotImplementedException();
		}

		public void OnSelect()
		{
			Shape.OnSelect();
		}

		public void OnStop()
		{
			throw new NotImplementedException();
		}
	}
}
