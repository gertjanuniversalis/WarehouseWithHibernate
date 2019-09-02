using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.Interfaces
{
	public interface IOrder
	{
		/// <summary>
		/// The active OrderID
		/// </summary>
		int OrderID { get; }

		/// <summary>
		/// The ID of the customer making the order
		/// </summary>

		int CustomerID { get; }

		/// <summary>
		/// The monent when the order was places
		/// </summary>
		DateTime OrderDate { get; }

		ISet<OrderedProduct> OrderedProducts { get;}
	}
}
