using System;
using System.Collections.Generic;
using System.Text;

namespace A3DPrinterControl
{
	public interface IShapeCommand : IActionCommand
	{
		public ICADShape Shape { get; }
	}
}
