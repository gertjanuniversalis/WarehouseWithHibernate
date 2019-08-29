using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Warehouse.Models;

namespace Warehouse.ModelTests
{
	[TestFixture]
	class CashSetTests
	{
		[Test]
		public void CanCreateEmptyCashSet()
		{
			try
			{
				CashSet set = new CashSet();

				Assert.IsNotNull(set);
			}
			catch
			{
				Assert.Fail();
			}
		}

		[Test]
		public void CanCreateDefaultCashSet()
		{
			try
			{
				CashSet set = Mocks.MockCashSets.MakeStandardSet();

				Assert.IsNotNull(set);
				Assert.AreEqual(34.71m, set.GetSum());
			}
			catch
			{
				Assert.Fail();
			}
		}
	}
}
