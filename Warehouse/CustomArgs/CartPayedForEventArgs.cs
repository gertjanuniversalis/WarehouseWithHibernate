using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.CustomArgs
{
	public class CartPayedForEventArgs : EventArgs
	{
		public Dictionary<IProduct, int> ProcessedCart { get; }

		public CartPayedForEventArgs(Dictionary<IProduct, int> cartContent)
		{
			this.ProcessedCart = cartContent;
		}
	}
}
