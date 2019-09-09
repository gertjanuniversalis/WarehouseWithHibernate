using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.CustomArgs
{
	public class CartContentChangedEventArgs : EventArgs
	{
		public IProduct Product { get; }
		public int Amount { get; }
		public bool Added { get; }

		public decimal NewSumTotal { get; }

		public CartContentChangedEventArgs(IProduct product, int amount, bool added, decimal newSum)
		{
			this.Product = product;
			this.Amount = amount;
			this.Added = added;
			this.NewSumTotal = newSum;
		}
	}
}
