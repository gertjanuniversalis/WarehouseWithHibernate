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
	class OrderedProductTests
	{
		[Test]
		public void CanCreateOrderedItem()
		{
			Order order = Mocks.MockOrders.StandardOrder;
			Product product = Mocks.MockProducts.BasicProduct;
			int odID = 2;
			int quantity = 4;


			OrderedProduct ordered = new OrderedProduct { Id = odID, Order = order, Product = product, Quantity = quantity };

			Assert.IsNotNull(ordered);
			Assert.AreEqual(product, ordered.Product);
			Assert.AreEqual(order, ordered.Order);
		}
	}
}
