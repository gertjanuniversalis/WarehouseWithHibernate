using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Models;
using Warehouse.Interfaces;

namespace Warehouse.Controllers
{
	/// <summary>
	/// Holds static methods to manipulate and create CashSets and CashItems
	/// </summary>
	public static class CashController
	{
		/// <summary>
		/// Builds an ICashSet containing the smallest amount of items totalling at the defined value
		/// </summary>
		/// <param name="value">The desired value of the ICashSet</param>
		public static ICashSet SmallestSetForValue(decimal value)
		{
			return SmallestSetForValue(value, InfiniteSet());
		}

		/// <summary>
		/// Builds an ICashSet containing the smallest amount of items totalling at the defined value from a resticted list
		/// </summary>
		/// <param name="value">The desired value of the ICashSet</param>
		/// <param name="availableItems">The set of ICashItems that can be used to build the resurnset</param>
		public static ICashSet SmallestSetForValue(decimal value, ICashSet availableItems)
		{
			if (value <= 0) { return null; }

			ICashSet returnSet = new CashSet();
			decimal valueToAllocate = value;
			decimal setValue = availableItems.GetSum();

			if (setValue == value)
			{
				return availableItems;
			}
			else if (setValue > value)
			{
				foreach (KeyValuePair<ICash, int> denomination in availableItems.CashStack)
				{
					int amountNeeded = (int)(valueToAllocate / denomination.Key.UnitValue);

					if (amountNeeded == 0 || denomination.Value == 0)
					{
						//we either don't need this denomination, or we don't have any to use => skip adding it to the set
						continue;
					}
					else if (denomination.Value >= amountNeeded)
					{
						//There are sufficient units of this value available => add the desired amount to the set
						returnSet.Add(denomination.Key, amountNeeded);
						valueToAllocate -= denomination.Key.UnitValue * amountNeeded;
					}
					else
					{
						//There are insufficient units available (but more than none) => use them all
						returnSet.Add(denomination.Key, denomination.Value);
						valueToAllocate -= denomination.Key.UnitValue * denomination.Value;
					}

					if (valueToAllocate == 0)
					{
						//All value has been accounted for: return the set
						return returnSet;
					}
				}
				return null;
			}
			else
			{
				return null;
			}
		}




		/// <summary>
		/// Provides a functionally infinitely large cashset to loop trough
		/// </summary>
		private static ICashSet InfiniteSet()
		{
			return new CashSet(new SortedDictionary<ICash, int>
			{
				{ new CashItem("50 euros", 50m), 1000 },
				{ new CashItem("20 euros", 20m), 1000 },
				{ new CashItem("10 euros", 10m), 1000 },
				{ new CashItem("5 euros", 5m), 1000 },
				{ new CashItem("2 euros", 2m), 1000 },
				{ new CashItem("1 euros", 1m), 1000 },
				{ new CashItem("50 cents", 0.5m), 1000 },
				{ new CashItem("20 cents", 0.2m), 1000 },
				{ new CashItem("10 cents", 0.1m), 1000 },
				{ new CashItem("5 cents", 0.05m), 1000 },
				{ new CashItem("2 cents", 0.02m), 1000 },
				{ new CashItem("1 cent", 0.01m), 1000 }
			});
		}

		/// <summary>
		/// Provides a CashSet with all available entries, and a sum total of 0
		/// </summary>
		private static ICashSet EmptySet()
		{
			return new CashSet(new SortedDictionary<ICash, int>
			{
				{ new CashItem("50 euros", 50m), 0 },
				{ new CashItem("20 euros", 20m), 0 },
				{ new CashItem("10 euros", 10m), 0 },
				{ new CashItem("5 euros", 5m), 0 },
				{ new CashItem("2 euros", 2m), 0 },
				{ new CashItem("1 euros", 1m), 0 },
				{ new CashItem("50 cents", 0.5m), 0 },
				{ new CashItem("20 cents", 0.2m), 0 },
				{ new CashItem("10 cents", 0.1m), 0 },
				{ new CashItem("5 cents", 0.05m), 0 },
				{ new CashItem("2 cents", 0.02m), 0 },
				{ new CashItem("1 cent", 0.01m), 0 }
			});
		}
	}
}
