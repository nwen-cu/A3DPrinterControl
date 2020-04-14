using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.Windows.Controls;

namespace A3DPrinterControl
{
	class AuxiliaryLineCollection : Canvas, IList<AuxiliaryLine>
	{
		private List<AuxiliaryLine> list = new List<AuxiliaryLine>();
		public AuxiliaryLine this[int index] { get => list[index]; set => list[index] = value; }

		public int Count => list.Count;

		public bool IsReadOnly => false;

		public void Add(AuxiliaryLine item)
		{
			list.Add(item);
		}

		public void Clear()
		{
			list.Clear();
		}

		public bool Contains(AuxiliaryLine item)
		{
			return list.Contains(item);
		}

		public void CopyTo(AuxiliaryLine[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<AuxiliaryLine> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public int IndexOf(AuxiliaryLine item)
		{
			return list.IndexOf(item);
		}

		public void Insert(int index, AuxiliaryLine item)
		{
			list.Insert(index, item);
		}

		public bool Remove(AuxiliaryLine item)
		{
			return list.Remove(item);
		}

		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}
	}
}
