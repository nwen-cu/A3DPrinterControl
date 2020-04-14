using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public interface ICADShape : INotifyPropertyChanged
	{
		public double PositionX { get; set; }
		public double PositionY { get; set; }

		public double Rotation { get; set; }
		public double ScaleX { get; set; }
		public double ScaleY { get; set; }
		public Shape ShapeControl { get; }
		public List<Point> Vertices { get; }
		public Canvas AuxiliaryLineContainer { get; }
		public List<AuxiliaryLine> AuxiliaryLines { get; }
		public IActionCommand Command { get; }

		public void AddAuxiliaryLine(AuxiliaryLine line);
		public void RemoveAuxiliaryLine(AuxiliaryLine line);
		public void ClearAuxiliaryLine(AuxiliaryLine line);

		public void OnSelect();
		public void OnDeselect();
		

	}
}
