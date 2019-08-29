using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	public class CashSet : ICashSet
	{
		public SortedDictionary<ICash, int> CashStack { get; private set; }

		/// <summary>
		/// Creates an empty cashset
		/// </summary>
		public CashSet()
		{
			CashStack = new SortedDictionary<ICash, int>();
		}

		/// <summary>
		/// Creates a new cashset with a pre-built dictionary
		/// </summary>
		/// <param name="initialSet">The dictionary to store</param>
		public CashSet(SortedDictionary<ICash, int> initialSet)
		{
			CashStack = initialSet ?? new SortedDictionary<ICash, int>();
		}

		/// <summary>
		/// Adds a new denomination of money to this drawer
		/// </summary>
		/// <param name="denomination">The type of cash to add</param>
		/// <param name="amount">(optional) The initial amount of this item to add (default: 0)</param>
		public void Add(ICash denomination, int amount = 0)
		{
			Edit(denomination, amount);
		}

		public void Add(ICashSet cashSet)
		{
			foreach(KeyValuePair<ICash, int> denomination in cashSet.CashStack)
			{
				Add(denomination.Key, denomination.Value);
			}
		}

		/// <summary>
		/// Adds the given amount to this entry.
		/// If no matching entry exists, one will be created
		/// </summary>
		/// <param name="denomination">The denomination to adjust</param>
		/// <param name="amount">The amount of this denomination to add (use negative values to subtract)</param>
		public void Edit(ICash denomination, int amount)
		{
			bool exists = CashStack.TryGetValue(denomination, out int value);

			if (exists)
			{
				CashStack[denomination] = value + amount;
			}
			else
			{
				CashStack.Add(denomination, amount);
			}
		}

		/// <summary>
		/// Sums the cash available in the drawer
		/// </summary>
		public decimal GetSum()
		{
			decimal sum = 0;

			foreach (KeyValuePair<ICash, int> pair in CashStack)
			{
				sum += pair.Key.UnitValue * pair.Value;
			}

			return sum;
		}

		public override string ToString()
		{
			StringBuilder content = new StringBuilder("");

			foreach (KeyValuePair<ICash, int> pair in CashStack)
			{
				content.Append(String.Format("{0} times {1}\n", pair.Value.ToString(), pair.Key.ValueName));
			}

			return content.ToString();
		}
	}
}
