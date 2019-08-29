using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	public interface IProduct
	{
		int Id { get; }
		int BarCode { get; }
		decimal UnitPrice { get; }
		string Description { get; }
	}
}
