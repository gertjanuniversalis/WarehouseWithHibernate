using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Warehouse.Models;

namespace Warehouse.ModelTests
{
	[TestFixture]
	class ShoppingCartTests
	{
		[Test]
		public void CanCreateEmptyCart()
		{
			ShoppingCart cart = new ShoppingCart();

			Assert.IsNotNull(cart);
			Assert.IsEmpty(cart.CartItems);
		}
	}
}
