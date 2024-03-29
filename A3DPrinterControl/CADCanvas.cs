﻿using System;
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

		public static CADCanvasBinder Binder => new CADCanvasBinder();

		public class CADCanvasBinder : IBinder
		{
			public double CanvasWidth
			{
				get => CADCanvas.CanvasWidth;
				set => CADCanvas.CanvasWidth = value;
			}

			public double CanvasHeight
			{
				get => CADCanvas.CanvasHeight;
				set => CADCanvas.CanvasHeight = value;
			}

			public double CanvasZoom
			{
				get => CADCanvas.CanvasZoom;
				set => CADCanvas.CanvasZoom = value;
			}
		}


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
				Binder.OnPropertyChanged("CanvasWidth");
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
				double oldCanvasHeight = MainCanvas.Height;
				MainCanvas.Height = value;
				/*foreach(var shape in MainCanvas.Children)
				{
					if(shape is Line line)
					{
						line.Y1 += value - oldCanvasHeight;
						line.Y2 += value - oldCanvasHeight;
					}
				}*/
				Binder.OnPropertyChanged("CanvasHeight");
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
				Binder.OnPropertyChanged("CanvasZoom");
			}
		}

		public static double ViewportOffsetX { get; set; }
		public static double ViewportOffsetY { get; set; }
		public static double ViewportWidth { get => Viewport.ActualWidth; }
		public static double ViewportHeight { get => Viewport.ActualHeight; }


		public static float ZoomRatio = 1;
		public static ScaleTransform ZoomTransform;

		public static double YConvertor(double coord)
		{
			return CanvasHeight - coord;
		}

		static CADCanvas()
		{
			Viewport = MainWindow.Instance.FindName("CanvasViewport") as ScrollViewer;
			Container = MainWindow.Instance.FindName("CanvasContainer") as Canvas;
			MainCanvas = MainWindow.Instance.FindName("MainCanvas") as Canvas;
			Axes = MainWindow.Instance.FindName("Axis") as Grid;
			BoundingBox = MainWindow.Instance.FindName("BoundingBox") as Rectangle;
			ShapesView = MainWindow.Instance.FindName("ShapesView") as ListView;
		}

		public static void InitializeCanvas()
		{
			CanvasHeight = 200;
			CanvasWidth = 300;
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
		}

		public static void RemoveShape(ICADShape shape)
		{
			if (shape == null) return;
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
