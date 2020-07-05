using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public static class MainController
	{
		public static bool IsInitialized { get; set; }
		public static bool IsCompiled { get; set; }

		public static void Initialize()
		{
			PyScriptManager.InitializeScriptEngine();
			PyScriptManager.LoadModule();
			PyScriptManager.ResetTempScope();
			PyScriptManager.LoadScript("Infill", "Infill.py");
		}

		public static void Compile()
		{
			Debug.Log("");
			Debug.Log("Vertices:");
			foreach (var shape in CADCanvas.CADShapes)
			{
				Debug.Log($"{shape.Command.DescriptionName}:\n" + string.Join(",\n", shape.Vertices.Select(v => $"({v.X:F2}, {v.Y:F2})")) + "\n");
			}

			Recipe.CommandList.ForEach(cmd => cmd.OnCompile());

			SlicingController.GenerateInfill();
		}

		public static void Run()
		{

		}

		public static void Pause()
		{ 
		
		}

		public static void Stop()
		{ 
		
		}

		public static void Load()
		{ 
		
		}

		public static void Save()
		{ 
		
		}
	}
}
