using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	public interface ICash : IComparable
	{
		/// <summary>
		/// The printable name of this item
		/// </summary>
		string ValueName { get; }

		/// <summary>
		/// The normalised value of this item
		/// </summary>
		decimal UnitValue { get; }
	}
}
