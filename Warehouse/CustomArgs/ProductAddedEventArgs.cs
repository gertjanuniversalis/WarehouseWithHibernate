using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.CustomArgs
{
	public class ProductAddedEventArgs : EventArgs
	{
		public IProduct product;
		public int amount;
	}
}
