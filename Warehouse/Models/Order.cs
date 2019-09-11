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


		public virtual string AsString()
		{
			StringBuilder builder = new StringBuilder(string.Format("Order number {0} contains:\n", OrderID.ToString()));

			builder.Append(string.Join("\n\t", OrderedProducts.Select(p => p.Quantity + " times " + p.Product.Description)));
			builder.Append(string.Format("\nFor a total value of {0}", OrderedProducts.Sum(p => p.Product.UnitPrice * p.Quantity).ToString()));
			builder.Append(string.Format("\nOrder finalised at {0}", OrderDate.ToString()));

			return builder.ToString();
		}
	}
}
