using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace A3DPrinterControl
{
	public static class CADCanvas
	{
		public static ScrollViewer Viewport;
		public static Canvas Container;
		public static Canvas MainCanvas;
		public static Grid Axes;
		public static Rectangle BoundingBox;

		public static List<ICADShape> CADShapes = new List<ICADShape>();
		public static ListView ShapesView;
		

		public static double BorderWidth = 10;

		public static double CanvasWidth
		{
			get
			{
				return Container.Width - BorderWidth * 2;
			}
			set
			{
				Container.Width = value + BorderWidth * 2;
				BoundingBox.Width = value;
				MainCanvas.Width = value;
			}
		}
		public static double CanvasHeight
		{
			get
			{
				return Container.Height - BorderWidth * 2;
			}
			set
			{
				Container.Height = value + BorderWidth * 2;
				BoundingBox.Height = value;
				MainCanvas.Height = value;
			}
		}
		public static double CanvasZoom
		{
			get 
			{
				return (Container.RenderTransform as ScaleTransform).ScaleX;
			}
			set
			{
				Container.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
				Container.RenderTransform = new ScaleTransform(value, value);
			}
		}

		public static double ViewportOffsetX { get; set; }
		public static double ViewportOffsetY { get; set; }
		public static double ViewportWidth { get => Viewport.ActualWidth; }
		public static double ViewportHeight { get => Viewport.ActualHeight; }


		public static float ZoomRatio = 1;
		public static System.Windows.Media.ScaleTransform ZoomTransform;

		static CADCanvas()
		{
			Viewport = MainWindow.Instance.FindName("CanvasViewport") as ScrollViewer;
			Container = MainWindow.Instance.FindName("CanvasContainer") as Canvas;
			MainCanvas = MainWindow.Instance.FindName("MainCanvas") as Canvas;
			Axes = MainWindow.Instance.FindName("Axis") as Grid;
			BoundingBox = MainWindow.Instance.FindName("BoundingBox") as Rectangle;
			ShapesView = MainWindow.Instance.FindName("ShapesView") as ListView;
		}

		public static void ShowAxes(bool toggle)
		{
			if (toggle)
			{
				Axes.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				Axes.Visibility = System.Windows.Visibility.Hidden;
			}
		}

		public static void AddShape(ICADShape shape)
		{
			if (shape == null) return;
			CADShapes.Add(shape);
			MainCanvas.Children.Add(shape.ShapeControl);
			MainCanvas.Children.Add(shape.AuxiliaryLineContainer);
		}

		public static void RemoveShape(ICADShape shape)
		{
			if (shape == null) return;
			MainCanvas.Children.Remove(shape.AuxiliaryLineContainer);
			MainCanvas.Children.Remove(shape.ShapeControl);
			CADShapes.Remove(shape);
		}

		public static void UpdateShape(ICADShape shape)
		{
			//shape.ShapeControl.Width;
			//shape.ShapeControl.Height;
			//shape.ShapeControl.Margin;
		}
	}
}
