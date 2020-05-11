using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public class EllipseArcShapeCommand : IBindable, IShapeCommand
	{
		private string _descriptionName = "Ellipse Arc";

		public EllipseArcShapeCommand()
		{
			Shape = new EllipseArcCADShape(this);
			RecipeViewItem = new ListViewItem();
			RecipeViewItem.Tag = this;
			RecipeViewItem.Content = new StackPanel() { Orientation = Orientation.Horizontal };
			(RecipeViewItem.Content as StackPanel).Children.Add(new Image() { Width = 16, Height = 16, Source = ImageResources.Load("Icons", "EllipseArcShape") });
			TextBlock text = new TextBlock();
			text.SetBinding(TextBlock.TextProperty, "DescriptionName");
			text.DataContext = this;
			(RecipeViewItem.Content as StackPanel).Children.Add(text);
			RecipeViewItem.Selected += RecipeViewItem_Selected;
		}

		public ICADShape Shape { get; }

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

		public UserControl OptionView => EllipseArcShapeCommandOptionView.Show(this);

		public IActionCommand ParentCommand => throw new NotImplementedException();

		public List<IActionCommand> ChildrenCommands => null;

		public void OnCompile()
		{
		}

		public void OnAdd()
		{
			CADCanvas.AddShape(Shape);
			CADCanvas.MainCanvas.SizeChanged += (Shape as EllipseArcCADShape).UpdateControl;
		}

		public void OnRemove()
		{
			CADCanvas.MainCanvas.SizeChanged -= (Shape as EllipseArcCADShape).UpdateControl;
		}

		public void OnMove()
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

		public void OnRun()
		{
		}

		public void OnPause()
		{
		}

		public void OnStop()
		{
		}

		public void OnReset()
		{
		}

		public void OnRecipeStart()
		{
		}

		public void OnRecipeStop()
		{
		}

		public void OnRecipeFinish()
		{
		}

		private void RecipeViewItem_Selected(object sender, System.Windows.RoutedEventArgs e)
		{
			Recipe.SelectedCommand?.OnDeselect();
			Recipe.SelectedCommand = this;
			MainWindow.Instance.CommandOptionPanel.Children.Clear();
			MainWindow.Instance.CommandOptionPanel.Children.Add(OptionView);
			OnSelect();
		}
	}
}