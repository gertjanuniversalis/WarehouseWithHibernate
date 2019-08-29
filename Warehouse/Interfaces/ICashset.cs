using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	public interface ICashSet
	{
		SortedDictionary<ICash, int> CashStack { get; }

		decimal GetSum();
		void Add(ICash denomination, int amount = 0);
		void Add(ICashSet cashSet);
		void Edit(ICash denomination, int amount = 0);
	}
}
