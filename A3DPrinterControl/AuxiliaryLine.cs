using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	public class AuxiliaryLine
	{
		public Line LineControl { get; } = new Line()
		{ 
			StrokeThickness = 1,
			Stroke = Brushes.Green
		};

		public ICADShape ParentShape;

		public bool Visibility 
		{
			get => LineControl.Visibility == System.Windows.Visibility.Visible;
			set
			{
				LineControl.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
			}
		}

		public double StartpointXValue
		{
			get => LineControl.X1;
			set => LineControl.X1 = value;
		}

		private double startpointY;
		public double StartpointYValue
		{
			get
			{
				return startpointY;
			}
			set
			{
				startpointY = value;
				LineControl.Y1 = CADCanvas.MainCanvas.Height - value;
			}
		}

		public double EndpointXValue
		{
			get => LineControl.X2;
			set => LineControl.X2 = value;
		}

		private double endpointY;
		public double EndpointYValue
		{
			get
			{
				return endpointY;
			}
			set
			{
				endpointY = value;
				LineControl.Y2 = CADCanvas.MainCanvas.Height - value;
			}
		}

		public enum AuxiliaryLineType
		{ 
			Infill,
			Support
		}

		public AuxiliaryLineType Type { get; set; } = AuxiliaryLineType.Infill;

		public AuxiliaryLine(ICADShape parent)
		{
			ParentShape = parent;
		}

	}
}
