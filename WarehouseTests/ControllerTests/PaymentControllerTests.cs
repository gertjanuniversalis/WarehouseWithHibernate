﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Warehouse.Models;

namespace Warehouse.ControllerTests
{
	[TestFixture]
	class PaymentControllerTests
	{
		[Test]
		[TestCase(30)]
		[TestCase(10)]
		[TestCase(5)]
		[TestCase(0.5)]
		public void CanCreatePayout(decimal value)
		{
			CashSet standardSet = Mocks.MockCashSets.StandardSet;
			decimal returnvalue = standardSet.GetSum() - value;

			CashSet payout = (CashSet)Controllers.PaymentController.Payout(value, standardSet);

			Assert.IsNotNull(payout);
			Assert.AreEqual(value, payout.GetSum());
		}
	}
}