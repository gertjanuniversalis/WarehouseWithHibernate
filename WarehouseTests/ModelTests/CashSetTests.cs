using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Warehouse.Interfaces;
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
		public void CanCreateCashSetFromDictionary()
		{
			try
			{
				var dict = new SortedDictionary<ICash, int>
				{
					{ new CashItem("2 cents", 0.02m), 1 },
					{ new CashItem("1 cent", 0.01m), 4 }
				};

				CashSet set = new CashSet(dict);

				Assert.IsNotNull(set);
				Assert.AreEqual(0.06m, set.GetSum());
			}
			catch
			{
				Assert.Fail();
			}
		}

		[Test]
		public void CanCreateCashSetFromSet()
		{
			ICashSet initialSet = Mocks.MockCashSets.StandardSet;

			CashSet madeSet = new CashSet(initialSet);

			Assert.AreEqual(initialSet.CashStack, madeSet.CashStack);
		}

		[Test]
		public void CanAddDictionary()
		{
			CashSet set = new CashSet(Mocks.MockCashSets.StandardSet);

			set.Add(new CashSet(new SortedDictionary<ICash, int>
				{
					{ new CashItem("2 cents", 0.02m), 1 },
					{ new CashItem("1 cent", 0.01m), 4 }
				}));

			Assert.AreEqual(34.77, set.GetSum());
		}
	}
}
