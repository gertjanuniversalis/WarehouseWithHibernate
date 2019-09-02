using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	public class Order : IOrder
	{
		public virtual int OrderID { get; set; }

		public virtual int CustomerID { get; set; }

		public virtual DateTime OrderDate { get; set; }

		public virtual ISet<OrderedProduct> OrderedProducts { get; set; } = new HashSet<OrderedProduct>();
	}
}
