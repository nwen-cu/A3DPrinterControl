using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Python.Runtime;

namespace A3DPrinterControl
{
	public class PyScriptManager
	{
		public static Dictionary<string, PyScope> ScriptScopes = new Dictionary<string, PyScope>();

		public static void InitializeScriptEngine()
		{
			Python.Included.Installer.SetupPython().Wait();
			Python.Included.Installer.InstallPip();
			PythonEngine.Initialize();
		}

		public static void LoadScript(string name, string path)
		{
			PyScope scriptScope = Py.CreateScope();
			scriptScope.Exec(File.ReadAllText(path));
			if (ScriptScopes.ContainsKey(name))
			{
				ScriptScopes[name].Dispose();
				ScriptScopes.Remove(name);
			}
			ScriptScopes.Add(name, scriptScope);
		}
	}
}
