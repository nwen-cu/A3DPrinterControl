﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public class RectangleShapeCommand : IShapeCommand
	{
		public string DescriptionName { get; set; } = "Rectangle";

		public ICADShape Shape { get; }

		public UserControl OptionView => RectangleShapeCommandOptionView.Show(this);

		public IActionCommand PreviousCommand 
		{
			get
			{
				int index = Recipe.CommandList.IndexOf(this);
				if (index < 1) return null;
				return Recipe.CommandList[index - 1];
			}
		}

		public IActionCommand NextCommand
		{
			get
			{
				int index = Recipe.CommandList.IndexOf(this);
				if (index == -1 || index + 1 == Recipe.CommandList.Count) return null;
				return Recipe.CommandList[index + 1];
			}
		}

		public IActionCommand ParentCommand => throw new NotImplementedException();

		public List<IActionCommand> ChildrenCommands { get; } = null;

		public ListViewItem RecipeViewItem { get; }

		public RectangleShapeCommand()
		{
			Shape = new RectangleCADShape(this);
			RecipeViewItem = new ListViewItem();
			RecipeViewItem.Tag = this;
			RecipeViewItem.Content = new StackPanel() { Orientation = Orientation.Horizontal };
			(RecipeViewItem.Content as StackPanel).Children.Add(new Grid() { Width = 16, Height = 16, Background = System.Windows.Media.Brushes.Blue });
			(RecipeViewItem.Content as StackPanel).Children.Add(new TextBlock() { Text = DescriptionName });
			RecipeViewItem.Selected += RecipeViewItem_Selected;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string info)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
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