using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Obsolete.Models
{
	public class CashItem : ICash
	{
		public string ValueName { get; }
		public decimal UnitValue { get; }

		/// <summary>
		/// Creates a new instance of CashItem
		/// </summary>
		/// <param name="valueName">The printable name of this instance</param>
		/// <param name="unitValue">The normalised value of this instance</param>
		public CashItem(string valueName, decimal unitValue)
		{
			ValueName = valueName;
			UnitValue = unitValue;
		}

		public int CompareTo(object obj)
		{
			if (obj == null) return 1;
			try
			{
				CashItem compareCash = obj as CashItem;

				if (compareCash.UnitValue < this.UnitValue)
				{
					return -1;
				}
				else if (compareCash.UnitValue == this.UnitValue)
				{
					return 0;
				}
				else
				{
					return 1;
				}
			}
			catch
			{
				//obj is not CashItem
				return 1;
			}
		}
	}
}
