using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Warehouse.Models;

namespace Warehouse.ModelTests
{
	[TestFixture]
	class OrderTests
	{
		[Test]
		public void CanCreateOrder()
		{
			int ID = 0;
			int custID = 2;
			DateTime time = new DateTime(0);

			Order order = new Order { CustomerID = custID, OrderID = ID, OrderDate = time };

			Assert.IsNotNull(order);
			Assert.AreEqual(custID, order.CustomerID);
			Assert.AreEqual(ID, order.OrderID);
			Assert.AreEqual(time, order.OrderDate);
		}
	}
}
