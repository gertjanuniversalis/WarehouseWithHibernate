using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.CustomArgs
{
	public class PaymentCompletedEventArgs
	{
		public CashSet GivenCash { get; }
		public CashSet PayoutSet { get; }

		public PaymentCompletedEventArgs(CashSet givenCash, CashSet returnCash)
		{
			GivenCash = givenCash;
			PayoutSet = returnCash;
		}
	}
}
