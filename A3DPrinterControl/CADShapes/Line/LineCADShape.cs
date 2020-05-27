using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true), KnownType(typeof(LineShapeCommand))]
	public class LineCADShape : IBindable, ICADShape
	{
		public LineCADShape(LineShapeCommand cmd)
		{
			Command = cmd;
			PositionX = 0;
			PositionY = 0;
			EndpointX = 50;
			EndpointY = 50;
			Panel.SetZIndex(ShapeControl, 10);
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			ShapeControl = new Line()
			{
				Stroke = Brushes.Black,
				StrokeThickness = 2,
				RenderTransformOrigin = new Point(0, 1),
				RenderTransform = new RotateTransform(0)
			};
			AuxiliaryLines = new List<AuxiliaryLine>();
		}

		[DataMember]
		public double PositionX
		{
			get => (ShapeControl as Line).X1;
			set
			{
				(ShapeControl as Line).X1 = value;
				OnPropertyChanged("PositionX");
				OnPropertyChanged("Rotation");
				OnPropertyChanged("Length");
			}
		}

		private double positionY;
		[DataMember]
		public double PositionY
		{
			get => positionY;
			set
			{
				positionY = value;
				(ShapeControl as Line).Y1 = CADCanvas.MainCanvas.Height - value;
				OnPropertyChanged("PositionY");
				OnPropertyChanged("Rotation");
				OnPropertyChanged("Length");
			}
		}

		[DataMember]
		public double EndpointX
		{
			get => (ShapeControl as Line).X2;
			set
			{
				(ShapeControl as Line).X2 = value;
				OnPropertyChanged("EndpointX");
				OnPropertyChanged("Rotation");
				OnPropertyChanged("Length");
			}
		}

		private double endpointY;
		[DataMember]
		public double EndpointY
		{
			get => endpointY;
			set
			{
				endpointY = value;
				(ShapeControl as Line).Y2 = CADCanvas.MainCanvas.Height - value;
				OnPropertyChanged("EndpointY");
				OnPropertyChanged("Rotation");
				OnPropertyChanged("Length");
			}
		}

		public double Rotation
		{
			get
			{
				Line line = (ShapeControl as Line);
				return -Math.Atan((line.Y2 - line.Y1) / (line.X2 - line.X1)) / Math.PI * 180;
			}
			set
			{
				double length = Length;
				EndpointX = PositionX + length * Math.Cos((Math.PI / 180) * value);
				EndpointY = PositionY + length * Math.Sin((Math.PI / 180) * value);
			}
		}

		public double Length
		{
			get
			{
				Line line = ShapeControl as Line;
				return Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2));
			}
			set
			{
				double angle = Rotation;
				EndpointX = PositionX + value * Math.Cos((Math.PI / 180) * angle);
				EndpointY = PositionY + value * Math.Sin((Math.PI / 180) * angle);
			}
		}


		public Shape ShapeControl { get; private set; } = new Line()
		{
			Stroke = Brushes.Black,
			StrokeThickness = 2,
			RenderTransformOrigin = new Point(0, 1),
			RenderTransform = new RotateTransform(0)
		};

		public List<AuxiliaryLine> AuxiliaryLines { get; private set; } = new List<AuxiliaryLine>();

		[DataMember]
		public IActionCommand Command { get; private set; }

		public List<Point> Vertices
		{
			get
			{
				List<Point> vertices = new List<Point>();
				vertices.Add(new Point(PositionX, PositionY));
				vertices.Add(new Point(EndpointX, EndpointY));
				return vertices;
			}
		}
	}
}
