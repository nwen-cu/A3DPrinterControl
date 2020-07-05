using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true), KnownType(typeof(PolygonShapeCommand))]
	public class PolygonCADShape : IBindable, ICADShape
	{
		public PolygonCADShape(PolygonShapeCommand command)
		{
			Command = command;
			UpdateControl();
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			ShapeControl = new Polygon()
			{
				Stroke = Brushes.Black,
				StrokeThickness = 2,
				StrokeLineJoin = PenLineJoin.Bevel
			};
			AuxiliaryLines = new List<AuxiliaryLine>();
		}

		private double _positionX = 0;
		[DataMember]
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
				OnPropertyChanged("PositionX");
			}
		}

		private double _positionY = 0;
		[DataMember]
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

		[DataMember]
		public ObservableCollection<ObservablePoint> Points { get; private set; } = new ObservableCollection<ObservablePoint>()
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

		public Shape ShapeControl { get; private set; } = new Polygon()
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
				vertices.Add(vertices[0]);
				return vertices;
			}
		}

		public List<AuxiliaryLine> AuxiliaryLines { get; private set; } = new List<AuxiliaryLine>();

		[DataMember]
		public IActionCommand Command { get; private set; }
	}
}
