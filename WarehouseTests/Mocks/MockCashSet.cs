using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.Mocks
{
	static class MockCashSets
	{
		public static CashSet EmptySet { get; private set; }
		public static CashSet InfiniteSet { get; private set; }

		/// <summary>
		/// value: 34.71
		/// </summary>
		public static CashSet StandardSet { get; private set; }

		static MockCashSets()
		{
			EmptySet = MakeEmptyset();
			InfiniteSet = MakeInfiniteSet();
			StandardSet = MakeStandardSet();
		}

		public static CashSet MakeStandardSet()
		{
			return new CashSet(new SortedDictionary<ICash, int>
			{
				{ new CashItem("50 euros", 50m), 0 },
				{ new CashItem("20 euros", 20m), 1 },
				{ new CashItem("10 euros", 10m), 0 },
				{ new CashItem("5 euros", 5m), 1 },
				{ new CashItem("2 euros", 2m), 1 },
				{ new CashItem("1 euros", 1m), 4 },
				{ new CashItem("50 cents", 0.5m), 2 },
				{ new CashItem("20 cents", 0.2m), 10 },
				{ new CashItem("10 cents", 0.1m), 3 },
				{ new CashItem("5 cents", 0.05m), 7 },
				{ new CashItem("2 cents", 0.02m), 1 },
				{ new CashItem("1 cent", 0.01m), 4 }
			});
		}

		private static CashSet MakeInfiniteSet()
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

		private static CashSet MakeEmptyset()
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
