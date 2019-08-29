using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	class Order : IOrder
	{
		public virtual int Id { get; set; }
		public virtual int CustomerID { get; set; }
		public virtual DateTime OrderDate { get; set; }
	}
}
