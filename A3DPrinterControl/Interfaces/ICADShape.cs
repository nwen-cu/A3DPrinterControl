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

		public void AddAuxiliaryLine(AuxiliaryLine line)
		{
			AuxiliaryLines.Add(line);
			CADCanvas.MainCanvas.Children.Add(line.LineControl);
		}

		public void RemoveAuxiliaryLine(AuxiliaryLine line)
		{
			AuxiliaryLines.Remove(line);
			CADCanvas.MainCanvas.Children.Remove(line.LineControl);
		}

		public void ClearAuxiliaryLine()
		{
			AuxiliaryLines.ForEach(line => CADCanvas.MainCanvas.Children.Remove(line.LineControl));
			AuxiliaryLines.Clear();
		}

		public void OnDeselect()
		{
			ShapeControl.Stroke = Brushes.Black;
		}

		public void OnSelect()
		{
			ShapeControl.Stroke = Brushes.Blue;
		}


	}
}
