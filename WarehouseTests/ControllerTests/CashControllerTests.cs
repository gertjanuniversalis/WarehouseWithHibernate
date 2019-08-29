using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Warehouse.Controllers;
using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.ControllerTests
{
	[TestFixture]
	class CashControllerTests
	{
		[Test]
		[TestCase(50)]
		[TestCase(5)]
		[TestCase(0.5)]
		[TestCase(0.05)]
		public void CanMakeSingleTypeCashSets(decimal value)
		{
			CashSet cashSet = (CashSet)CashController.SmallestSetForValue(value);

			ICash item = (from cashItem 
								 in cashSet.CashStack
								 where cashItem.Key.UnitValue == value
								 select cashItem.Key).FirstOrDefault();

			cashSet.CashStack.TryGetValue(item, out int amount);

			Assert.AreEqual(1, amount);
		}
	}
}
