using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public static class Recipe
	{
		public static List<IActionCommand> CommandList = new List<IActionCommand>();
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
				CommandList.Remove(command);
				RecipeView.Items.Remove(command.RecipeViewItem);
				command.OnRemove();
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
	}
}
