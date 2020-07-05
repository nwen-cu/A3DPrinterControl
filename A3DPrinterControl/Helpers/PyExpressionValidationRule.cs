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
			string errorMsg;

			try
			{
				errorMsg = result.As<string>();	//Expect true(bool) for valid input, an error message string for invalid input 
			}
			catch (InvalidCastException)
			{

				return new ValidationResult(true, null);
			}


			return new ValidationResult(false, errorMsg);
		}
	}
}
