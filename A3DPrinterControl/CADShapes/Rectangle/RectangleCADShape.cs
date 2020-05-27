using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Runtime.Serialization;

namespace A3DPrinterControl
{
	[DataContract(IsReference = true)]
	[KnownType(typeof(RectangleShapeCommand))]
	class RectangleCADShape : IBindable, ICADShape
	{
		public RectangleCADShape(RectangleShapeCommand cmd)
		{
			Command = cmd;
			Canvas.SetLeft(ShapeControl, 0);
			Canvas.SetBottom(ShapeControl, 0);
			Panel.SetZIndex(ShapeControl, 10);
		}

		[OnDeserializing]
		private void OnDeserializing(StreamingContext c)
		{ 
			ShapeControl= new Rectangle()
			{
				Stroke = Brushes.Black,
				StrokeThickness = 2,
				Width = 50,
				Height = 50,
				RenderTransformOrigin = new Point(0, 1),
				RenderTransform = new RotateTransform(0)
			};
			AuxiliaryLines = new List<AuxiliaryLine>();
		}

		[DataMember]
		public double PositionX
		{
			get
			{
				return Canvas.GetLeft(ShapeControl);
			}
			set
			{

				Canvas.SetLeft(ShapeControl, value);
				OnPropertyChanged("PositionX");
			}
		}

		[DataMember]
		public double PositionY
		{
			get
			{
				return Canvas.GetBottom(ShapeControl);
			}
			set
			{
				Canvas.SetBottom(ShapeControl, value);
				OnPropertyChanged("PositionY");
			}
		}

		[DataMember]
		public double Rotation 
		{
			get
			{
				return -(ShapeControl.RenderTransform as RotateTransform).Angle;
			}
			set
			{
				(ShapeControl.RenderTransform as RotateTransform).Angle = -value;
				OnPropertyChanged("Rotation");
			}
		}

		[DataMember]
		public double ScaleX
		{
			get
			{
				return ShapeControl.Width;
			}
			set
			{
				if (value < 0) return;
				ShapeControl.Width = value;
				OnPropertyChanged("ScaleX");
			}
		}

		[DataMember]
		public double ScaleY
		{
			get
			{
				return ShapeControl.Height;
			}
			set
			{
				if (value < 0) return;
				ShapeControl.Height = value;
				OnPropertyChanged("ScaleY");
			}
		}

		public Shape ShapeControl { get; private set; } = new Rectangle() 
		{ 
			Stroke = Brushes.Black, 
			StrokeThickness = 2, 
			Width = 50, 
			Height = 50,
			RenderTransformOrigin = new Point(0, 1),
			RenderTransform = new RotateTransform(0)
		};

		public List<Point> Vertices
		{
			get
			{
				List<Point> vertices = new List<Point>();

				Point origin = new Point(PositionX, PositionY);
				System.Windows.Vector refVector = new System.Windows.Vector(origin.X, origin.Y);
				vertices.Add(origin);
				vertices.Add(ShapeControl.RenderTransform.Transform(new Point(ScaleX, 0)) + refVector);
				vertices.Add(ShapeControl.RenderTransform.Transform(new Point(ScaleX, ScaleY)) + refVector);
				vertices.Add(ShapeControl.RenderTransform.Transform(new Point(0, ScaleY)) + refVector);
				vertices.Add(origin);
				return vertices;
			}
		}

		public List<AuxiliaryLine> AuxiliaryLines { get; private set; } = new List<AuxiliaryLine>();

		[DataMember]
		public IActionCommand Command { get; private set; }
	}
}
