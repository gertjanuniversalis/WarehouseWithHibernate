using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using Warehouse.Models;

namespace Warehouse.ModelTests
{
	[TestFixture]
	class CashItemTests
	{
		[Test]
		public void CanCreateCashItem()
		{
			try
			{
				string name = "TestItem";
				decimal value = 12m;

				CashItem item = new CashItem(name, value);

				Assert.IsNotNull(item);
				Assert.AreEqual(value, item.UnitValue);
			}
			catch
			{
				Assert.Fail();
			}
		}
	}
}
