using System;
using System.Collections.Generic;
using System.Text;

namespace A3DPrinterControl
{
	public static class GlobalSettings
	{
		private static Dictionary<string, object> settings = new Dictionary<string, object>();

		public static void LoadSettings()
		{ 
		
		}

		public static void SaveSettings()
		{ 
			
		}

		public static object GetSetting(string key)
		{
			return settings[key];
		}

		public static void SetSetting(string key, object value)
		{ 
		
		}
	}
}
