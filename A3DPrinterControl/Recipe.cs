using A3DPrinterControl.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public static class Recipe
	{
		private static UserControl DefaultOptionView = new UserControl();

		public static ActionCommandCollection _commandList = new ActionCommandCollection();
		public static ActionCommandCollection CommandList
		{
			get => _commandList;
			set
			{
				_commandList = value;
				Binder.OnPropertyChanged("CommandList");
			}
		}
		public static TreeView RecipeView { get; set; }

		private static IActionCommand _selectedCommand;
		public static IActionCommand SelectedCommand
		{
			get => _selectedCommand;
			set
			{
				_selectedCommand = value;
				Binder.OnPropertyChanged("SelectedCommand");
			}
		}

		public static RecipeBinder Binder => new RecipeBinder();

		public class RecipeBinder : IBinder
		{
			public ActionCommandCollection CommandList
			{
				get => Recipe.CommandList;
				set => Recipe.CommandList = value;
			}

			public static IActionCommand SelectedCommand
			{
				get => Recipe.SelectedCommand;
				set => Recipe.SelectedCommand = value;
			}
		}

		static Recipe()
		{
			RecipeView = MainWindow.Instance.FindName("RecipeView") as TreeView;
			DefaultOptionView.Content = new TextBlock() { Text = "No option available for this command.", TextWrapping = TextWrapping.Wrap };
		}

		public static void AddCommand(IActionCommand command)
		{
			if (command == null) return;
			CommandList.Add(command);
			//RecipeView.Items.Add(command.RecipeViewItem);
			command.OnAdd();
		}

		public static void RemoveCommand(IActionCommand command)
		{
			if (command == null) return;
			if (SelectedCommand == command)
			{
				CommandDeselected(command);
			}
			command.ChildrenCommands.ForEach(cmd => RemoveCommand(cmd));
			command.OnRemove();
			if (command.ParentCommand == null)
			{
				CommandList.Remove(command);
			}
			else
			{
				command.ParentCommand.ChildrenCommands.Remove(command);
			}
		}

		public static void ClearCommand(ActionCommandCollection list)
		{
			MainWindow.CommandOptionContainer.Children.Clear();
			while (list.Count > 0)
			{
				RemoveCommand(list[0]);
			}
		}

		public static void CommandSelected(IActionCommand command)
		{
			if (command == null) return;
			if (SelectedCommand != null) CommandDeselected(SelectedCommand);
			SelectedCommand = command;
			MainWindow.CommandOptionContainer.Children.Clear();
			MainWindow.CommandOptionContainer.Children.Add(command.OptionView ?? DefaultOptionView);
			if (command.GetType().GetInterface("IMotion") != null)
			{
				MainWindow.MotionOptionContainer.Children.Clear();
				MainWindow.MotionOptionContainer.Children.Add((command as IMotion)?.MotionOption?.OptionView ?? DefaultOptionView);
				MainWindow.MotionOptionTabpage.IsEnabled = true;
			}
			else
			{
				MainWindow.OptionTabContainer.SelectedIndex = 0;
				MainWindow.MotionOptionTabpage.IsEnabled = false;
			}
			if (command.GetType().GetInterface("IInfill") != null)
			{
				MainWindow.InfillOptionContainer.Children.Clear();
				MainWindow.InfillOptionContainer.Children.Add((command as IInfill)?.InfillOption?.OptionView ?? DefaultOptionView);
				MainWindow.InfillOptionTabpage.IsEnabled = true;
			}
			else
			{
				MainWindow.OptionTabContainer.SelectedIndex = 0;
				MainWindow.InfillOptionTabpage.IsEnabled = false;
			}
			command.OnSelect();
			if (command is IShapeCommand scommand)
			{
				scommand.Shape.OnSelect();
			}
		}

		public static void CommandDeselected(IActionCommand command)
		{
			if (command == null) return;
			command.OnDeselect();
			if (command is IShapeCommand scommand)
			{
				scommand.Shape.OnDeselect();
			}
			SelectedCommand = null;
		}

		public static void CommandMoveUp(IActionCommand command)
		{
			if (command.GetType().GetInterface("IManagedCommand") != null) return;
			if (command.ParentCommand == null)
			{
				int index = CommandList.IndexOf(command);
				if (index < 1) return;
				CommandList.Remove(command);
				CommandList.Insert(index - 1, command);
				(RecipeView.ItemContainerGenerator.ContainerFromItem(command) as TreeViewItem).IsSelected = true;
			}
			else
			{
				int index = command.ParentCommand.ChildrenCommands.IndexOf(command);
				if (index < 1) return;
				command.ParentCommand.ChildrenCommands.Remove(command);
				command.ParentCommand.ChildrenCommands.Insert(index - 1, command);
			}
		}

		public static void CommandMoveDown(IActionCommand command)
		{
			if (command.GetType().GetInterface("IManagedCommand") != null) return;
			if (command.ParentCommand == null)
			{
				int index = CommandList.IndexOf(command);
				if (index == CommandList.Count - 1) return;
				CommandList.Remove(command);
				CommandList.Insert(index + 1, command);
				(RecipeView.ItemContainerGenerator.ContainerFromItem(command) as TreeViewItem).IsSelected = true;
			}
			else
			{
				int index = command.ParentCommand.ChildrenCommands.IndexOf(command);
				if (index == command.ParentCommand.ChildrenCommands.Count - 1) return;
				command.ParentCommand.ChildrenCommands.Remove(command);
				command.ParentCommand.ChildrenCommands.Insert(index + 1, command);
			}
		}

		public static void CommandLevelUp(IActionCommand command)
		{
			if (command.GetType().GetInterface("IManagedCommand") != null) return;
			if (command.ParentCommand == null) return;
			else if (command.ParentCommand.ParentCommand == null)
			{
				int index = CommandList.IndexOf(command.ParentCommand);
				command.ParentCommand.ChildrenCommands.Remove(command);
				CommandList.Insert(index + 1, command);
				command.ParentCommand = null;
			}
			else
			{
				int index = command.ParentCommand.ParentCommand.ChildrenCommands.IndexOf(command.ParentCommand);
				command.ParentCommand.ChildrenCommands.Remove(command);
				command.ParentCommand.ParentCommand.ChildrenCommands.Insert(index + 1, command);
				command.ParentCommand = command.ParentCommand.ParentCommand;
			}
			FindRecipeViewItem(command).IsSelected = true;
		}

		public static void CommandLevelDown(IActionCommand command)
		{
			if (command.GetType().GetInterface("IManagedCommand") != null) return;
			if (command.ParentCommand == command.PreviousCommand) return;
			
			IActionCommand parent = command.PreviousCommand;

			while (parent.ParentCommand != command.ParentCommand)
			{
				parent = parent.PreviousCommand;
			}
			if (command.ParentCommand == null)
			{
				CommandList.Remove(command);
			}
			else
			{
				command.ParentCommand.ChildrenCommands.Remove(command);
			}
			command.ParentCommand = parent;
			parent.ChildrenCommands.Add(command);

			parent = command.ParentCommand;
			while (parent != null)
			{
				if (RecipeView.ItemContainerGenerator.ContainerFromItem(parent) is TreeViewItem item)
				{
					item.IsExpanded = true;
				}
				parent = parent.ParentCommand;
			}
			FindRecipeViewItem(command).IsSelected = true;
		}

		public static TreeViewItem FindRecipeViewItem(IActionCommand command)
		{
			return TreeViewHelper.FindTreeViewItem(RecipeView, command);
		}
	}
}
