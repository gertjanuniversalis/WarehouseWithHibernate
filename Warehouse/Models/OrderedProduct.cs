using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
	public class OrderedProduct
	{
		public virtual int Id { get; set; }
		public virtual Product Product { get; set; }
		public virtual Order Order { get; set; }
		public virtual int Quantity { get; set; }
	}
}
