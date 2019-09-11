using System;
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
		private bool EventTriggered;

		[Test]
		[TestCase(30)]
		[TestCase(10)]
		[TestCase(5)]
		[TestCase(0.5)]
		public void CanCreatePayout(decimal value)
		{
			var payCont = new Controllers.PaymentController(new TillDrawer(Mocks.MockCashSets.StandardSet));
			CashSet standardSet = Mocks.MockCashSets.StandardSet;

			CashSet payout = (CashSet)payCont.Payout(value, standardSet);

			Assert.IsNotNull(payout);
			Assert.AreEqual(value, payout.GetSum());
		}

		[Test]
		public void CanCalculateChange()
		{
			var payCont = new Controllers.PaymentController(new TillDrawer(Mocks.MockCashSets.StandardSet));

			payCont.PaymentPossible += EventListener;

			payCont.DetermineChange(null, new CustomArgs.ProvideChangeEventArgs(10, 8));

			Assert.IsTrue(EventTriggered);
		}

		private void EventListener(object sender, CustomArgs.PaymentCompletedEventArgs e)
		{
			EventTriggered = true;
		}
	}
}
