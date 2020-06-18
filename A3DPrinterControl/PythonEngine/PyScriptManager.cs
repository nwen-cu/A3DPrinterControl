using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Python.Runtime;
using System.Windows;

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

		public static void LoadModule()
		{
			LoadScript("Init", "Init.py");
			string[] modules = ScriptScopes["Init"].Eval("OnLoadModule()").As<string[]>();
			foreach(string m in modules)
			{
				Python.Included.Installer.PipInstallModule(m);
			}
		}

		public static void LoadScript(string name, string file)
		{
			PyScope scriptScope = Py.CreateScope();
			scriptScope.Exec(File.ReadAllText(Path.Combine("PyScripts", file)));
			if (ScriptScopes.ContainsKey(name))
			{
				ScriptScopes[name].Dispose();
				ScriptScopes.Remove(name);
			}
			ScriptScopes.Add(name, scriptScope);
			scriptScope.Set("debuglog", (Action<string>)Debug.Log);
			scriptScope.Set("debugmsgbox", (Action<string>)Debug.MsgBox);
		}
	}
}
