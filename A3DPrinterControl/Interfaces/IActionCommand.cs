using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace A3DPrinterControl
{
	public interface IActionCommand : INotifyPropertyChanged
	{
		public string DescriptionName { get; set; }

		public UserControl OptionView { get; }

		public ImageSource Icon { get; }

		public IActionCommand PreviousCommand 
		{
			get
			{
				if (ParentCommand == null)
				{
					int index = Recipe.CommandList.IndexOf(this);
					if (index < 1) return null;
					return Recipe.CommandList[index - 1];
				}
				else
				{
					int index = ParentCommand.ChildrenCommands.IndexOf(this);
					if (index == 0) return ParentCommand;
					if (index < 1) return null;
					return ParentCommand.ChildrenCommands[index - 1];
				}
			}
		}
		public IActionCommand NextCommand
		{
			get
			{
				if (ParentCommand == null)
				{
					int index = Recipe.CommandList.IndexOf(this);
					if (index == -1 || index + 1 == Recipe.CommandList.Count) return null;
					return Recipe.CommandList[index + 1];
				}
				else
				{
					int index = ParentCommand.ChildrenCommands.IndexOf(this);
					if (index + 1 == Recipe.CommandList.Count)
					{
						if (ChildrenCommands.Count > 0)
						{
							return ChildrenCommands[0];
						}
						else
						{
							return ParentCommand.NextCommand;
						}
					}
					if (index == -1) return null;
					return ParentCommand.ChildrenCommands[index + 1];
				}
			}
		}
		public IActionCommand ParentCommand { get; set; }
		public ActionCommandCollection ChildrenCommands { get; }

		public void OnCompile() => ChildrenCommands.ForEach(command => command.OnCompile());

		public void OnAdd() { }
		public void OnRemove() => ChildrenCommands.ForEach(command => command.OnRemove());
		public void OnMove() { }
		public void OnSelect() { }
		public void OnDeselect() { }
		public void OnRun() => ChildrenCommands.ForEach(command => command.OnRun());
		public void OnPause() => ChildrenCommands.ForEach(command => command.OnPause());
		public void OnStop() => ChildrenCommands.ForEach(command => command.OnStop());
		public void OnReset() => ChildrenCommands.ForEach(command => command.OnReset());
		public void OnRecipeStart() => ChildrenCommands.ForEach(command => command.OnRecipeStart());
		public void OnRecipeStop() => ChildrenCommands.ForEach(command => command.OnRecipeStop());
		public void OnRecipeFinish() => ChildrenCommands.ForEach(command => command.OnRecipeFinish());
	}
}
