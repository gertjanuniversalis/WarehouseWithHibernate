using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Obsolete.Models
{
#pragma warning disable CS0738 // 'Order' does not implement interface member 'IOrder.OrderedProducts'. 'Order.OrderedProducts' cannot implement 'IOrder.OrderedProducts' because it does not have the matching return type of 'ISet<OrderedProduct>'.
	public class Order : IOrder
#pragma warning restore CS0738 // 'Order' does not implement interface member 'IOrder.OrderedProducts'. 'Order.OrderedProducts' cannot implement 'IOrder.OrderedProducts' because it does not have the matching return type of 'ISet<OrderedProduct>'.
	{
		public virtual int OrderID { get; set; }

		public virtual int CustomerID { get; set; }

		public virtual DateTime OrderDate { get; set; }

		public virtual ISet<OrderedProduct> OrderedProducts { get; set; } = new HashSet<OrderedProduct>();
	}
}
