using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	public class LineCADShape : ICADShape
	{
		public double PositionX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public double PositionY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public double Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public double ScaleX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public double ScaleY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public Shape ShapeControl => throw new NotImplementedException();

		public List<AuxiliaryLine> AuxiliaryLines => throw new NotImplementedException();

		public IActionCommand Command => throw new NotImplementedException();

		public List<Point> Vertices => throw new NotImplementedException();

		public Canvas AuxiliaryLineContainer => throw new NotImplementedException();

		public event PropertyChangedEventHandler PropertyChanged;

		public void AddAuxiliaryLine(AuxiliaryLine line)
		{
			throw new NotImplementedException();
		}

		public void ClearAuxiliaryLine(AuxiliaryLine line)
		{
			throw new NotImplementedException();
		}

		public void OnDeselect()
		{
			throw new NotImplementedException();
		}

		public void OnSelect()
		{
			throw new NotImplementedException();
		}

		public void RemoveAuxiliaryLine(AuxiliaryLine line)
		{
			throw new NotImplementedException();
		}
	}
}
