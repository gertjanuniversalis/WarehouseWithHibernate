using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	class ProductModel : IProduct
	{
		public virtual int Id { get; set; }

		public virtual int BarCode { get; set; }

		public virtual decimal UnitPrice { get; set; }

		public virtual string Description { get; set; }		
	}
}
