using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	public class NumericRangeRule : ValidationRule
	{
		public double Min { get; set; }
		public double Max { get; set; }

		public bool IncludeMin { get; set; } = false;

		public bool IncludeMax { get; set; } = false;


		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			throw new NotImplementedException();
		}
	}
}
