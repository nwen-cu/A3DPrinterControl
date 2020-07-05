using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace A3DPrinterControl.Helpers
{
	class PyExpressionValidationRule : ValidationRule
	{
		public string Script { get; set; }

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			PyScope scope = PyScriptManager.ScriptScopes["Temp"];
			scope.ImportAll("math");

			scope.Set("value", value);

			PyObject result = scope.Eval(Script);

			if (result.As<bool>())
			{ 
				
			}

			return new ValidationResult(default, default);
		}
	}
}
