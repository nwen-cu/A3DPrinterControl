using Python.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace A3DPrinterControl
{
	public static class SlicingController
	{
		public static void GenerateInfill()
		{
			List<List<List<double>>> shapes = CADCanvas.CADShapes
				.Where(shape => shape.Command.GetType().GetInterface("IInfill") != null)
				.Select(shape => shape.Vertices.Select(vertex => new List<double>() { vertex.X, vertex.Y }).ToList()).ToList();
			List<ICADShape> shapeObjects = CADCanvas.CADShapes
				.Where(shape => shape.Command.GetType().GetInterface("IInfill") != null)
				.ToList();
			PyScriptManager.ScriptScopes["Infill"].Set("shapes", shapes); 
			PyScriptManager.ScriptScopes["Infill"].Set("shape_objects", shapeObjects);
			PyObject infills = PyScriptManager.ScriptScopes["Infill"].Eval("OnGeneratingInfill()");
			Debug.Log("Infills:\n");
			foreach (var shape in shapeObjects)
			{
				shape.ClearAuxiliaryLine();
				Debug.Log($"{shape.Command.DescriptionName}:\n");
				PyObject infill = infills[CADCanvas.CADShapes.IndexOf(shape)];

				AuxiliaryLineCollection infillCommand = shape.Command.ChildrenCommands.FirstOrDefault(cmd => cmd.DescriptionName == "#Infill") as AuxiliaryLineCollection;
				if (infillCommand == null)
				{
					infillCommand = new AuxiliaryLineCollection();
					infillCommand.DescriptionName = "#Infill";
					shape.Command.ChildrenCommands.AddCommand(infillCommand);
				}

				foreach (PyObject line in infill)
				{
					PyObject start = line[0];
					PyObject end = line[1];

					/*AuxiliaryLine auxiliaryLine = new AuxiliaryLine(shape)
					{
						Type = AuxiliaryLine.AuxiliaryLineType.Infill,

						StartpointXValue = start[0].As<double>(),
						StartpointYValue = start[1].As<double>(),
						EndpointXValue = end[0].As<double>(),
						EndpointYValue = end[1].As<double>()
					};

					Debug.Log($"({auxiliaryLine.StartpointXValue}, {auxiliaryLine.StartpointYValue}) =>\n" +
						$"({auxiliaryLine.EndpointXValue}, {auxiliaryLine.EndpointYValue})\n");

					shape.AddAuxiliaryLine(auxiliaryLine);*/

					AuxiliaryLineCommand auxiliaryLine = new AuxiliaryLineCommand();
					auxiliaryLine.DescriptionName = "Infill Line";
					(auxiliaryLine.Shape as LineCADShape).PositionX = start[0].As<double>();
					(auxiliaryLine.Shape as LineCADShape).PositionY = start[1].As<double>();
					(auxiliaryLine.Shape as LineCADShape).EndpointX = end[0].As<double>();
					(auxiliaryLine.Shape as LineCADShape).EndpointY = end[1].As<double>();
					(auxiliaryLine.Shape as LineCADShape).Color = Brushes.Green;
					(auxiliaryLine.Shape as LineCADShape).ShapeControl.StrokeThickness = 1;

					infillCommand.ChildrenCommands.AddCommand(auxiliaryLine);
					auxiliaryLine.ParentCommand = infillCommand;
				}
				
			}
			
		}
	}
}
