using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace A3DPrinterControl
{
	public class ObservablePoint : IBindable
	{
		private double _x;
		public double X 
		{
			get => _x;
			set
			{
				_x = value;
				OnPropertyChanged("X");
			}
		}

		private double _y;
		public double Y
		{
			get => _y;
			set
			{
				_y = value;
				OnPropertyChanged("Y");
			}
		}

		public ObservablePoint()
		{
			_x = 0;
			_y = 0;
		}

		public ObservablePoint(double x, double y)
		{
			_x = x;
			_y = y;
		}
		public ObservablePoint(Point point)
		{
			_x = point.X;
			_y = point.Y;
		}
	}
}
