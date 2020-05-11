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
			
		}

		public static void Compile()
		{
			TextBlock debug = MainWindow.Instance.FindName("DebugBox") as TextBlock;
			debug.Text = "";
			foreach (var shape in CADCanvas.CADShapes)
			{
				debug.Text += string.Join(", ", shape.Vertices.Select(v => $"({v.X:F2}, {v.Y:F2})")) + "\n";
			}
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
