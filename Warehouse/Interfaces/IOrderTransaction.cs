using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	interface IOrderTransaction
	{
		void StartTransaction(int userID);

		void AddToOrder(IProduct product, int amount);

		void Commit();

		void Revert();
	}
}
