using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace A3DPrinterControl
{
	class RectangleCADShape : ICADShape
	{
		public double PositionX
		{
			get
			{
				return Canvas.GetLeft(ShapeControl);
			}
			set
			{

				Canvas.SetLeft(ShapeControl, value);
				Canvas.SetLeft(AuxiliaryLineContainer, value);
				OnPropertyChanged("PositionX");
			}
		}
		public double PositionY
		{
			get
			{
				return Canvas.GetBottom(ShapeControl);
			}
			set
			{
				Canvas.SetBottom(ShapeControl, value);
				Canvas.SetBottom(AuxiliaryLineContainer, value);
				OnPropertyChanged("PositionY");
			}
		}
		public double Rotation 
		{
			get
			{
				return (ShapeControl.RenderTransform as RotateTransform).Angle;
			}
			set
			{
				(ShapeControl.RenderTransform as RotateTransform).Angle = value;
				(AuxiliaryLineContainer.RenderTransform as RotateTransform).Angle = value;
				OnPropertyChanged("Rotation");
			}
		}
		public double ScaleX
		{
			get
			{
				return ShapeControl.Width;
			}
			set
			{
				ShapeControl.Width = value;
				AuxiliaryLineContainer.Width = value;
				OnPropertyChanged("ScaleX");
			}
		}
		public double ScaleY
		{
			get
			{
				return ShapeControl.Height;
			}
			set
			{
				ShapeControl.Height = value;
				AuxiliaryLineContainer.Height = value;
				OnPropertyChanged("ScaleY");
			}
		}

		public Shape ShapeControl { get; } = new Rectangle() 
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
				vertices.Add(ShapeControl.RenderTransform.Inverse.Transform(new Point(ScaleX, 0)) + refVector);
				vertices.Add(ShapeControl.RenderTransform.Inverse.Transform(new Point(ScaleX, ScaleY)) + refVector);
				vertices.Add(ShapeControl.RenderTransform.Inverse.Transform(new Point(0, ScaleY)) + refVector);
				vertices.Add(origin);
				return vertices;
			}
		}

		public Canvas AuxiliaryLineContainer { get; } = new Canvas()
		{
			Width = 50,
			Height = 50,
			RenderTransformOrigin = new Point(0, 1),
			RenderTransform = new RotateTransform(0),
			HorizontalAlignment = HorizontalAlignment.Left,
			VerticalAlignment = VerticalAlignment.Bottom
		};

		public List<AuxiliaryLine> AuxiliaryLines { get; } = new List<AuxiliaryLine>();
		public IActionCommand Command { get; }

		public RectangleCADShape(RectangleShapeCommand cmd)
		{
			Canvas.SetLeft(ShapeControl, 0);
			Canvas.SetBottom(ShapeControl, 0);
			Panel.SetZIndex(ShapeControl, 10);
			Canvas.SetLeft(AuxiliaryLineContainer, 0);
			Canvas.SetBottom(AuxiliaryLineContainer, 0);
			Command = cmd;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string info)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
		}

		public void OnDeselect()
		{
			ShapeControl.Stroke = Brushes.Black;
		}

		public void OnSelect()
		{
			ShapeControl.Stroke = Brushes.Blue;
		}

		public void AddAuxiliaryLine(AuxiliaryLine line)
		{
			throw new NotImplementedException();
		}

		public void RemoveAuxiliaryLine(AuxiliaryLine line)
		{
			throw new NotImplementedException();
		}

		public void ClearAuxiliaryLine(AuxiliaryLine line)
		{
			throw new NotImplementedException();
		}
	}
}
