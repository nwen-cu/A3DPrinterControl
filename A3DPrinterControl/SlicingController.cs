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
			PyScriptManager.ScriptScopes["Infill"].Set("shape_objects", CADCanvas.CADShapes.AsReadOnly());
			PyObject infills = PyScriptManager.ScriptScopes["Infill"].Eval("OnGenerateInfill()");
			Debug.Log("Infills:\n");
			foreach (var shape in CADCanvas.CADShapes)
			{
				shape.ClearAuxiliaryLine();
				Debug.Log($"{shape.Command.DescriptionName}:\n");
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

					Debug.Log($"({auxiliaryLine.StartpointXValue}, {auxiliaryLine.StartpointYValue}) =>\n" +
						$"({auxiliaryLine.EndpointXValue}, {auxiliaryLine.EndpointYValue})\n");

					shape.AddAuxiliaryLine(auxiliaryLine);
				}
				
			}
			
		}
	}
}
