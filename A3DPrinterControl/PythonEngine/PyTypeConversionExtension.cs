using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace A3DPrinterControl
{
	public static class PyTypeConversionExtension
	{
		public static PyTuple ToPython<T1>(this Tuple<T1> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython() });
		}
		public static PyTuple ToPython<T1, T2>(this Tuple<T1, T2> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython() });
		}
		public static PyTuple ToPython<T1, T2, T3>(this Tuple<T1, T2, T3> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython(),
												tuple.Item6.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython(),
												tuple.Item6.ToPython(),
												tuple.Item7.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5, T6, T7, TRest>(this Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple)
		{
			if (tuple == null) throw new ArgumentNullException(nameof(tuple));
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython(),
												tuple.Item6.ToPython(),
												tuple.Item7.ToPython(),
												tuple.Rest.ToPython()});
		}
		public static PyTuple ToPython<T1>(this ValueTuple<T1> tuple)
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython() });
		}
		public static PyTuple ToPython<T1, T2>(this ValueTuple<T1, T2> tuple)
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython() });
		}
		public static PyTuple ToPython<T1, T2, T3>(this ValueTuple<T1, T2, T3> tuple)
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4>(this ValueTuple<T1, T2, T3, T4> tuple)
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2, T3, T4, T5> tuple)
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5, T6>(this ValueTuple<T1, T2, T3, T4, T5, T6> tuple)
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython(),
												tuple.Item6.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5, T6, T7>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7> tuple)
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython(),
												tuple.Item6.ToPython(),
												tuple.Item7.ToPython()});
		}
		public static PyTuple ToPython<T1, T2, T3, T4, T5, T6, T7, TRest>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple) where TRest : struct
		{
			return new PyTuple(new PyObject[] { tuple.Item1.ToPython(),
												tuple.Item2.ToPython(),
												tuple.Item3.ToPython(),
												tuple.Item4.ToPython(),
												tuple.Item5.ToPython(),
												tuple.Item6.ToPython(),
												tuple.Item7.ToPython(),
												tuple.Rest.ToPython()});
		}
	}
}
