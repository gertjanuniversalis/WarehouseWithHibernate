using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Models;

namespace Warehouse.Mocks
{
	static class MockOrders
	{
		public static Order StandardOrder { get; set; }

		static MockOrders()
		{
			StandardOrder = MakeStandardOrder();
		}

		private static Order MakeStandardOrder()
		{
			return new Order()
			{
				CustomerID = 1,
				OrderID = 3,
				OrderDate = new DateTime(100),
			};
		}
	}
}
