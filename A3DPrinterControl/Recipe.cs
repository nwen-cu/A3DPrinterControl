using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public static class Recipe
	{
		public static ActionCommandCollection CommandList = new ActionCommandCollection();
		public static ListView RecipeView;
		public static IActionCommand SelectedCommand;

		static Recipe()
		{
			RecipeView = MainWindow.Instance.FindName("RecipeView") as ListView;
		}

		public static void AddCommand(IActionCommand command)
		{
			if (command == null) return;
			CommandList.Add(command);
			RecipeView.Items.Add(command.RecipeViewItem);
			command.OnAdd();
		}

		public static void RemoveCommand(IActionCommand command)
		{
			if (command == null) return;
			if (CommandList.Contains(command))
			{
				if (SelectedCommand == command)
				{
					CommandDeselected(command);
				}
				CommandList.Remove(command);
				RecipeView.Items.Remove(command.RecipeViewItem);
				command.OnRemove();
			}
		}

		public static void ClearCommand()
		{
			MainWindow.CommandOptionContainer.Children.Clear();
			while (CommandList.Count > 0)
			{
				RecipeView.Items.Remove(CommandList[0].RecipeViewItem);
				CommandList[0].OnRemove();
				CommandList.RemoveAt(0);
			}
		}

		public static void CommandSelected(IActionCommand command)
		{
			if (command == null) return;
			MainWindow.CommandOptionContainer.Children.Clear();
			MainWindow.CommandOptionContainer.Children.Add(command.OptionView);
			if (command is IShapeCommand scommand)
			{
				scommand.Shape.OnSelect();
			}
		}

		public static void CommandDeselected(IActionCommand command)
		{
			if (command == null) return;
			if (command is IShapeCommand scommand)
			{
				scommand.Shape.OnDeselect();
			}
		}

		public static ListViewItem CreateRecipeViewItem(IActionCommand cmd, string iconImage)
		{
			ListViewItem item = new ListViewItem();

			item.Tag = cmd;

			item.Content = new StackPanel() { Orientation = Orientation.Horizontal };
			(item.Content as StackPanel).Children.Add(new Image() { Width = 16, Height = 16, Source = ImageResources.Load("Icons", iconImage) });
			TextBlock text = new TextBlock();
			text.SetBinding(TextBlock.TextProperty, "DescriptionName");
			text.DataContext = cmd;
			(item.Content as StackPanel).Children.Add(text);

			item.Selected += RecipeViewItem_Selected;
			item.ContextMenu = new ContextMenu();
			MenuItem del = new MenuItem() { Header = "Delete", Tag = cmd };
			item.ContextMenu.Items.Add(del);
			del.Click += RecipeViewItemContextMenuDelete_Click;

			return item;
		}

		private static void RecipeViewItem_Selected(object sender, RoutedEventArgs e)
		{
			SelectedCommand?.OnDeselect();
			SelectedCommand = (sender as ListViewItem).Tag as IActionCommand;
			MainWindow.Instance.CommandOptionPanel.Children.Clear();
			MainWindow.Instance.CommandOptionPanel.Children.Add(SelectedCommand.OptionView);
			SelectedCommand.OnSelect();
		}

		private static void RecipeViewItemContextMenuDelete_Click(object sender, RoutedEventArgs e)
		{
			RemoveCommand((sender as FrameworkElement).Tag as IActionCommand);
		}
	}
}
