using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.ModelTests
{
	[TestFixture]
	class ProductTests
	{
		[Test]
		public void CanCreateProduct()
		{
			int id = 2;
			decimal price = 3.1m;
			string desc = "Quick Item";
			int code = 938;

			IProduct product = new Product()
			{
				Id = id,
				UnitPrice = price,
				Description = desc,
				BarCode = code
			};

			Assert.IsNotNull(product);
			Assert.AreEqual(price, product.UnitPrice);
		}
	}
}
