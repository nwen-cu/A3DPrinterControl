using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public interface IActionCommand : INotifyPropertyChanged
	{
		public string DescriptionName { get; set; }

		public ListViewItem RecipeViewItem { get; }
		public UserControl OptionView { get; }

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
		public IActionCommand ParentCommand { get; }
		public List<IActionCommand> ChildrenCommands { get; }

		public void OnCompile() { }

		public void OnAdd() { }
		public void OnRemove() { }
		public void OnMove() { }
		public void OnSelect() { }
		public void OnDeselect() { }
		public void OnRun() { }
		public void OnPause() { }
		public void OnStop() { }
		public void OnReset() { }
		public void OnRecipeStart() { }
		public void OnRecipeStop() { }
		public void OnRecipeFinish() { }
	}
}
