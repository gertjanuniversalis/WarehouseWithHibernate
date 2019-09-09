using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.CustomArgs
{
	class OrderEventArgs
	{
		public string OrderIdStr { get; }

		public OrderEventArgs(string orderID)
		{
			this.OrderIdStr = orderID;
		}
	}
}
