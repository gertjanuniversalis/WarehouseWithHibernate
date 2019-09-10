using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.CustomArgs
{
	public class ProvideChangeEventArgs
	{
		public decimal CashGiven { get; }
		public decimal TransactionValue { get; }

		public ProvideChangeEventArgs(decimal cashGiven, decimal transVal)
		{
			this.CashGiven = cashGiven;
			this.TransactionValue = transVal;
		}
	}
}
