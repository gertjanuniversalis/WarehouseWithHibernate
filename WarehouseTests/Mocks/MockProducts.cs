using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Models;

namespace Warehouse.Mocks
{
	static class MockProducts
	{
		public static Product BasicProduct { get; private set; }

		static MockProducts()
		{
			BasicProduct = MakeBasicProduct();
		}

		private static Product MakeBasicProduct()
		{
			return new Product()
			{
				Id = 10,
				BarCode = 1234,
				Description = "A fake item",
				UnitPrice = 12.34m
			};
		}
	}
}
