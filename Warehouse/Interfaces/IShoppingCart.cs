using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Models;

namespace Warehouse.Interfaces
{
	public interface IShoppingCart
	{
		Dictionary<IProduct, int> CartItems { get; }
		/// <summary>
		/// Calculates the value of this transaction
		/// </summary>
		/// <returns>The value</returns>
		decimal GetTransactionValue();

		/// <summary>
		/// Adds a new product to the transaction
		/// </summary>
		/// <param name="product">The product to add</param>
		/// <param name="amount">(optional) The amount of this product to add at once; default = 1</param>
		/// <returns>The success of the addition</returns>
		Success AddItem(int barCode, int amount = 1);

		/// <summary>
		/// Removes items from the transaction
		/// </summary>
		/// <param name="product">The product to remove</param>
		/// <param name="amount">(optional) The amount of the product to remove; -1 for 'all' (default)</param>
		/// <returns></returns>
		Success RemoveItem(int barCode, int amount = 1);

		/// <summary>
		/// Gets the contents of the current transaction
		/// </summary>
		string ToString();
	}
}
