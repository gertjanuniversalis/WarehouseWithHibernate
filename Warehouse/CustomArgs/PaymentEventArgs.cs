using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.CustomArgs
{
	public class PaymentEventArgs : EventArgs
	{
		public string CashGivenStr { get; }

		public PaymentEventArgs(string cashGivenStr)
		{
			this.CashGivenStr = cashGivenStr;
		}
	}
}
