using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace A3DPrinterControl.ActionCommands
{
	public class LineShapeCommand : IShapeCommand
	{
		public string DescriptionName { get; set; } = "Line";
		public ICADShape Shape { get; } = new LineCADShape();

		public UserControl OptionView { get; }

		public IActionCommand PreviousCommand => throw new NotImplementedException();

		public IActionCommand NextCommand => throw new NotImplementedException();

		public IActionCommand ParentCommand => throw new NotImplementedException();

		public List<IActionCommand> ChildrenCommands => throw new NotImplementedException();

		public ListViewItem RecipeViewItem => throw new NotImplementedException();

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnAdd()
		{
			throw new NotImplementedException();
		}

		public void OnCompile()
		{
			throw new NotImplementedException();
		}

		public void OnDeselect()
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

		public void OnSelect()
		{
			throw new NotImplementedException();
		}

		public void OnStop()
		{
			throw new NotImplementedException();
		}
	}
}
