using System;
using System.Collections.Generic;
using System.Text;

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
			foreach(ICADShape shape in CADCanvas.CADShapes)
			{
				//Infill
			}
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
