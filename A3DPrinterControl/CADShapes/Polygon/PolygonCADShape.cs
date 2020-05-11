using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	public class PolygonCADShape : IBindable, ICADShape
	{
		public PolygonCADShape(IActionCommand command)
		{
			Command = command;
			UpdateControl();
		}

		private double _positionX = 0;
		public double PositionX 
		{
			get
			{
				return _positionX;
			}
			set
			{
				_positionX = value;
				UpdateControl();
				OnPropertyChanged("PositionY");
			}
		}

		private double _positionY = 0;
		public double PositionY
		{
			get
			{
				return _positionY;
			}
			set
			{
				_positionY = value;
				UpdateControl();
				OnPropertyChanged("PositionY");
			}
		}

		public ObservableCollection<ObservablePoint> Points { get; } = new ObservableCollection<ObservablePoint>()
		{
			new ObservablePoint(0, 0),
			new ObservablePoint(40, 20),
			new ObservablePoint(20, 40)
		};

		public void UpdateControl(object sender = null, SizeChangedEventArgs e = null)
		{
			var polygon = ShapeControl as Polygon;
			polygon.Points.Clear();
			foreach (ObservablePoint p in Points)
			{
				double x = p.X + PositionX;
				double y = CADCanvas.MainCanvas.Height - (p.Y + PositionY);
				polygon.Points.Add(new Point(x, y));
			}
		}

		public Shape ShapeControl { get; } = new Polygon()
		{
			Stroke = Brushes.Black,
			StrokeThickness = 2,
			StrokeLineJoin = PenLineJoin.Bevel
		};

		public List<Point> Vertices
		{
			get
			{
				List<Point> vertices = new List<Point>();

				foreach (ObservablePoint point in Points)
				{
					vertices.Add(new Point(PositionX + point.X, PositionY + point.Y));
				}

				return vertices;
			}
		}

		public List<AuxiliaryLine> AuxiliaryLines { get; } = new List<AuxiliaryLine>();

		public IActionCommand Command { get; }
	}
}
