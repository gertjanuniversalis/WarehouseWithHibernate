using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	public interface ITillDrawer
	{
		ICashSet DrawerContent { get; }

		/// <summary>
		/// Sums the total value of all cash in this drawer
		/// </summary>
		/// <returns></returns>
		decimal GetTotalValue();

		/// <summary>
		/// Checks if there is sufficient value available to provide change
		/// </summary>
		/// <param name="changeSum">The value required</param>
		/// <returns></returns>
		bool ContainsEnough(decimal changeSum);

		/// <summary>
		/// Modifies the amount of this cashitem in the drawer
		/// </summary>
		/// <param name="cashItem">The CashItem to adjust</param>
		/// <param name="difference">The amount to add to the drawer (negative to subtract)</param>
		void ChangeContent(ICash cashItem, int difference);

		/// <summary>
		/// Modifies the amount of this cash in the drawer
		/// </summary>
		/// <param name="value">The value of the added or removed cash (smallest possible CashSet assumed)</param>
		/// <remarks>If any CashSet other than the smallest possible is used; pass the set itself, instead of the value</remarks>
		void Add(decimal value);

		/// <summary>
		/// Modifies the amount of this cash in the drawer
		/// </summary>
		/// <param name="changeSet">The CashSet representing the change in the drawer</param>
		void Add(ICashSet changeSet);

		void Remove(decimal value);

		void Remove(ICashSet changeSet);

		/// <summary>
		/// Creates the smallest set of denominations that totals to the required sum
		/// </summary>
		/// <param name="changeAmount">The sum of money to create</param>
		/// <returns>The smallest CashSet values at the passed sum, or NULL</returns>
		ICashSet MakeChange(decimal changeAmount);
	}
}
