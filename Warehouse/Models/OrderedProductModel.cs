using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
	class OrderedProductModel
	{
		public virtual int Id { get; set; }
		public virtual int ProductID { get; set; }
		public virtual int OrderID { get; set; }
		public virtual int Quantity { get; set; }
	}
}
