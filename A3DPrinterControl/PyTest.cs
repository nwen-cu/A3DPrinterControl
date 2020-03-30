using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Included;
using Python.Runtime;
using System.Windows;
using System.IO;

namespace A3DPrinterControl
{
	public static class PyTest
	{
		public struct tstruct
		{
			public int item1;
			public float item2;
			public string item3;

			public tstruct(int item1, float item2, string item3)
			{
				this.item1 = item1;
				this.item2 = item2;
				this.item3 = item3 ?? throw new ArgumentNullException(nameof(item3));
			}
		}

		public static string PySetup()
		{
			Installer.SetupPython().Wait();
			Installer.InstallPip();
			Installer.PipInstallModule("numpy");

			PythonEngine.Initialize();

			PyScope scope = Py.CreateScope();


			scope.Set("scr", "sys.executable");
			string rtn;
			using (Py.GIL())
			{
				dynamic sys = scope.Import("sys");
				//dynamic np = scope.Import("numpy");
				//dynamic result = scope.Execute("eval(scr)".ToPython());
				//rtn = np.__file__.ToString();
				scope.Exec(File.ReadAllText("test.py"));
				dynamic f = scope.Eval("f");
				dynamic e = scope.Eval("e");
				//rtn = scope.Eval("f(2)").ToString();
				//dynamic npa = np.random.randn(15);
				//rtn = f(npa[10]).ToString();
				scope.Set("a", (1, 2, 4, 5, 1, 2, 3, 4).ToPython());
				rtn = e(((1, 2, 4, 5, 1, 2, 3, 4).ToPython(),
					(2, 3, 4, 5, 1, 2, 3, 4).ToPython(),
					(3, 5, 4, 5, 1, 2, 3, 4).ToPython()).ToPython()).ToString();
			}
			scope.Dispose();
			return rtn;
		}
	}
}
