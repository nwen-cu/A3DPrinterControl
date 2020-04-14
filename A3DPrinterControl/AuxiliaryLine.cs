using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	public class AuxiliaryLine
	{
		public Line LineControl { get; } = new Line();

		public Canvas Container { get; }

		public bool Visibility { get; set; }

		public PositionType StartpointXType { get; set; }
		public PositionType StartpointYType { get; set; }
		public PositionType EndpointXType { get; set; }
		public PositionType EndpointYType { get; set; }

		public double StartpointXValue { get; set; }
		public double StartpointYValue { get; set; }
		public double EndpointXValue { get; set; }
		public double EndpointYValue { get; set; }

		public enum PositionType
		{ 
			Absolute,
			Relative
		}

		public enum AuxiliaryLineType
		{ 
			Infill,
			Support
		}


		public AuxiliaryLineType Type { get; set; } = AuxiliaryLineType.Infill;

		public AuxiliaryLine(Canvas container)
		{
			Container = container;
			container.Children.Add(LineControl);
		}

		public void Update()
		{ 
		
		}
	}
}
