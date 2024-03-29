﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true), KnownType(typeof(EllipseArcShapeCommand))]
	public class EllipseArcCADShape : IBindable, ICADShape
	{
		private double _centerX;

		private double _centerY;

		private double _axisX = 50;

		private double _axisY = 25;

		private double _startAngle = 0;

		private double _endAngle = 360;

		private double _rotation = 0;

		private double _sampleRate = 10;

		private List<Point> points = new List<Point>();

		public EllipseArcCADShape(EllipseArcShapeCommand cmd)
		{
			Command = cmd;
			UpdateEllipse();
			UpdateControl();
		}

		private Brush _color = Brushes.Black;
		[DataMember]
		public Brush Color
		{
			get => _color;
			set
			{
				_color = value;
				ShapeControl.Stroke = value;
			}
		}

		[DataMember]
		public double PositionX
		{
			get
			{
				return _centerX;
			}
			set
			{
				_centerX = value;
				OnPropertyChanged("PositionX");
				UpdateControl();
			}
		}

		[DataMember]
		public double PositionY
		{
			get
			{
				return _centerY;
			}
			set
			{
				_centerY = value;
				OnPropertyChanged("PositionY");
				UpdateControl();
			}
		}

		[DataMember]
		public double AxisX
		{
			get
			{
				return _axisX;
			}
			set
			{
				_axisX = value;
				OnPropertyChanged("AxisX");
				UpdateEllipse();
				UpdateControl();
			}
		}

		[DataMember]
		public double AxisY
		{
			get
			{
				return _axisY;
			}
			set
			{
				_axisY = value;
				OnPropertyChanged("AxisY");
				UpdateEllipse();
				UpdateControl();
			}
		}

		[DataMember]
		public double StartAngle
		{
			get
			{
				return _startAngle;
			}
			set
			{
				_startAngle = value;
				OnPropertyChanged("StartAngle");
				UpdateEllipse();
				UpdateControl();
			}
		}

		[DataMember]
		public double EndAngle
		{
			get
			{
				return _endAngle;
			}
			set
			{
				_endAngle = value;
				OnPropertyChanged("EndAngle");
				UpdateEllipse();
				UpdateControl();
			}
		}

		[DataMember]
		public double Rotation
		{
			get
			{
				return _rotation;
			}
			set
			{
				_rotation = value;
				OnPropertyChanged("Rotation");
				UpdateEllipse();
				UpdateControl();
			}
		}

		[DataMember]
		public double SampleRate
		{
			get
			{
				return _sampleRate;
			}
			set
			{
				if (value <= 0) return;
				_sampleRate = value;
				OnPropertyChanged("SampleRate");
				UpdateEllipse();
				UpdateControl();
			}
		}

		public Shape ShapeControl { get; private set; } = new Polyline()
		{
			Stroke = Brushes.Black,
			StrokeThickness = 2
		};

		public List<Point> Vertices
		{
			get
			{
				List<Point> vertices = new List<Point>();

				foreach (Point point in points)
				{
					double x = PositionX + point.X;
					double y = PositionY + point.Y;
					vertices.Add(new Point(x, y));
				}

				if (EndAngle - StartAngle == 360) vertices.Add(vertices.First());

				return vertices;
			}
		}

		public List<AuxiliaryLine> AuxiliaryLines { get; private set; } = new List<AuxiliaryLine>();

		public IActionCommand Command { get; private set; }

		public void UpdateEllipse()
		{
			//Update axes, angles, rotation.
			if (SampleRate == 0) return;
			points.Clear();
			double angle = Rotation * Math.PI / 180;

			for (double theta = StartAngle; theta <= EndAngle; theta += SampleRate)
			{
				double t = theta * Math.PI / 180;

				double x = AxisX / 2 * Math.Cos(t);
				double y = AxisY / 2 * Math.Sin(t);
				double xr = x * Math.Cos(angle) - y * Math.Sin(angle);
				double yr = x * Math.Sin(angle) + y * Math.Cos(angle);
				points.Add(new Point(xr, yr));
			}
		}

		public void UpdateControl(object sender = null, SizeChangedEventArgs e = null)
		{
			//Update position
			var polygon = ShapeControl as Polyline;
			polygon.Points.Clear();
			foreach (Point p in points)
			{
				double x = p.X + PositionX;
				double y = CADCanvas.MainCanvas.Height - (p.Y + PositionY);
				polygon.Points.Add(new Point(x, y));
			}
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{
			ShapeControl = new Polyline()
			{
				Stroke = Brushes.Black,
				StrokeThickness = 2
			};
			points = new List<Point>();
			AuxiliaryLines = new List<AuxiliaryLine>();
		}
	}
}
