using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	public static class SlicingController
	{
		public static void GenerateInfill()
		{
			List<List<List<double>>> shapes = CADCanvas.CADShapes.Select(shape => shape.Vertices.Select(vertex => new List<double>() { vertex.X, vertex.Y }).ToList()).ToList();
			PyScriptManager.ScriptScopes["Infill"].Set("shapes", shapes);
			PyObject infills = PyScriptManager.ScriptScopes["Infill"].Eval("OnGenerateInfill()");
			foreach (var shape in CADCanvas.CADShapes)
			{
				PyObject infill = infills[CADCanvas.CADShapes.IndexOf(shape)];
				foreach (PyObject line in infill)
				{
					PyObject start = line[0];
					PyObject end = line[1];

					AuxiliaryLine auxiliaryLine = new AuxiliaryLine(shape)
					{
						Type = AuxiliaryLine.AuxiliaryLineType.Infill,

						StartpointXValue = start[0].As<double>(),
						StartpointYValue = start[1].As<double>(),
						EndpointXValue = end[0].As<double>(),
						EndpointYValue = end[1].As<double>()
					};

					shape.AddAuxiliaryLine(auxiliaryLine);
				}
				
			}
			
		}
	}
}
