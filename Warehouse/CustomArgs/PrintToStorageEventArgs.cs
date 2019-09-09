using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Models;

namespace Warehouse.CustomArgs
{
	public class PrintToStorageEventArgs: EventArgs
	{
#pragma warning disable CS0052 // Inconsistent accessibility: field type 'Order' is less accessible than field 'PrintToStorageEventArgs.order'
		public Order order;
#pragma warning restore CS0052 // Inconsistent accessibility: field type 'Order' is less accessible than field 'PrintToStorageEventArgs.order'
	}
}
