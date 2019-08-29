using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Models;
using Warehouse.Interfaces;

namespace Warehouse.Controllers
{
	public static class CashController
	{

		public static ICashSet SmallestSetForValue(decimal value)
		{
			//TODO
			//make a cashset from infinite resouce, to add to the drawer
			throw new NotImplementedException();
		}

		public static ICashSet SmallestSetForValue(decimal value, ICashSet availableItems)
		{
			//TODO
			//make a cashset out of the drawer
			throw new NotImplementedException();
		}





		private static ICashSet GetInfiniteSet()
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

		private static ICashSet GetEmprySet()
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
