using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace A3DPrinterControl
{
	[KnownType("IActionCommandTypes")]
	public class ActionCommandCollection : List<IActionCommand>
	{
		public static IEnumerable<Type> IActionCommandTypes()
		{
			return Assembly
				.GetAssembly(typeof(ActionCommandCollection))
				.GetTypes()
				.Where(type => type.IsClass)
				.Where(type => type.GetInterface("IActionCommand") != null)
				.Distinct();
		}
	}
}
