using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace A3DPrinterControl
{
	public interface ICADShape : INotifyPropertyChanged
	{
		public double PositionX { get; set; }
		public double PositionY { get; set; }
		public Shape ShapeControl { get; }
		public List<Point> Vertices { get; }
		public List<AuxiliaryLine> AuxiliaryLines { get; }
		public IActionCommand Command { get; }

		public Brush Color { get; set; }

		public void Highlight(bool toggle = true)
		{
			ShapeControl.Stroke = toggle ? Brushes.Blue : Color;
		}

		public void ClearAuxiliaryLine()
		{
			AuxiliaryLines.ForEach(line => CADCanvas.MainCanvas.Children.Remove(line.LineControl));
			AuxiliaryLines.Clear();
		}

		public void OnDeselect()
		{
			Highlight(false);
		}

		public void OnSelect()
		{
			Highlight();
		}


	}
}
